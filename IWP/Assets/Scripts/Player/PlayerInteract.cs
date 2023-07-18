using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject inventory;
    public GameObject stats;
    public GameObject weaponStats;
    private bool toggle = false;
    bool statsToggle = false;
    bool weaponStatsToggle = false;
    public ParticleSystem bloodSplash;

    public Transform playerSpawnPoint;
    public Transform bossStageSpawn;

    PlayerStats playerStats;

    public void Start()
    {
        playerStats = transform.GetComponent<PlayerStats>();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI();
        }

        if (Input.GetKeyDown(KeyCode.U))
        {
            StatsUI();
        }
        if (Input.GetKeyDown(KeyCode.Y))
        {
            weaponStatsUI();
        }
    }

    public void InventoryUI()
    {
        toggle = !toggle;
        if (toggle)
            inventory.gameObject.SetActive(true);
        else
            inventory.gameObject.SetActive(false);
    }

    public void StatsUI()
    {
        statsToggle = !statsToggle;
        if (statsToggle)
            stats.gameObject.SetActive(true);
        else
            stats.gameObject.SetActive(false);
    }

    public void weaponStatsUI()
    {
        weaponStatsToggle = !weaponStatsToggle;
        if (weaponStatsToggle)
            weaponStats.gameObject.SetActive(true);
        else
            weaponStats.gameObject.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Death")
        {
            if (playerStats.stage == 1)
            {
                transform.position = new Vector3(64, 4, 5);
            }
            if (playerStats.stage == 2)
            {
                transform.position = bossStageSpawn.position;
            }
            transform.GetComponent<PlayerStats>().xp = 0;
            transform.GetComponent<PlayerStats>().hp = transform.GetComponent<PlayerStats>().maxHp;

        }

        // If player touches boss 1 portal
        if (other.gameObject.tag == "Portal")
        {
            if (playerStats.stage == 1)
            {
                transform.position = bossStageSpawn.position;
                playerStats.stage += 1;
            }
        }

        if (other.gameObject.tag == "EnemyWeapon" || other.gameObject.tag == "BossWeapon")
        {

            bloodSplash.Play();
        }
    }
}
