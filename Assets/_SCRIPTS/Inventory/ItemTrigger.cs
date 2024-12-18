using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    [SerializeField] private ItemObject itemObject => GetComponentInParent<ItemObject>();
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Character>() != null)
        {
            itemObject.ItemPickUp();
        }
    }
}
