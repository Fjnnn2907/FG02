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
        cratfButton.onClick.RemoveAllListeners();

        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color =  Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0;i < _item.craftMaterials.Count; i++)
        {
            if (_item.craftMaterials.Count > materialImage.Length)
                Debug.Log("Khong du slot UI de chua nguyen lieu");

            materialImage[i].sprite = _item.craftMaterials[i].itemData.icon;
            materialImage[i].color = Color.white;

            var materialSlotText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();

            materialSlotText.text = _item.craftMaterials[i].stackSize.ToString();
            materialSlotText.color = Color.white;
        }

        itemIcon.color = Color.white;
        itemIcon.sprite = _item.icon;
        itemName.text = _item.itemName;
        itemDescription.text = _item.GetDescription();

        cratfButton.onClick.AddListener(() => Inventory.instance.CanCraft(_item,_item.craftMaterials));

    }
}
