using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackholeSkill : Skill
{
    [SerializeField] private GameObject backholePrefab;
    [SerializeField] private float maxSize = 12;
    [SerializeField] private float growSpeed = 3;
    [SerializeField] private float shirnkSpeed = 3;
    [SerializeField] private float backholeDuration;

    [SerializeField] private int amoutAttack = 4;
    [SerializeField] private float cloneCooldown =.3f;

    [SerializeField] private UISkillTreeSlot backholeButton;
    public bool backhole { get; private set; }

    protected override bool IsSkillUnlocked() => backhole;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        if (!backhole) return;
        base.UseSkill();

        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(3, transform);
            AudioManager.instance.PlaySFX(7, transform);
        }

        if (backholePrefab == null || character == null)
            return;

        GameObject newBackhole = Instantiate(backholePrefab, character.transform.position, Quaternion.identity);

        var newScriptBackhole = newBackhole.GetComponent<BackholeSkillController>();

        if (newScriptBackhole != null)
            newScriptBackhole.SetupBlackhole(maxSize, growSpeed, shirnkSpeed, amoutAttack, cloneCooldown, backholeDuration);
    }

    protected override void Start()
    {
        base.Start();

        if (backholeButton != null)
        {
            Button btn = backholeButton.GetComponent<Button>();
            if (btn != null)
                btn.onClick.AddListener(() => UnlocedBackhole());
        }
    }

    protected override void Update()
    {
        base.Update();
    }

    private void UnlocedBackhole()
    {
        if (backholeButton != null && backholeButton.unlock)
            backhole = true;
    }
    protected override void CheckUnlock()
    {
        UnlocedBackhole();
    }
}
