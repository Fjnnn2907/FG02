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
        GetComponentInChildren<Button>().onClick.AddListener(() => UnlockSKillSlot());
    }
    private void Start()
    {
        skillImage = GetComponent<Image>();
        ui = GetComponentInParent<UI>();
        skillImage.color = lockedColor;

        if(unlock)
            skillImage.color = Color.white;

    }

    public void UnlockSKillSlot()
    {

        if(!PlayerManager.instance.HaveMoney(countBuySkill))
            return;

        for (int i = 0; i < Unlocked.Length; i++)
        {
            if (Unlocked[i].unlock == false)
            {
                Debug.Log("mo cong skill");
                return;
            }
        }

        for (int i = 0;i < Locked.Length; i++)
        {
            if (Locked[i].unlock == true)
            {
                Debug.Log("khong the cong skill");
                return;
            }
        }

        unlock = true;
        skillImage.color = Color.white;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ui.toolTipSkillTree.ShowToolTip(skillName, skillDescription);

        //MoiveUIToolTip();
    }

    private void MoiveUIToolTip()
    {
        Vector2 mousePos = Input.mousePosition;

        float xOffsize = 0;
        float yOffsize = 0;

        if (mousePos.x > 600)
            xOffsize = 150;
        else
            xOffsize = -150;

        if (mousePos.y > 350)
            yOffsize = 150;
        else
            yOffsize = -150;

        ui.toolTipSkillTree.transform.position = new Vector2(mousePos.x + xOffsize, mousePos.y + yOffsize);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ui.toolTipSkillTree.HideToolTip();
    }

    public void LoadData(GameData _data)
    {
        if(_data.skillTree.TryGetValue(skillName,out bool value))
        {
            unlock = value;
        }
    }

    public void SaveData(ref GameData _data)
    {

        if (_data.skillTree.TryGetValue(skillName, out bool value))
        {
            _data.skillTree.Remove(skillName);
            _data.skillTree.Add(skillName, value);

        }
        else
            _data.skillTree.Add(skillName, unlock);
    }
}
