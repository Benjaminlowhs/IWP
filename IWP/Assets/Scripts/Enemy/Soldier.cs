using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soldier : Enemy
{
    State myCurrentState;
    // Start is called before the first frame update
    void Start()
    {
        myCurrentState = State.IDLE;
        healthPoint = maxHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (healthPoint <= 0)
        {
            die();
        }
    }
}
