using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    public State myCurrentState;
    Transform player;
    PlayerStats playerStats;
    public GameObject enemyWeapon;

    public Animator animator;
    private float stoppingDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        myCurrentState = State.IDLE;
        
        fovRange = 10;
        movementSpeed = 2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        playerStats = player.GetComponent<PlayerStats>();
        enemyLevel = playerStats.level + 2;
        enemyDamage = enemyLevel * 5;
        maxHP = enemyLevel * 5;
        healthPoint = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoint <= 0)
        {
            die();
            playerStats.xp += enemyLevel * 5;
        }

        
        else 

        switch (myCurrentState)
        {
            case State.IDLE:
                //Debug.Log("Idling");
                if (fovRange >= Vector3.Distance(player.position, transform.position))
                {
                    //Debug.Log("In range");
                    myCurrentState = State.CHASE;
                }
                break;
            case State.CHASE:
                //Debug.Log("Chasing");
                animator.SetBool("isChasing", true);
                if (fovRange < Vector3.Distance(player.position, transform.position))
                {
                    myCurrentState = State.IDLE;
                    animator.SetBool("isChasing", false);
                }
                else
                {
                    transform.LookAt(player);
                    transform.position += transform.forward * movementSpeed * Time.deltaTime;
                    if (stoppingDistance >= Vector3.Distance(player.position, transform.position))
                    {
                        animator.SetBool("isChasing", false);
                        myCurrentState = State.ATTACK;
                    }
                }
                break;
            case State.ATTACK:
                animator.SetTrigger("Attack");
                if (stoppingDistance < Vector3.Distance(player.position, transform.position))
                {
                    animator.SetBool("isChasing", true);
                    myCurrentState = State.CHASE;
                }
                //Debug.Log("Attacking");
                //playerStats.hp -= (enemyDamage - playerStats.defense);
                break;
            case State.SURRENDER:
                Debug.Log("Surrender");
                break;
        }

    }

    public void StartAttack()
    {
        enemyWeapon.GetComponent<Collider>().enabled = true;
    }

    public void StopAttack()
    {
        enemyWeapon.GetComponent<Collider>().enabled = false;
    }
}
