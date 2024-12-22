using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : ItemSlot
{
    public void SetUpCraftSlot(ItemEquipment _item)
    {
        if(_item == null)
            return;

        item.itemData = _item;

        itemIcon.sprite = item.itemData.icon;
        itemText.text = item.itemData.itemName;

        itemIcon.enabled = true;
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.craftWindow.SetUpCraftWindow(item.itemData as ItemEquipment);
    }

    private void CarftItem()
    {
        ItemEquipment craftData = item.itemData as ItemEquipment;

        Inventory.instance.CanCraft(craftData, craftData.craftMaterials);
    }
}
