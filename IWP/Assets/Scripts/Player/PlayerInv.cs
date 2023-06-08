using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInv : MonoBehaviour
{
    public InventoryObject inventory;
    public PotionObject potion;

    public void OnTriggerEnter(Collider other)
    {
        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            inventory.AddItem(new Item(item.item), 1);
            //Debug.Log(potion.restoreHealthValue);
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            inventory.Save();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            inventory.Load();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            for (int i = 0; i < inventory.Container.Items.Length; i++)
            {
                Debug.Log("ID: "+inventory.Container.Items[i].item.Id + "amount:" + inventory.Container.Items[i].amount);
                if (inventory.Container.Items[i].item.Id == 4)
                {
                    Debug.Log(inventory.Container.Items[i].amount);
                }
                
            }
        }
    }

    private void OnApplicationQuit()
    {
        inventory.Container.Items = new InventorySlot[15];
    }
}
