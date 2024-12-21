using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICraftList : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private GameObject craftPreFab;

    [SerializeField] private List<ItemEquipment> craftEquiment;
    [SerializeField] private List<CraftSlot> craftSlots;

    private void Start()
    {
        CraftSlots();
    }

    private void CraftSlots()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
            craftSlots.Add(craftSlotParent.GetChild(i).GetComponent<CraftSlot>());
        }
    }

    public void SetUpCraftList()
    {
        for(int i = 0; i < craftSlots.Count; i++)
        {
            craftSlots[i].gameObject.SetActive(false);
        }

        craftSlots = new List<CraftSlot>();

        for(int i = 0;i < craftEquiment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftPreFab, craftSlotParent);

            newSlot.GetComponent<CraftSlot>().SetUpCraftSlot(craftEquiment[i]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetUpCraftList();
    }
}
