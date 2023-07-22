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

    //Audio stuff
    AudioManager audioManager;
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

        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Soldier")
        {
            if (!other.gameObject.GetComponent<Soldier>().isHit)
            {

                Debug.Log("Hit soldier");
                other.gameObject.GetComponent<Soldier>().healthPoint -= playerStats.attack + weaponAttack;
                other.gameObject.GetComponent<Soldier>().isHit = true;
                other.gameObject.GetComponent<Animator>().Play("flinch", 0 ,0);
                audioManager.PlaySFX(audioManager.hit);
            }
            
        }
        if (other.gameObject.tag == "Boss")
        {
            if (!other.gameObject.GetComponent<Boss>().isHit)
            {
                other.gameObject.GetComponent<Boss>().healthPoint -= playerStats.attack + weaponAttack;
                other.gameObject.GetComponent<Boss>().isHit = true;
            }
        }
    }

    




}
