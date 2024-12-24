using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IPointerDownHandler,IPointerEnterHandler,IPointerExitHandler
{
    [SerializeField] protected Image itemIcon;
    [SerializeField] protected TextMeshProUGUI itemText;

    [SerializeField] protected InventoryItem item;

    [SerializeField] protected UI ui;

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;
        itemIcon.color = Color.white;

        if (item != null)
        {
            itemIcon.sprite = item.itemData.icon;
            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void CleanSlot()
    {
        item = null;

        itemIcon.sprite = null; 
        itemIcon.color = Color.clear;
        itemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if(item == null) return;

        if(item.itemData.itemType == ItemTpye.Equipment)
            Inventory.instance.EquipItem(item.itemData);

        ui.toolTip.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(item == null) return;
        ui.toolTip.ShowToolTip(item.itemData as ItemEquipment);

        MoveUIToolTip();


    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null) return;
        ui.toolTip.HideToolTip();
    }

    private void MoveUIToolTip()
    {
        Vector2 mousePos = Input.mousePosition;


        float xOffsize = 0;
        float yOffsize = 0;

        if (mousePos.x > 960)
            xOffsize = -100;
        else
            xOffsize = 100;

        if (mousePos.y > 540)
            yOffsize = -100;
        else
            yOffsize = 100;

        ui.toolTip.transform.position = new Vector2(mousePos.x + xOffsize, mousePos.y + yOffsize);
    }


}
