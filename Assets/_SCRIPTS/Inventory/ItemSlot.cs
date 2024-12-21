using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image itemIcon;
    [SerializeField] private TextMeshProUGUI itemText;

    [SerializeField] protected InventoryItem item;

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemIcon.color = Color.white;

        if (item != null)
        {
            itemIcon.sprite = item.itemData.icon;
            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanSlot()
    {
        item = null;

        itemIcon.sprite = null; 
        itemIcon.color = Color.clear;
        itemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null) return;

        if(item.itemData.itemType == ItemTpye.Equipment)
            Inventory.instance.EquipItem(item.itemData);
    }
}
