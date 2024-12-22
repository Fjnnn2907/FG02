using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICraftList : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private GameObject craftPreFab;

    [SerializeField] private List<ItemEquipment> craftEquiment;

    private void Start()
    {
        transform.parent.GetChild(0).GetComponent<UICraftList>().SetUpCraftList();
        SetUpDefaultCartfWindow();
    }


    public void SetUpCraftList()
    {
        for(int i = 0; i < craftSlotParent.childCount; i++)
        {
            craftSlotParent.GetChild(i).gameObject.SetActive(false);
        }

        for(int i = 0;i < craftEquiment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftPreFab, craftSlotParent);

            newSlot.GetComponent<CraftSlot>().SetUpCraftSlot(craftEquiment[i]);
        }
    }

    public void SetUpDefaultCartfWindow()
    {
        if (craftEquiment[0] != null)
            GetComponentInParent<UI>().craftWindow.SetUpCraftWindow(craftEquiment[0]);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetUpCraftList();
    }
}
