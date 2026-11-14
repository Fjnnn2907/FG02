using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EquipmentSlot : ItemSlot
{
    public EquipmentType slotType;
    private void OnValidate()
    {
        gameObject.name = "Equipment Slot_" + slotType.ToString();
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.itemData == null || Inventory.instance == null)
            return;

        ItemEquipment equip = item.itemData as ItemEquipment;
        if (equip == null || !Inventory.instance.CanAddEquipment(equip))
            return;

        Inventory.instance.UnEquipment(equip);
        Inventory.instance.AddItem(equip);
        CleanSlot();

        if (ui != null && ui.toolTip != null)
            ui.toolTip.HideToolTip();
    }
}
