using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;

    [SerializeField] private Button cratfButton;

    [SerializeField] private Image[] materialImage;

    public void SetUpCraftWindow(ItemEquipment _item)
    {
        if (_item == null || cratfButton == null)
            return;

        cratfButton.onClick.RemoveAllListeners();

        if (materialImage != null)
        {
            for (int i = 0; i < materialImage.Length; i++)
            {
                if (materialImage[i] == null) continue;
                materialImage[i].color = Color.clear;
                var txt = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();
                if (txt != null)
                    txt.color = Color.clear;
            }

            if (_item.craftMaterials != null)
            {
                if (_item.craftMaterials.Count > materialImage.Length)
                    Debug.LogWarning("Khong du slot UI de chua nguyen lieu");

                int count = Mathf.Min(_item.craftMaterials.Count, materialImage.Length);
                for (int i = 0; i < count; i++)
                {
                    if (_item.craftMaterials[i] == null || _item.craftMaterials[i].itemData == null || materialImage[i] == null)
                        continue;

                    materialImage[i].sprite = _item.craftMaterials[i].itemData.icon;
                    materialImage[i].color = Color.white;

                    var materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();
                    if (materialSlotText != null)
                    {
                        materialSlotText.text = _item.craftMaterials[i].stackSize.ToString();
                        materialSlotText.color = Color.white;
                    }
                }
            }
        }

        if (itemIcon != null)
        {
            itemIcon.color = Color.white;
            itemIcon.sprite = _item.icon;
        }
        if (itemName != null)
            itemName.text = _item.itemName;
        if (itemDescription != null)
            itemDescription.text = _item.GetDescription();

        cratfButton.onClick.AddListener(() =>
        {
            if (Inventory.instance != null)
                Inventory.instance.CanCraft(_item, _item.craftMaterials);
        });
    }
}
