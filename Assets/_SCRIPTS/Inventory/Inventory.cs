using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;


public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public List<InventoryItem> equipment;
    public Dictionary<ItemEquipment, InventoryItem> equipmentDir;

    public List<InventoryItem> invetory;
    public Dictionary<ItemData, InventoryItem> inventoryDic;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDir;


    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlots;
    [SerializeField] private Transform stashSlots;


    private ItemSlot[] itemSlot;
    private ItemSlot[] stashSlot;
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    private void Start()
    {
        invetory = new List<InventoryItem>();
        inventoryDic = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDir = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDir = new Dictionary<ItemEquipment, InventoryItem>();

        itemSlot = inventorySlots.GetComponentsInChildren<ItemSlot>();
        stashSlot = stashSlots.GetComponentsInChildren<ItemSlot>();
    }

    private void updateSlotUI()
    {
        for(int i = 0; i < invetory.Count; i++)
        {
            itemSlot[i].UpdateSlot(invetory[i]);
        }

        for(int i = 0;i < stash.Count; i++)
        {
            stashSlot[i].UpdateSlot(stash[i]);
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemEquipment newEquipment = _item as ItemEquipment;
        InventoryItem newItem = new(newEquipment);

        ItemEquipment itemToRemove = null;

        foreach (var item in equipmentDir)
        {
            if (item.Key.itemType == newEquipment.itemType)
            {
                itemToRemove = item.Key;
            }
        }

        if(itemToRemove != null)
            UnEquipment(itemToRemove);

        equipment.Add(newItem);
        equipmentDir.Add(newEquipment, newItem);


        //updateSlotUI();
    }

    private void UnEquipment(ItemEquipment itemToRemove)
    {
        if (equipmentDir.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDir.Remove(itemToRemove);
        }
    }

    public void AddItem(ItemData _item)
    {
        if(_item.itemType == ItemTpye.Equipment)
        {
            AddToInventory(_item);
        }
        else if(_item.itemType == ItemTpye.Metarial)
        {
            AddToStash(_item);
        }

        updateSlotUI();
    }

    private void AddToStash(ItemData _item)
    {
        if (stashDir.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            stash.Add(newItem);
            stashDir.Add(_item, newItem);
        }
    }

    private void AddToInventory(ItemData _item)
    {
        if (inventoryDic.TryGetValue(_item, out InventoryItem value))
        {
            value.AddStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            invetory.Add(newItem);
            inventoryDic.Add(_item, newItem);
        }
    }

    public void RemoveItem(ItemData _item)
    {
        if(inventoryDic.TryGetValue(_item,out InventoryItem value))
        {
            if(value.stackSize <= 1)
            {
                invetory.Remove(value);
                inventoryDic.Remove(_item);
            }
            else
                value.RemoveStack();
        }

        if (stashDir.TryGetValue(_item, out InventoryItem stashValue))
        {
            if (stashValue.stackSize <= 1)
            {
                stash.Remove(stashValue);
                stashDir.Remove(_item);
            }
            else
                stashValue.RemoveStack();
        }


        updateSlotUI();
    }
}
