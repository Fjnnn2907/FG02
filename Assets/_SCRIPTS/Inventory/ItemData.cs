using System.Text;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

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
