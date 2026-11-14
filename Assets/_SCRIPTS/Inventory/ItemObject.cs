using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData itemData;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 velocity;

    private bool isPickedUp;

    private void Start()
    {
        if (itemData != null)
            SetUpItemObject();
    }

    private void SetUpItemObject()
    {
        if (itemData == null)
            return;

        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
            sr.sprite = itemData.icon;
        gameObject.name = itemData.name + " Item";
    }

    public void SetUpItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        if (rb != null)
            rb.linearVelocity = _velocity;
        SetUpItemObject();
    }

    public void ItemPickUp()
    {
        if (isPickedUp || itemData == null || Inventory.instance == null)
            return;

        if (itemData.itemType == ItemTpye.Equipment && !Inventory.instance.CanAddEquipment(itemData))
        {
            if (rb != null)
                rb.linearVelocity = new Vector2(0, 7);
            return;
        }
        if (itemData.itemType == ItemTpye.Metarial && !Inventory.instance.CanAddMaterial(itemData))
        {
            if (rb != null)
                rb.linearVelocity = new Vector2(0, 7);
            return;
        }

        isPickedUp = true;

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(18, transform);

        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
