using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    // Boss current state var
    public State myCurrentState;

    // Reference to player
    Transform player;

    // Reference to player stats
    PlayerStats playerStats;

    // Reference to boss's weapon object
    public GameObject bossWeapon;

    // Reference to boss's animator
    public Animator animator;

    // Stopping distance
    private float stoppingDistance = 3f;

    // Check if enemy is hit
    public bool isHit;
    // Hit timer so player weapon collider doesn't trigger multiple times
    public float isHitTimer = 1f;

    float healTimer = 3f;


    public UnityEngine.AI.NavMeshAgent enemy;

    float prepareTimer = 0f;
    float maxPrepareTime = 10f;

    bool rollAttackChance = false;

    int attackChoice = 0;

    // Time for boss to despawn after losing
    float timeToDespawn;

    //Audio stuff
    AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        myCurrentState = State.IDLE;
        // FOV range of boss
        fovRange = 20;
        // Movementspeed of boss
        movementSpeed = 4;
        // Get player component
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        // Get animator component of parent
        animator = GetComponent<Animator>();
        // Get Player stats component
        playerStats = player.GetComponent<PlayerStats>();
        enemyDamage = 20;
        maxHP = 500;
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
                    animator.SetBool("isChasing", true);
                    myCurrentState = State.CHASE;
                }
                break;
            case State.CHASE:
                //Debug.Log("Chasing");
                animator.ResetTrigger("attack1");
                animator.ResetTrigger("attack2");
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
                        myCurrentState = State.ATTACK;
                    }
                }
                break;
            case State.ATTACK:
                if (!rollAttackChance)
                {
                    attackChoice = Random.Range(1, 3);
                    rollAttackChance = true;
                    Debug.Log(attackChoice);
                }
                if (attackChoice == 1)
                {
                    RotateTowards(player);
                    animator.SetTrigger("attack1");
                    

                }
                else if (attackChoice == 2)
                {
                    animator.SetTrigger("attack2");
                }

                break;
            case State.PREPARE:
                rollAttackChance = false;
                break;
        }
    }

    public void StartPrepare()
    {
        myCurrentState = State.PREPARE;
        animator.SetBool("isPreparing", true);
    }

    public void StopPrepare()
    {
        myCurrentState = State.CHASE;
        animator.SetBool("isPreparing", false);
    }

    public void StopMovement()
    {
        enemy.speed = 0;
    }

    public void StartMovement()
    {
        enemy.speed = 3.5f;
    }

    public void StartAttack()
    {
        bossWeapon.GetComponent<Collider>().enabled = true;
    }

    public void StopAttack()
    {
        bossWeapon.GetComponent<Collider>().enabled = false;
    }


    public void PlaySingleSwingSound()
    {
        audioManager.PlaySFX(audioManager.bossAttackSwing);

    }
}
