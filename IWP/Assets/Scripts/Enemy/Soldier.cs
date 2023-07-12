using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Soldier : Enemy
{
    public State myCurrentState;
    public Transform player;
    PlayerStats playerStats;
    public GameObject enemyWeapon;

    public Animator animator;
    private float stoppingDistance = 3f;

    public NavMeshAgent enemy;

    // Time for soldier to despawn
    float timeToDespawn;

    // Check if enemy is hit
    public bool isHit;
    // Hit timer so player weapon collider doesn't trigger multiple times
    public float isHitTimer = 1f;

    public bool hasRolled = false;
    float chance;

    //Spawn dropped items
    public GameObject gold;
    public GameObject bandage;
    public GameObject iron;
    public GameObject steel;
    public GameObject carbon;

    float healTimer = 3f;

    // Start is called before the first frame update
    void Start()
    {
        myCurrentState = State.IDLE;
        chance = 0f;
        fovRange = 7;
        movementSpeed = 2;
        //player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        animator = GetComponent<Animator>();
        playerStats = player.GetComponent<PlayerStats>();
        enemyLevel = playerStats.level + 2;
        enemyDamage = 5;
        maxHP = enemyLevel * 25;
        healthPoint = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        

        if (isHit)
        {
            isHitTimer -= Time.deltaTime;
            if (isHitTimer < 0f)
            {
                isHit = false;
                isHitTimer = 1f;
            }
        }

        if (healthPoint <= 0)
        {
            if (myCurrentState == State.SURRENDER)
            {
                playerStats.honor -= 1;
            }
            else
            {
                playerStats.honor += 1;
            }
            DropItem();
            die();
            playerStats.xp += enemyLevel * 5;
        }

        
        
        switch (myCurrentState)
        {
            case State.IDLE:
                //Debug.Log("Idling");
                if (healthPoint > 0 && healthPoint < maxHP)
                {
                    healTimer -= Time.deltaTime;
                    if (healTimer < 0)
                    {
                        healthPoint += 5;
                        if (healthPoint > maxHP)
                        {
                            healthPoint = maxHP;
                        }
                        healTimer = 3f;
                    }
                }
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
                    //transform.position += transform.forward * movementSpeed * Time.deltaTime;
                    enemy.SetDestination(player.position);
                    if (stoppingDistance >= Vector3.Distance(player.position, transform.position))
                    {
                        animator.SetBool("isChasing", false);
                        myCurrentState = State.ATTACK;
                    }
                }
                break;
            case State.ATTACK:
                RotateTowards(player);
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
                animator.SetBool("isSurrender", true);
                if (timeToDespawn <= Time.time)
                {
                    playerStats.honor += 1;
                    Debug.Log("Surrender kill");
                    Destroy(gameObject);
                    
                }
                //Debug.Log("Surrender");
                break;
        }

        if (healthPoint <= (0.2 * maxHP))
        {
            if (!hasRolled)
            {
                if (RollSurrenderChance())
                {
                    myCurrentState = State.SURRENDER;

                    timeToDespawn = Time.time + 10f;
                }
            }
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

    public bool RollSurrenderChance()
    {
        chance = (float)playerStats.honor / (enemyLevel * 20);
        if (chance > 100)
        {
            chance = 100;
        }

        hasRolled = true;
        if (Random.value <= chance)
        {
            //Debug.Log(chance.ToString("F8") + ", successful");
            return true;
        }
        else
        {
            //Debug.Log(chance.ToString("F8") + ", failed");
            return false;
        }
    }

    

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Death")
        {
            Debug.Log("Suicide");
            Destroy(gameObject);

        }
    }

    private void DropItem()
    {
        if (Random.value <= 0.01)
        {
            Instantiate(carbon, transform.position, Quaternion.identity);
        }
        if (Random.value <= 0.05)
        {
            Instantiate(steel, transform.position, Quaternion.identity);
        }
        if (Random.value <= 0.1)
        {
            Instantiate(iron, transform.position, Quaternion.identity);
        }
        if (Random.value <= 0.15)
        {
            Instantiate(gold, transform.position, Quaternion.identity);
        }
        if (Random.value <= 1.0)
        {
            Instantiate(bandage, transform.position, Quaternion.identity);
        }
    }
}
