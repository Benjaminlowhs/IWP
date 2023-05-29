using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float movementSpeed;
    public float maxHP;
    public int fovRange;
    public float healthPoint;
    public int enemyLevel;
    

    public enum State
    {
        IDLE,
        CHASE,
        ATTACK,
        SURRENDER
    };

    public State currentState;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.IDLE;
    }

    // Update is called once per frame
    //void Update()
    //{
    //     switch (currentState)
    //    {
    //        case State.IDLE:
    //            break;
    //    }
    //}

    protected void chase()
    {

    }

    protected void attack()
    {

    }

    protected void dodge()
    {

    }

    protected void idle()
    {

    }

    protected void die()
    {
        Destroy(gameObject);
    }

   
}
