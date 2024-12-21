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
        if(_item == null)
            return;

        itemNameText.text = _item.itemName;
        itemTpyeText.text = _item.itemType.ToString();
        itemDescriptions.text = _item.GetDescription();


        if (itemNameText.text.Length > 12)
            itemNameText.fontSize *= .9f;
        else
            itemNameText.fontSize = fontSizeTextDefaut;
        gameObject.SetActive(true);
    }

    public void HideToolTip()
    {
        gameObject.SetActive(false);
        itemNameText.fontSize = fontSizeTextDefaut;
    }
}
