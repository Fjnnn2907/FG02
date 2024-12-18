using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
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
        rb.velocity = _velocity;
        SetUpItemObject();
    }

    public void ItemPickUp()
    {
        Inventory.instance.AddItem(itemData);
        Destroy(gameObject);
    }
}
