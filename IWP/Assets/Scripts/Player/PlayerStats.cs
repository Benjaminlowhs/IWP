using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{

    public int xp;
    public int hp;
    public int level;
    public int maxHp = 10;
    public int attack;
    public int resistance;
    public float speed;
    private int carryOverXp;
    private Inventory inventory;
    [SerializeField] private UI_Inventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory();
        uiInventory.SetInventory(inventory);
        ItemWorld.SpawnItemWorld(new Vector3(0,0,0), new Item{ itemType = Item.ItemType.Bandage, amount = 1});
    }
    // Start is called before the first frame update
    void Start()
    {
        level = 1;
        speed = 5f;
        hp = maxHp;
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
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        ItemWorld itemWorld = other.GetComponent<ItemWorld>();
        if (itemWorld != null)
        {
            inventory.AddItem(itemWorld.GetItem());
            itemWorld.DestroySelf();
        }
    }

}
