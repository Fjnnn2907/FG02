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
        for(int i = 0; i < possibleDrop.Length; i++)
        {
            if(Random.Range(0,100) < possibleDrop[i].dropChane)
            {
                listDrop.Add(possibleDrop[i]);
            }
        }

        for(int i = 0;i < amoutDrop; i++)
        {
            var randomItem = listDrop[Random.Range(0,listDrop.Count -1)];

            listDrop.Remove(randomItem);
            DropItem(randomItem);
        }
    }

    public void DropItem(ItemData _item)
    {
        var newItem = Instantiate(itemPrefab,transform.position,Quaternion.identity);
        
        Vector2 randomVelocity = new Vector2(Random.Range(-5,5),Random.Range(10,13));


        newItem.GetComponent<ItemObject>().SetUpItem(_item, randomVelocity);
    }
}
