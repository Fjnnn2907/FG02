using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_ToolTip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemNameText;
    [SerializeField] private TextMeshProUGUI itemTpyeText;
    [SerializeField] private TextMeshProUGUI itemDescriptions;
    [SerializeField] private float fontSizeTextDefaut = 35;
    public void ShowToolTip(ItemEquipment _item)
    {
        ShowItemToolTip(_item);
    }

    public void ShowItemToolTip(ItemData _item)
    {
        if (_item == null)
            return;

        if (itemNameText != null)
        {
            itemNameText.text = _item.itemName;
            itemNameText.fontSize = itemNameText.text != null && itemNameText.text.Length > 12
                ? fontSizeTextDefaut * .9f
                : fontSizeTextDefaut;
        }

        if (itemTpyeText != null)
            itemTpyeText.text = _item.itemType.ToString();

        if (itemDescriptions != null)
            itemDescriptions.text = _item.GetDescription();

        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        if (itemNameText != null)
            itemNameText.fontSize = fontSizeTextDefaut;
    }
}
