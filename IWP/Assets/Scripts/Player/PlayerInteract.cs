using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public GameObject inventory;
    private bool toggle = false;
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            InventoryUI();
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
    //public InventoryObject inventory;

    //private void OnMouseDown()
    //{
    //    if ()
    //}

    //// Start is called before the first frame update
    //void Start()
    //{
    //    inventory = new Inventory();
    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}

    //private void OnTriggerEnter(Collider other)
    //{
    //    ItemWorld itemWorld = other.GetComponent<ItemWorld>();
    //    if (itemWorld != null)
    //    {

    //    }
    //}
}
