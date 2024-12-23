using UnityEngine;
using UnityEngine.UI;

public class UISkillTreeSlot : MonoBehaviour
{
    public bool unlock;

    [SerializeField] private string skillName;
    [SerializeField] private string skillDescription;

    [SerializeField] private UISkillTreeSlot[] Unlocked;
    [SerializeField] protected UISkillTreeSlot[] Locked;

    [SerializeField] private Color lockedColor;

    [SerializeField] private Image skillImage;

    private void Start()
    {
        skillImage = GetComponent<Image>();

        skillImage.color = lockedColor;

        GetComponentInChildren<Button>().onClick.AddListener(() => UnlockSKillSlot());
    }

    public void UnlockSKillSlot()
    {
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

}
