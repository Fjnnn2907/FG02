using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public ItemData itemData;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Vector2 velocity;

    private void SetUpItemObject()
    {
        GetComponent<SpriteRenderer>().sprite = itemData.icon;
        gameObject.name = itemData.name + " Item";
    }

    public void SetUpItem(ItemData _itemData, Vector2 _velocity)
    {
        itemData = _itemData;
        rb.linearVelocity = _velocity;
        SetUpItemObject();
    }

    public void ItemPickUp()
    {
        if(!Inventory.instance.CanAddItem() && itemData.itemType == ItemTpye.Equipment)
        {
            rb.linearVelocity = new Vector2(0, 7);
            return;
        }

        AudioManager.instance.PlaySFX(18, transform);

        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
