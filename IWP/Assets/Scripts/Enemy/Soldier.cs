using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    State myCurrentState;
    Transform player;
    private float stoppingDistance = 2f;
    // Start is called before the first frame update
    void Start()
    {
        myCurrentState = State.IDLE;
        healthPoint = maxHP;
        fovRange = 10;
        movementSpeed = 2;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoint <= 0)
        {
            die();
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
                if (fovRange < Vector3.Distance(player.position, transform.position))
                {
                    myCurrentState = State.IDLE;
                }
                else
                {
                    transform.LookAt(player);
                    transform.position += transform.forward * movementSpeed * Time.deltaTime;
                    if (stoppingDistance >= Vector3.Distance(player.position, transform.position))
                    {
                        myCurrentState = State.ATTACK;
                    }
                }
                break;
            case State.ATTACK:
                if (stoppingDistance < Vector3.Distance(player.position, transform.position))
                {
                    myCurrentState = State.CHASE;
                }
                Debug.Log("Attacking");
                break;
            case State.SURRENDER:
                Debug.Log("Surrender");
                break;
        }

    }
}
