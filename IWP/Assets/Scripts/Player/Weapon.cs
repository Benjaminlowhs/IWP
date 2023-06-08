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
    //public GameObject soldier;
    //Soldier soldierScript;


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
        //soldierScript = soldier.GetComponent<Soldier>();

        
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
                //Debug.Log("Hit soldier");
                other.gameObject.GetComponent<Soldier>().healthPoint -= playerStats.attack + weaponAttack;
                //if (soldierScript.healthPoint < (0.2 * soldierScript.maxHP))
                //{
                //    if (!soldierScript.hasRolled)
                //    {

                //        soldierScript.RollSurrenderChance();
                //        if (soldierScript.hasRolled)
                //        {
                //            soldierScript.myCurrentState = Enemy.State.SURRENDER;
                //        }
                //    }
                //}
                playerAttack_script.isAttacking = false;
            }
            
        }
    }




}
