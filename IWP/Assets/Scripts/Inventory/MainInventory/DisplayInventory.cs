using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class DisplayInventory : MonoBehaviour
{
    public GameObject inventoryPrefab;

    public InventoryObject inventory;

    public int X_START;
    public int Y_START;

    public int X_SPACE_BETWEEN_ITEM;
    public int NUMBER_OF_COLUMN;
    public int Y_SPACE_BETWEEN_ITEM;
    Dictionary<GameObject, InventorySlot > itemsDisplayed = new Dictionary<GameObject,InventorySlot>();



    Transform player;
    PlayerStats playerStats;
    public PotionObject bandage;

    //Start is called before the first frame update
    void Start()
    {
        CreateSlots();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        playerStats = player.GetComponent<PlayerStats>();
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateDisplay();
        UpdateSlots();
    }

    public void UpdateSlots()
    {
        foreach (KeyValuePair<GameObject,InventorySlot> _slot in itemsDisplayed)
        {
            if (_slot.Value.ID >= 0 && _slot.Value.amount >= 1)
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[_slot.Value.item.Id].uiDisplay;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 1);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = _slot.Value.amount == 1 ? "" : _slot.Value.amount.ToString("n0");
            }
            else
            {
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().sprite = null;
                _slot.Key.transform.GetChild(0).GetComponentInChildren<Image>().color = new Color(1, 1, 1, 0);
                _slot.Key.GetComponentInChildren<TextMeshProUGUI>().text = "";
                
            }
        }
    }

    //public void UpdateDisplay()
    //{
    //    for (int i = 0; i < inventory.Container.Items.Count; i++)
    //    {
    //        InventorySlot slot = inventory.Container.Items[i];
    //        if (itemsDisplayed.ContainsKey(slot))
    //        {
    //            itemsDisplayed[inventory.Container.Items[i]].GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");

    //        }
    //        else
    //        {
    //            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
    //            obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
    //            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
    //            obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
    //            itemsDisplayed.Add(slot, obj);
    //        }
    //    }
    //}
    public void CreateSlots()
    {
        //for (int i = 0; i < inventory.Container.Items.Count; i++)
        //{
        //    InventorySlot slot = inventory.Container.Items[i];
        //    var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
        //    obj.transform.GetChild(0).GetComponentInChildren<Image>().sprite = inventory.database.GetItem[slot.item.Id].uiDisplay;
        //    obj.GetComponent<RectTransform>().localPosition = GetPosition(i);
        //    obj.GetComponentInChildren<TextMeshProUGUI>().text = slot.amount.ToString("n0");
        //    itemsDisplayed.Add(slot, obj);
        //}

        itemsDisplayed = new Dictionary<GameObject, InventorySlot>();
        for (int i = 0; i < inventory.Container.Items.Length; i++)
        {
            var obj = Instantiate(inventoryPrefab, Vector3.zero, Quaternion.identity, transform);
            obj.GetComponent<RectTransform>().localPosition = GetPosition(i);

            AddEvent(obj, EventTriggerType.PointerEnter, delegate { OnEnter(obj); });
            AddEvent(obj, EventTriggerType.PointerExit, delegate { OnExit(obj); });
            AddEvent(obj, EventTriggerType.PointerClick, delegate { OnClick(obj); });
            itemsDisplayed.Add(obj, inventory.Container.Items[i]);
        }
    }

    public Vector3 GetPosition(int i)
    {
        return new Vector3(X_START + X_SPACE_BETWEEN_ITEM * (i % NUMBER_OF_COLUMN), Y_START + (-Y_SPACE_BETWEEN_ITEM * (i / NUMBER_OF_COLUMN)), 0f);
    }

    public void AddEvent(GameObject obj, EventTriggerType type, UnityAction<BaseEventData> action)
    {
        EventTrigger trigger = obj.GetComponent<EventTrigger>();
        var eventTrigger = new EventTrigger.Entry();
        eventTrigger.eventID = type;
        eventTrigger.callback.AddListener(action);
        trigger.triggers.Add(eventTrigger);

    }

    public void OnEnter(GameObject obj)
    {

    }
    public void OnExit(GameObject obj)
    {

    }
    public void OnClick(GameObject obj)
    {
        if (itemsDisplayed[obj].item != null)
        {
            if (itemsDisplayed[obj].item.Id == 4)
            {
                playerStats.hp += bandage.restoreHealthValue;
                if (playerStats.hp > playerStats.maxHp)
                {
                    playerStats.hp = playerStats.maxHp;
                }
            }

            inventory.UseItem(itemsDisplayed[obj].item);
        }

        
    }


}
