using System.Collections;
using System.Collections.Generic;
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
    private float lastTimeUsedFlask = -Mathf.Infinity;

    [Header("Data Base")]
    public List<ItemData> itemDataBase = new List<ItemData>();
    public List<InventoryItem> loadItems = new List<InventoryItem>();
    public List<ItemEquipment> loadEquipment = new List<ItemEquipment>();

    private bool isInitialized;
    private bool hasLoadedData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            InitializeCollections();
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeCollections()
    {
        if (isInitialized)
            return;
        isInitialized = true;

        invetory = new List<InventoryItem>();
        inventoryDic = new Dictionary<ItemData, InventoryItem>();

        stash = new List<InventoryItem>();
        stashDir = new Dictionary<ItemData, InventoryItem>();

        equipment = new List<InventoryItem>();
        equipmentDir = new Dictionary<ItemEquipment, InventoryItem>();

        if (inventorySlots != null) itemSlot = inventorySlots.GetComponentsInChildren<ItemSlot>();
        if (stashSlots != null) stashSlot = stashSlots.GetComponentsInChildren<ItemSlot>();
        if (equipmentSlots != null) equipmentSlot = equipmentSlots.GetComponentsInChildren<EquipmentSlot>();
        if (statsSlots != null) statUI = statsSlots.GetComponentsInChildren<StatUI>();
    }

    private void Start()
    {
        InitializeCollections();

        if (!hasLoadedData && SaveManager.instance == null)
            AddItemStarting();
        else
            updateSlotUI();
    }

    private void AddItemStarting()
    {
        foreach (var item in loadEquipment)
        {
            if (item != null)
                EquipItem(item);
        }

        if (loadItems.Count > 0 || loadEquipment.Count > 0)
        {
            foreach (var item in loadItems)
            {
                if (item == null || item.itemData == null)
                    continue;

                for (var i = 0; i < item.stackSize; i++)
                {
                    AddItem(item.itemData);
                }
            }

            updateSlotUI();
            return;
        }

        for (int i = 0; i < startItem.Count; i++)
        {
            if (startItem[i] != null)
                AddItem(startItem[i]);
        }
    }

    private void updateSlotUI()
    {
        if (equipmentSlot != null)
        {
            for (int i = 0; i < equipmentSlot.Length; i++)
            {
                equipmentSlot[i].CleanSlot();
                foreach (var item in equipmentDir)
                {
                    if (item.Key.equipmentType == equipmentSlot[i].slotType)
                    {
                        equipmentSlot[i].UpdateSlot(item.Value);
                    }
                }
            }
        }

        if (itemSlot != null)
        {
            for (int i = 0; i < itemSlot.Length; i++)
            {
                itemSlot[i].CleanSlot();
            }

            int invCount = Mathf.Min(invetory.Count, itemSlot.Length);
            for (int i = 0; i < invCount; i++)
            {
                itemSlot[i].UpdateSlot(invetory[i]);
            }
        }

        if (stashSlot != null)
        {
            for (int i = 0; i < stashSlot.Length; i++)
            {
                stashSlot[i].CleanSlot();
            }

            int stCount = Mathf.Min(stash.Count, stashSlot.Length);
            for (int i = 0; i < stCount; i++)
            {
                stashSlot[i].UpdateSlot(stash[i]);
            }
        }

        if (statUI != null)
        {
            for (int i = 0; i < statUI.Length; i++)
            {
                statUI[i].UpdateStatValue();
            }
        }
    }

    public void EquipItem(ItemData _item)
    {
        ItemEquipment newEquipment = _item as ItemEquipment;
        if (newEquipment == null)
            return;

        InventoryItem newItem = new(newEquipment);

        ItemEquipment itemToRemove = null;

        foreach (var item in equipmentDir)
        {
            if (item.Key.equipmentType == newEquipment.equipmentType)
            {
                itemToRemove = item.Key;
            }
        }

        RemoveItem(_item);

        if (itemToRemove != null)
        {
            UnEquipment(itemToRemove);
            AddItem(itemToRemove);
        }

        equipment.Add(newItem);
        equipmentDir.Add(newEquipment, newItem);
        newEquipment.AddModifiers();

        updateSlotUI();
    }

    public void UnEquipment(ItemEquipment itemToRemove)
    {
        if (itemToRemove != null && equipmentDir.TryGetValue(itemToRemove, out InventoryItem value))
        {
            equipment.Remove(value);
            equipmentDir.Remove(itemToRemove);
            itemToRemove.RemoveModifiers();
            updateSlotUI();
        }
    }
    public bool CanAddItem()
    {
        if (itemSlot != null && invetory.Count >= itemSlot.Length)
        {
            return false;
        }
        return true;
    }
    public bool CanAddEquipment(ItemData _item)
    {
        if (_item == null)
            return false;
        if (inventoryDic != null && inventoryDic.ContainsKey(_item))
            return true;
        return CanAddItem();
    }
    public bool CanAddMaterial(ItemData _item)
    {
        if (_item == null)
            return false;
        if (stashDir != null && stashDir.ContainsKey(_item))
            return true;
        return stashSlot == null || stash.Count < stashSlot.Length;
    }
    public void AddItem(ItemData _item)
    {
        if (_item == null)
            return;

        if (_item.itemType == ItemTpye.Equipment && CanAddEquipment(_item))
        {
            AddToInventory(_item);
        }
        else if (_item.itemType == ItemTpye.Metarial && CanAddMaterial(_item))
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
        if (_item == null)
            return;

        if (_item.itemType == ItemTpye.Equipment && inventoryDic.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1)
            {
                invetory.Remove(value);
                inventoryDic.Remove(_item);
            }
            else
                value.RemoveStack();
        }
        else if (_item.itemType == ItemTpye.Metarial && stashDir.TryGetValue(_item, out InventoryItem stashValue))
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

    public bool CanCraft(ItemEquipment _itemToCraft, List<InventoryItem> _itemMaterials)
    {
        if (_itemToCraft == null || _itemMaterials == null)
            return false;

        if (!CanAddEquipment(_itemToCraft))
        {
            Debug.Log("Tui do da day, khong the che tao");
            return false;
        }

        for (int i = 0; i < _itemMaterials.Count; i++)
        {
            if (_itemMaterials[i] == null || _itemMaterials[i].itemData == null)
                continue;

            if (stashDir.TryGetValue(_itemMaterials[i].itemData, out InventoryItem value))
            {
                if (value.stackSize < _itemMaterials[i].stackSize)
                {
                    Debug.Log("khong du nguyen lieu");
                    return false;
                }
            }
            else
            {
                Debug.Log("khong du nguyen lieu");
                return false;
            }
        }

        for (int i = 0; i < _itemMaterials.Count; i++)
        {
            if (_itemMaterials[i] == null || _itemMaterials[i].itemData == null)
                continue;

            for (int k = 0; k < _itemMaterials[i].stackSize; k++)
            {
                RemoveItem(_itemMaterials[i].itemData);
            }
        }

        AddItem(_itemToCraft);
        Debug.Log("Che tao thanh cong " + _itemToCraft.name);
        return true;
    }

    public ItemEquipment GetItemEquipment(EquipmentType _type)
    {
        ItemEquipment itemEquiment = null;

        foreach (var item in equipmentDir)
        {
            if (item.Key.equipmentType == _type)
                itemEquiment = item.Key;
        }

        return itemEquiment;
    }

    public void UseFlask()
    {
        ItemEquipment currentFlask = GetItemEquipment(EquipmentType.Flask);

        if (currentFlask == null)
            return;

        bool canFlask = Time.time >= lastTimeUsedFlask + currentFlask.itemCooldown;

        if (canFlask)
        {
            currentFlask.ItemEffect(null);
            lastTimeUsedFlask = Time.time;
        }
    }

    public void LoadData(GameData _data)
    {
        InitializeCollections();
        hasLoadedData = true;

        invetory.Clear();
        inventoryDic.Clear();
        stash.Clear();
        stashDir.Clear();

        List<ItemEquipment> equippedCopy = new List<ItemEquipment>(equipmentDir.Keys);
        foreach (var eq in equippedCopy)
        {
            UnEquipment(eq);
        }

        loadItems.Clear();
        loadEquipment.Clear();

        List<ItemData> db = GetItemDataBase();

        foreach (var pair in _data.inventory)
        {
            foreach (var item in db)
            {
                if (item != null && item.itemiD == pair.Key)
                {
                    InventoryItem itemToLoad = new InventoryItem(item);
                    itemToLoad.stackSize = pair.Value;

                    loadItems.Add(itemToLoad);
                    break;
                }
            }
        }

        foreach (var loadItem in _data.equipmentID)
        {
            foreach (var item in db)
            {
                if (item != null && loadItem == item.itemiD)
                {
                    ItemEquipment eq = item as ItemEquipment;
                    if (eq != null)
                        loadEquipment.Add(eq);
                    break;
                }
            }
        }

        bool hasSavedFile = SaveManager.instance != null && SaveManager.instance.HasSaveData();
        if (hasSavedFile)
        {
            foreach (var item in loadEquipment)
            {
                if (item != null)
                    EquipItem(item);
            }

            foreach (var item in loadItems)
            {
                if (item != null && item.itemData != null)
                {
                    for (int i = 0; i < item.stackSize; i++)
                        AddItem(item.itemData);
                }
            }
            updateSlotUI();
        }
        else
        {
            AddItemStarting();
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.inventory.Clear();
        _data.equipmentID.Clear();

        foreach (var item in inventoryDic)
        {
            if (item.Key != null && !string.IsNullOrEmpty(item.Key.itemiD) && !_data.inventory.ContainsKey(item.Key.itemiD))
                _data.inventory.Add(item.Key.itemiD, item.Value.stackSize);
        }

        foreach (var item in stashDir)
        {
            if (item.Key != null && !string.IsNullOrEmpty(item.Key.itemiD) && !_data.inventory.ContainsKey(item.Key.itemiD))
                _data.inventory.Add(item.Key.itemiD, item.Value.stackSize);
        }

        foreach (var item in equipmentDir)
        {
            if (item.Key != null && !string.IsNullOrEmpty(item.Key.itemiD))
                _data.equipmentID.Add(item.Key.itemiD);
        }
    }
    private List<ItemData> GetItemDataBase()
    {
        if (itemDataBase == null || itemDataBase.Count == 0)
        {
            ItemData[] loaded = Resources.LoadAll<ItemData>("");
            if (loaded != null && loaded.Length > 0)
            {
                itemDataBase = new List<ItemData>(loaded);
            }
        }
        return itemDataBase ?? new List<ItemData>();
    }
}
