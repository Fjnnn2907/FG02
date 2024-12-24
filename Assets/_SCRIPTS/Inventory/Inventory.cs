using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;


public class Inventory : MonoBehaviour, ISaveManager
{
    public static Inventory instance;

    public List<InventoryItem> equipment;
    public Dictionary<ItemEquipment, InventoryItem> equipmentDir;

    public List<InventoryItem> invetory;
    public Dictionary<ItemData, InventoryItem> inventoryDic;

    public List<InventoryItem> stash;
    public Dictionary<ItemData, InventoryItem> stashDir;


    public List<ItemData> startItem = new();

    [Header("Inventory UI")]
    [SerializeField] private Transform inventorySlots;
    [SerializeField] private Transform stashSlots;
    [SerializeField] private Transform equipmentSlots;
    [SerializeField] private Transform statsSlots;

    private ItemSlot[] itemSlot;
    private ItemSlot[] stashSlot;
    private EquipmentSlot[] equipmentSlot;
    private StatUI[] statUI;

    [Header("Cooldown")]
    private float lastTimeUsedFlask;

    [Header("Data Base")]
    public List<InventoryItem> loadItems;
    public List<ItemEquipment> loadEquipment;
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
        equipmentSlot = equipmentSlots.GetComponentsInChildren<EquipmentSlot>();
        statUI = statsSlots.GetComponentsInChildren<StatUI>();
        
        AddItemStarting();
    }

    private void AddItemStarting()
    {
        foreach(var item in loadEquipment)
        {
            EquipItem(item);
        }


        if(loadItems.Count > 0)
        {
            foreach (var item in loadItems)
            {
                for (var i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.itemData);
                }
            }

            return;
        }
        

        for (int i = 0; i < startItem.Count; i++)
        {
            AddItem(startItem[i]);
        }
    }

    private void updateSlotUI()
    {

        for(int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach(var item in equipmentDir)
            {
                if(item.Key.equipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].UpdateSlot(item.Value);
                }
            }
        }

        for (int i = 0; i < itemSlot.Length; i++)
        {
            itemSlot[i].CleanSlot();
        }

        for (int i = 0; i < stashSlot.Length; i++)
        {
            stashSlot[i].CleanSlot();
        }



        for (int i = 0; i < invetory.Count; i++)
        {
            itemSlot[i].UpdateSlot(invetory[i]);
        }


        for(int i = 0;i < stash.Count; i++)
        {
            stashSlot[i].UpdateSlot(stash[i]);
        }

        for(int i = 0; i < statUI.Length; i++)
        {
            statUI[i].UpdateStatValue();
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemEquipment newEquipment = _item as ItemEquipment;
        InventoryItem newItem = new(newEquipment);

        ItemEquipment itemToRemove = null;

        foreach (var item in equipmentDir)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                itemToRemove = item.Key;
            }
        }

        if(itemToRemove != null)
        {
            UnEquipment(itemToRemove);
            AddItem(itemToRemove);
        }

        equipment.Add(newItem);
        equipmentDir.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        RemoveItem(_item);

        updateSlotUI();
    }

    public void UnEquipment(ItemEquipment itemToRemove)
    {
        if (equipmentDir.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDir.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
        }
    }
    public bool CanAddItem()
    {
        if(invetory.Count >= itemSlot.Length)
        {
            Debug.Log("Full Do");
            return false;
        }
        return true;
    }
    public void AddItem(ItemData _item)
    {
        if(_item.itemType == ItemTpye.Equipment && CanAddItem())
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

    public bool CanCraft(ItemEquipment _itemToCraft,List<InventoryItem> _itemMaterials)
    {
        List<InventoryItem> itemMaterialsToRemove = new List<InventoryItem>();

        for (int i = 0; i < _itemMaterials.Count; i++)
        {
            if (stashDir.TryGetValue(_itemMaterials[i].itemData, out InventoryItem value))
            {
                if(value.stackSize < _itemMaterials[i].stackSize)
                {
                    Debug.Log("khong du nguyen lieu");
                    return false;
                }
                else
                {
                    itemMaterialsToRemove.Add(value);
                }
            }
            else
            {
                Debug.Log("khong du nguyen lieu");
                return false;
            }
        }

        for(int i = 0; i < itemMaterialsToRemove.Count; i++)
        {
            RemoveItem(_itemMaterials[i].itemData);
        }
        
        AddItem(_itemToCraft);
        Debug.Log("Che tao thanh cong" +  _itemToCraft.name);
        return true;
    }

    public ItemEquipment GetItemEquipment(EquipmentType _type)
    {
        ItemEquipment itemEquiment = null;
        
        foreach(var item in equipmentDir)
        {
            if(item.Key.equipmentType == _type)
                itemEquiment = item.Key;
        }


        return itemEquiment;
    }

    public void UseFlask()
    {
        ItemEquipment currentFlask = GetItemEquipment(EquipmentType.Flask);

        if(currentFlask == null) 
            return;

        bool canFlask = Time.time > lastTimeUsedFlask + currentFlask.itemCooldown;

        if (canFlask)
        {
            currentFlask.ItemEffect(null);
            lastTimeUsedFlask = Time.time;
        }
    }

    public void LoadData(GameData _data)
    {
        foreach(var pair in _data.inventory)
        {
            foreach(var item in GetItemDataBase())
            {
                if(item != null && item.itemiD == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadItems.Add(itemToLoad);
                }
            }
        }

        foreach(var loadItem in _data.equipmentID)
        {
            foreach (var item in GetItemDataBase())
            {
                if(item != null && loadItem == item.itemiD)
                {
                    loadEquipment.Add(item as ItemEquipment);
                }
            }
        }
        
        
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        
        foreach(var item in inventoryDic)
        {
            _data.inventory.Add(item.Key.itemiD, item.Value.stackSize);
        }

        foreach(var item in stashDir)
        {
            _data.inventory.Add(item.Key.itemiD, item.Value.stackSize);
        }

        foreach(var item in equipmentDir)
        {
            _data.equipmentID.Add(item.Key.itemiD);
        }
    }
    private List<ItemData> GetItemDataBase()
    {
        List<ItemData> itemDataBase = new List<ItemData>();
        string[] assetNames = AssetDatabase.FindAssets("", new[] { "Assets/Data" });

        foreach(var asset in assetNames)
        {
            var SOpath = AssetDatabase.GUIDToAssetPath(asset);
            var itemdata = AssetDatabase.LoadAssetAtPath<ItemData>(SOpath);
            itemDataBase.Add(itemdata);
        }
        return itemDataBase;
    }
}
