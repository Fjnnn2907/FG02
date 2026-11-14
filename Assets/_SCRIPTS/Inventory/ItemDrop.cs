using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{
    [SerializeField] private int amoutDrop;
    [SerializeField] private ItemData[] possibleDrop;
    [SerializeField] private List<ItemData> listDrop = new();
    
    
    [SerializeField] private GameObject itemPrefab;


    public void GrenerateDrop()
    {
        if (possibleDrop == null || possibleDrop.Length == 0 || itemPrefab == null)
            return;

        listDrop.Clear();

        for (int i = 0; i < possibleDrop.Length; i++)
        {
            if (possibleDrop[i] != null && Random.Range(0f, 100f) < possibleDrop[i].dropChane)
            {
                listDrop.Add(possibleDrop[i]);
            }
        }

        if (listDrop.Count == 0)
            return;

        int dropsToSpawn = Mathf.Min(amoutDrop, listDrop.Count);
        for (int i = 0; i < dropsToSpawn; i++)
        {
            int randomIndex = Random.Range(0, listDrop.Count);
            var randomItem = listDrop[randomIndex];

            listDrop.RemoveAt(randomIndex);
            DropItem(randomItem);
        }
    }

    public void DropItem(ItemData _item)
    {
        if (_item == null || itemPrefab == null)
            return;

        var newItem = Instantiate(itemPrefab, transform.position, Quaternion.identity);

        Vector2 randomVelocity = new Vector2(Random.Range(-5f, 5f), Random.Range(10f, 13f));

        newItem.GetComponent<ItemObject>()?.SetUpItem(_item, randomVelocity);
    }
}
