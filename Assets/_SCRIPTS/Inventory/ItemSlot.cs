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

    protected virtual void Awake()
    {
        if (ui == null)
            ui = GetComponentInParent<UI>();
    }
    protected virtual void Start()
    {
        if (ui == null)
            ui = GetComponentInParent<UI>();
    }

    public void UpdateSlot(InventoryItem _newItem)
    {
        item = _newItem;

        if (item != null && item.itemData != null)
        {
            if (itemIcon != null)
            {
                itemIcon.color = Color.white;
                itemIcon.sprite = item.itemData.icon;
            }
            if (itemText != null)
            {
                itemText.text = item.stackSize > 1 ? item.stackSize.ToString() : "";
            }
        }
        else
        {
            CleanSlot();
        }
    }

    public void CleanSlot()
    {
        item = null;

        if (itemIcon != null)
        {
            itemIcon.sprite = null;
            itemIcon.color = Color.clear;
        }
        if (itemText != null)
            itemText.text = "";
    }
    public virtual void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.itemData == null) return;

        if (item.itemData.itemType == ItemTpye.Equipment && Inventory.instance != null)
            Inventory.instance.EquipItem(item.itemData);

        if (ui != null && ui.toolTip != null)
            ui.toolTip.HideToolTip();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null || item.itemData == null) return;

        if (ui == null)
            ui = GetComponentInParent<UI>();

        if (ui != null && ui.toolTip != null)
        {
            ui.toolTip.ShowItemToolTip(item.itemData);
            MoveUIToolTip();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ui == null)
            ui = GetComponentInParent<UI>();

        if (ui != null && ui.toolTip != null)
            ui.toolTip.HideToolTip();
    }

    private void MoveUIToolTip()
    {
        Vector2 mousePos = Input.mousePosition;

        float xOffsize = mousePos.x > Screen.width * 0.5f ? -100f : 100f;
        float yOffsize = mousePos.y > Screen.height * 0.5f ? -100f : 100f;

        ui.toolTip.transform.position = new Vector2(mousePos.x + xOffsize, mousePos.y + yOffsize);
    }


}
