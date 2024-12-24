using System.Text;
using UnityEditor;
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
    public string itemiD;

    [Range(0f, 100f)]
    public float dropChane;

    protected StringBuilder sb = new StringBuilder();

    private void OnValidate()
    {
#if UNITY_EDITOR
        string path = AssetDatabase.GetAssetPath(this);
        itemiD = AssetDatabase.AssetPathToGUID(path);
#endif
    }

    public virtual string GetDescription()
    {
        return "";
    }
}
