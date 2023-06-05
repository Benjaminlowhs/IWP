using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{

    Collider _collider;
    GameObject player;
    PlayerAttack playerAttack_script;
    PlayerStats playerStats;
    public int weaponAttack = 10;

    // Start is called before the first frame update
    void Start()
    {

        _collider = GetComponent<Collider>();
        _collider.enabled = false;
        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerAttack_script = player.GetComponent<PlayerAttack>();
        }

        playerStats = player.GetComponent<PlayerStats>();

        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerAttack_script.isAttacking == true)
        {
            _collider.enabled = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Soldier")
        {
            if (playerAttack_script.isAttacking != false)
            {
                Debug.Log("Hit soldier");
                other.gameObject.GetComponent<Soldier>().healthPoint -= playerStats.attack + weaponAttack;
                playerAttack_script.isAttacking = false;
            }
            
        }
    }


}
