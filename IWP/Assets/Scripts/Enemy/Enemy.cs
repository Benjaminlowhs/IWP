using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float movementSpeed;
    public float maxHP;
    public int fovRange;
    public float healthPoint;
    public float enemyLevel;
    public int enemyDamage;

    public float rotationSpeed = 10f;


    public enum State
    {
        IDLE,
        CHASE,
        ATTACK,
        SURRENDER,
        PREPARE
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

    public void RotateTowards (Transform target)
    {
        Vector3 direction = (target.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

   
}
