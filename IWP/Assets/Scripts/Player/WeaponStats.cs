using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponStats : MonoBehaviour
{

    Weapon weapon;
    int weaponDamage;
    int weaponLevel;

    int requiredAmount;
    int requiredID;


    public TextMeshProUGUI weaponLvlUI;
    public TextMeshProUGUI weaponDmgUI;

    public Image requiredMatImg;
    public TextMeshProUGUI requiredAmt;

    public Image EnhanceBackground;

    public Sprite ironImg;
    public Sprite steelImg;
    public Sprite carbonImg;

    Button enhanceBtn;

    public InventoryObject inventory;

    // Start is called before the first frame update
    void Start()
    {
        weapon = transform.GetComponent<Weapon>();
        enhanceBtn = EnhanceBackground.GetComponent<Button>();
        enhanceBtn.enabled = false;
        weaponDamage = weapon.weaponAttack;
        //Debug.Log(weaponDamage + "," + weapon.weaponAttack);
        weaponLevel = 1;
    }

    // Update is called once per frame
    void Update()
    {
        weaponLvlUI.text = weaponLevel.ToString();
        weaponDmgUI.text = weaponDamage.ToString();

        CheckRequiredMaterials();
    }

    public void CheckRequiredMaterials()
    {
        if(weaponLevel == 1 || weaponLevel == 2 || weaponLevel == 3 )
        {
            requiredID = 1;
            requiredAmount = weaponLevel * 5;
        }
        else if (weaponLevel == 4 || weaponLevel == 5 || weaponLevel == 6)
        {
            requiredID = 2;
            requiredAmount = (weaponLevel - 3) * 5;
        }

        if (requiredID == 1)
        {
            requiredMatImg.sprite = ironImg;
            requiredAmt.text = requiredAmount.ToString();
        }
        else if (requiredID == 2)
        {
            requiredMatImg.sprite = steelImg;
            requiredAmt.text = requiredAmount.ToString();
        }



        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
           
            if (inventory.Container.Items[i].ID == requiredID)
            {
                if (inventory.Container.Items[i].amount >= requiredAmount)
                {
                    EnhanceBackground.color = new Color(0, 1, 0, 1);
                    enhanceBtn.enabled = true;
                }
                else
                {
                    EnhanceBackground.color = new Color(1, 0, 0, 1);
                    enhanceBtn.enabled = false;
                }
                
            }
        }
    }

    public void EnhanceWeapon()
    {
        weaponLevel += 1;
        weaponDamage += 5;
        weapon.weaponAttack += 5;
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {

            if (inventory.Container.Items[i].ID == requiredID)
            {
                inventory.Container.Items[i].amount -= requiredAmount;

            }
        }
    }
}
