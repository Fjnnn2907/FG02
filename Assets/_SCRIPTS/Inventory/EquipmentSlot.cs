using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using static UnityEditor.Progress;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType slotType;
    private void OnValidate()
    {
        gameObject.name = "Equipment Slot_" + slotType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if(item == null || item.itemData == null) 
            return;

        Inventory.instance.UnEquipment(item.itemData as ItemEquipment);
        Inventory.instance.AddItem(item.itemData as ItemEquipment);
        CleanSlot();

    }
}
