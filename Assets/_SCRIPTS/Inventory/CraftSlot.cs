using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : ItemSlot
{
    public void SetUpCraftSlot(ItemEquipment _item)
    {
        if (_item == null)
            return;

        if (item == null)
            item = new InventoryItem(_item);
        else
            item.itemData = _item;

        if (itemIcon != null)
        {
            itemIcon.sprite = item.itemData.icon;
            itemIcon.color = Color.white;
            itemIcon.enabled = true;
        }
        if (itemText != null)
            itemText.text = item.itemData.itemName;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (ui == null)
            ui = GetComponentInParent<UI>();

        if (ui != null && ui.craftWindow != null && item != null)
            ui.craftWindow.SetUpCraftWindow(item.itemData as ItemEquipment);
    }
}
