using System.Text;
using UnityEngine;

public enum ItemTpye
{
    Metarial,
    Equipment
}


[CreateAssetMenu(fileName = "new ItemData", menuName = "Data/Item")]
public class ItemData : ScriptableObject
{
    public ItemTpye itemType;
    public string itemName;
    public Sprite icon;

    [Range(0f, 100f)]
    public float dropChane;

    protected StringBuilder sb = new StringBuilder();

    public virtual string GetDescription()
    {
        return "";
    }
}
