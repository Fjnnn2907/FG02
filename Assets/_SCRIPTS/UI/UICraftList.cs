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
        if (transform.GetSiblingIndex() == 0)
        {
            SetUpCraftList();
            SetUpDefaultCartfWindow();
        }
    }

    public void SetUpCraftList()
    {
        if (craftSlotParent == null || craftPreFab == null || craftEquiment == null)
            return;

        for (int i = craftSlotParent.childCount - 1; i >= 0; i--)
        {
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }

        for (int i = 0; i < craftEquiment.Count; i++)
        {
            GameObject newSlot = Instantiate(craftPreFab, craftSlotParent);

            newSlot.GetComponent<CraftSlot>()?.SetUpCraftSlot(craftEquiment[i]);
        }
    }

    public void SetUpDefaultCartfWindow()
    {
        if (craftEquiment != null && craftEquiment.Count > 0 && craftEquiment[0] != null)
        {
            UI parentUi = GetComponentInParent<UI>();
            if (parentUi != null && parentUi.craftWindow != null)
                parentUi.craftWindow.SetUpCraftWindow(craftEquiment[0]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        SetUpCraftList();
    }
}
