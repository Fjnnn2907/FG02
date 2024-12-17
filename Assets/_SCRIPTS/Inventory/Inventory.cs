using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> inventoryItems;
    public Dictionary<ItemData, InventoryItem> inventoryDic;

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlots;
    private ItemSlot[] itemSlot;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        inventoryItems = new List<InventoryItem>();
        inventoryDic = new Dictionary<ItemData, InventoryItem>();

        itemSlot = inventorySlots.GetComponentsInChildren<ItemSlot>();
    }

    private void updateSlotUI()
    {
        for(int i = 0; i < inventoryItems.Count; i++)
        {
            itemSlot[i].UpdateSlot(inventoryItems[i]);
        }
    }

    public void AddItem(ItemData _item)
    {
        if(inventoryDic.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            inventoryItems.Add(newItem);
            inventoryDic.Add(_item, newItem);
        }

        updateSlotUI();
    }
    public void RemoveItem(ItemData _item)
    {
        if(inventoryDic.TryGetValue(_item,out InventoryItem value))
        {
            if(value.stackSize <= 1)
            {
                inventoryItems.Remove(value);
                inventoryDic.Remove(_item);
            }
            else
                value.RemoveStack();
        }

        updateSlotUI();
    }
}
