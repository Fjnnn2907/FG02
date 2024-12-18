using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CraftSlot : ItemSlot
{
    private void OnEnable()
    {
        UpdateSlot(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemEquipment craftData = item.itemData as ItemEquipment;

        Inventory.instance.CanCraft(craftData,craftData.craftMaterials);
    }
}
