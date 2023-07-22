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
    private float stoppingDistance = 2.5f;

    public NavMeshAgent enemy;

    // Time for soldier to despawn
    float timeToDespawn;

    // Check if enemy is hit
    public bool isHit;
    // Hit timer so player weapon collider doesn't trigger multiple times
    public float isHitTimer = 1f;

    public bool hasRolled = false;
    float chance;

    bool isAttacking = false;

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
        enemyDamage = 20;
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
            playerStats.xp += enemyLevel * 2;
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

                enemy.speed = 0f;

                if (fovRange >= Vector3.Distance(player.position, transform.position))
                {
                    //Debug.Log("In range");
                    if (!hasRolled)
                    {
                        if (RollSurrenderChance())
                        {
                            Debug.Log("Surrender");
                            myCurrentState = State.SURRENDER;

                            timeToDespawn = Time.time + 10f;
                        }
                        else
                        {

                            enemy.speed = 5f;
                            myCurrentState = State.CHASE;
                        }

                    }
                    else
                    {

                        enemy.speed = 5f;
                        myCurrentState = State.CHASE;
                    }

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
                    enemy.SetDestination(player.position);
                    if (stoppingDistance >= Vector3.Distance(player.position, transform.position))
                    {
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

        //if (healthPoint <= (0.2 * maxHP))
        //{
        //    if (!hasRolled)
        //    {
        //        if (RollSurrenderChance())
        //        {
        //            myCurrentState = State.SURRENDER;

        //            timeToDespawn = Time.time + 10f;
        //        }
        //    }
        //}

    }

    public void StartAttack()
    {
        enemyWeapon.GetComponent<Collider>().enabled = true;
        enemy.speed = 0;
    }

    public void StopAttack()
    {
        enemyWeapon.GetComponent<Collider>().enabled = false;
        enemy.speed = 5;
        animator.ResetTrigger("Attack");
    }

    public bool RollSurrenderChance()
    {
        chance = (float)playerStats.honor / (enemyLevel * 20);
        if (chance > 100)
        {
            chance = 100;
        }

        hasRolled = true;
        float random = Random.value;
        if (random <= chance)
        {
            //Debug.Log(chance.ToString("F8") + ", successful");
            //Debug.Log(random.ToString("F8") + ", successful");
            return true;
        }
        else
        {
            //Debug.Log(chance.ToString("F8") + ", failed");
            //Debug.Log(random.ToString("F8") + ", fail");
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
        else if (Random.value <= 0.05)
        {
            Instantiate(steel, transform.position, Quaternion.identity);
        }
        else if (Random.value <= 0.1)
        {
            Instantiate(iron, transform.position, Quaternion.identity);
        }
        else if (Random.value <= 0.15)
        {
            Instantiate(gold, transform.position, Quaternion.identity);
        }
        else if (Random.value <= 0.5)
        {
            Instantiate(bandage, transform.position, Quaternion.identity);
        }
    }
}
