
using UnityEngine;

public enum EquipmentType
{
    Vukhi,
    Non,
    Giap,
    Quan
}


[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/Equipment")]
public class ItemEquipment: ItemData
{
    public EquipmentType equipmentType;
}
