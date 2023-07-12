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
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Debug.Log("Hit Player");
            other.gameObject.GetComponent<PlayerStats>().hp -= (soldierScript.enemyDamage - other.gameObject.GetComponent<PlayerStats>().defense);
            other.gameObject.GetComponent<PlayerStats>().hitAmount += 1;
            other.gameObject.GetComponent<PlayerStats>().isHit = true;
            other.gameObject.GetComponent<PlayerStats>().bloodScreenTimer = 5f;

            _collider.enabled = false;
            

        }
    }

    
}
