using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerStats : MonoBehaviour
{

    public int xp;
    public int hp;
    public int level;
    public int maxHp = 10;
    public int attack;
    public int defense;
    public float speed;
    private int carryOverXp;
    public int levelUpPoint;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;

    public TextMeshProUGUI lvlUpPointText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;


    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        speed = 5f;
        attack = 5;
        defense = 5;
        levelUpPoint = 0;
        hp = maxHp;
        healthText.text = "HP: " + hp.ToString();
        xpText.text = "XP: " + xp.ToString();
        levelText.text = "Level: " + level.ToString();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            //Debug.Log("Dead");
        }

        if (xp >= level * 10)
        {
            carryOverXp = xp - (level * 10);
            xp = carryOverXp;
            level += 1;
            levelUpPoint += 1;
        }

        healthText.text = "HP: " + hp.ToString();
        xpText.text = "XP: " + xp.ToString();
        levelText.text = "Level: " + level.ToString();

        lvlUpPointText.text = "Available Points: " + levelUpPoint;
        atkText.text = attack.ToString();
        defText.text = defense.ToString();
    }

    public void UpgradeAtk()
    {
        attack += 1;
    }

    public void UpgradeDef()
    {
        defense += 1;
    }



}
