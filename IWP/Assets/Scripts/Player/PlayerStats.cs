using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{

    public int xp;
    public int hp;
    public int level;
    public int maxHp;
    public int attack;
    public int defense;
    public float speed;
    private int carryOverXp;
    public int levelUpPoint;
    public int honor;

    public TextMeshProUGUI healthText;
    public TextMeshProUGUI xpText;
    public TextMeshProUGUI levelText;
    public TextMeshProUGUI honorText;

    public TextMeshProUGUI lvlUpPointText;
    public TextMeshProUGUI atkText;
    public TextMeshProUGUI defText;


    public GameObject bloodScreen1;
    public GameObject bloodScreen2;
    public GameObject bloodScreen3;
    Image bs1;
    Image bs2;
    Image bs3;

    public bool isHit;
    public float bloodScreenTimer = 5f;
    public bool fadeBloodScreen = false;

    public GameObject atkBtn;
    public GameObject defBtn;

    public Transform playerRespawnPoint;

    public int hitAmount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        level = 1;
        speed = 5f;
        attack = 5;
        defense = 5;
        levelUpPoint = 0;
        maxHp = 100;
        hp = maxHp;
        honor = 0;

        bs1 = bloodScreen1.GetComponent<Image>();
        bs2 = bloodScreen2.GetComponent<Image>();
        bs3 = bloodScreen3.GetComponent<Image>();

        isHit = false;
        //healthText.text = "HP: " + hp.ToString();
        //xpText.text = "XP: " + xp.ToString();
        //levelText.text = "Level: " + level.ToString();
    }

    private void Update()
    {
        if (hp <= 0)
        {
            //Debug.Log("Dead");
            xp = 0;
            hp = maxHp;
            transform.position = playerRespawnPoint.position;
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
        honorText.text = "Honor: " + honor.ToString();

        lvlUpPointText.text = "Available Points: " + levelUpPoint;
        atkText.text = attack.ToString();
        defText.text = defense.ToString();

        LevelUpUI();
        BloodScreenUI();



    }

    public void UpgradeAtk()
    {
        attack += 1;
        levelUpPoint -= 1;
    }

    public void UpgradeDef()
    {
        defense += 1;
        levelUpPoint -= 1;
    }

    public void LevelUpUI()
    {
        if (levelUpPoint > 0)
        {
            atkBtn.gameObject.SetActive(true);
            defBtn.gameObject.SetActive(true);
        }
        else
        {
            atkBtn.gameObject.SetActive(false);
            defBtn.gameObject.SetActive(false);
        }
    }

    public void BloodScreenUI()
    {
        if (hitAmount == 1)
        {
            bs1.color = new Color(bs1.color.r, bs1.color.g, bs1.color.b, 0.6f);
        }
        if (hitAmount == 2)
        {
            bs2.color = new Color(bs1.color.r, bs1.color.g, bs1.color.b, 0.6f);
        }
        if (hitAmount == 3)
        {
            bs3.color = new Color(bs1.color.r, bs1.color.g, bs1.color.b, 0.6f);
        }
        if (hitAmount == 4)
        {
            bs1.color = new Color(bs1.color.r, bs1.color.g, bs1.color.b, 1f);
        }
        if (hitAmount == 5)
        {
            bs2.color = new Color(bs1.color.r, bs1.color.g, bs1.color.b, 1f);
        }
        if (hitAmount == 6)
        {
            bs3.color = new Color(bs1.color.r, bs1.color.g, bs1.color.b, 1f);
        }

        if (isHit == true)
        {
            bloodScreenTimer -= Time.deltaTime;
            if (bloodScreenTimer < 0f)
            {
                isHit = false;
                hitAmount = 0;
                fadeBloodScreen = true;
                bloodScreenTimer = 5f;
            }
        }

        if (fadeBloodScreen == true)
        {
            bs1.color -= new Color(0, 0, 0, 0.5f * Time.deltaTime);
            bs2.color -= new Color(0, 0, 0, 0.5f * Time.deltaTime);
            bs3.color -= new Color(0, 0, 0, 0.5f * Time.deltaTime);

            if (bs1.color.a < 0f && bs2.color.a < 0f && bs3.color.a < 0f)
            {
                fadeBloodScreen = false;
            }
        }
    }



}
