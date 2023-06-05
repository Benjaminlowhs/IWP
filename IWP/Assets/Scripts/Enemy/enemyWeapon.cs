using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyWeapon : MonoBehaviour
{
    Collider _collider;
    public GameObject soldier;
    Soldier soldierScript;

    bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        isAttacking = false;
        soldierScript = soldier.GetComponent<Soldier>();
    }

    // Update is called once per frame
    void Update()
    {
        //if (soldierScript.myCurrentState == Enemy.State.ATTACK)
        //{
        //    isAttacking = true;
        //    Debug.Log("Attacking");
        //}
        //if (soldierScript.animator.GetCurrentAnimatorStateInfo(0).IsName("attack"))
        //{
        //    _collider.enabled = true;
        //}
        //else
        //{
        //    _collider.enabled = false;
        //}

        //if (isAttacking)
        //{
        //    _collider.enabled = true;
        //}
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("Hit Player");
            other.gameObject.GetComponent<PlayerStats>().hp -= (soldierScript.enemyDamage - other.gameObject.GetComponent<PlayerStats>().defense);
            _collider.enabled = false;
            

        }
    }

    
}
