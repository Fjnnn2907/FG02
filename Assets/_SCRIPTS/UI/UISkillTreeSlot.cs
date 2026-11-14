using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UISkillTreeSlot : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler,ISaveManager
{
    private Image skillImage;
    private UI ui;

    public bool unlock;

    [SerializeField] private string skillName;
    [TextArea(2,2)]
    [SerializeField] private string skillDescription;
    [SerializeField] private int countBuySkill;


    [SerializeField] private UISkillTreeSlot[] Unlocked;
    [SerializeField] protected UISkillTreeSlot[] Locked;

    [SerializeField] private Color lockedColor;

    private void Awake()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();
        Button btn = GetComponentInChildren<Button>();
        if (btn != null)
            btn.onClick.AddListener(() => UnlockSKillSlot());
    }
    private void Start()
    {
        if (skillImage == null)
            skillImage = GetComponent<Image>();
        if (ui == null)
            ui = GetComponentInParent<UI>();

        UpdateSlotVisual();
    }

    private void UpdateSlotVisual()
    {
        if (skillImage != null)
            skillImage.color = unlock ? Color.white : lockedColor;
    }

    public void UnlockSKillSlot()
    {
        if (unlock)
            return;

        if (PlayerManager.instance == null || !PlayerManager.instance.CanAfford(countBuySkill))
            return;

        if (Unlocked != null)
        {
            for (int i = 0; i < Unlocked.Length; i++)
            {
                if (Unlocked[i] != null && Unlocked[i].unlock == false)
                {
                    Debug.Log("mo cong skill");
                    return;
                }
            }
        }

        if (Locked != null)
        {
            for (int i = 0; i < Locked.Length; i++)
            {
                if (Locked[i] != null && Locked[i].unlock == true)
                {
                    Debug.Log("khong the cong skill");
                    return;
                }
            }
        }

        if (!PlayerManager.instance.HaveMoney(countBuySkill))
            return;

        unlock = true;
        UpdateSlotVisual();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (ui == null)
            ui = GetComponentInParent<UI>();
        if (ui != null && ui.toolTipSkillTree != null)
            ui.toolTipSkillTree.ShowToolTip(skillName, skillDescription);
    }

    private void MoiveUIToolTip()
    {
        Vector2 mousePos = Input.mousePosition;

        float xOffsize = mousePos.x > Screen.width * 0.5f ? -150f : 150f;
        float yOffsize = mousePos.y > Screen.height * 0.5f ? -150f : 150f;

        if (ui != null && ui.toolTipSkillTree != null)
            ui.toolTipSkillTree.transform.position = new Vector2(mousePos.x + xOffsize, mousePos.y + yOffsize);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (ui == null)
            ui = GetComponentInParent<UI>();
        if (ui != null && ui.toolTipSkillTree != null)
            ui.toolTipSkillTree.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if (!string.IsNullOrEmpty(skillName) && _data.skillTree.TryGetValue(skillName, out bool value))
        {
            unlock = value;
        }
        UpdateSlotVisual();
    }

    public void SaveData(ref GameData _data)
    {
        if (string.IsNullOrEmpty(skillName))
            return;

        if (_data.skillTree.ContainsKey(skillName))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, unlock);
        }
        else
            _data.skillTree.Add(skillName, unlock);
    }
}
