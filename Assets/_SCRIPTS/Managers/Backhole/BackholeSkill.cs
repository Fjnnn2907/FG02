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

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        if (!backhole) return;
        base.UseSkill();

        GameObject newBackhole = Instantiate(backholePrefab,character.transform.position,Quaternion.identity);

        var newScriptBackhole = newBackhole.GetComponent<BackholeSkillController>();

        newScriptBackhole.SetupBlackhole(maxSize,growSpeed,shirnkSpeed,amoutAttack,cloneCooldown, backholeDuration);
    }

    protected override void Start()
    {
        base.Start();

        backholeButton.GetComponent<Button>().onClick.AddListener(() => UnlocedBackhole());
    }

    protected override void Update()
    {
        base.Update();
    }

    private void UnlocedBackhole()
    {
        if(backholeButton.unlock)
            backhole = true;
    }
}
