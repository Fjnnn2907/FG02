using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackholeSkill : Skill
{
    [SerializeField] private GameObject backholePrefab;
    [SerializeField] private float maxSize = 12;
    [SerializeField] private float growSpeed = 3;
    [SerializeField] private float shirnkSpeed = 3;
    
    [SerializeField] private int amoutAttack = 4;
    [SerializeField] private float cloneCooldown =.3f;
    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();

        GameObject newBackhole = Instantiate(backholePrefab,character.transform.position,Quaternion.identity);

        var newScriptBackhole = newBackhole.GetComponent<BackholeSkillController>();

        newScriptBackhole.SetupBlackhole(maxSize,growSpeed,shirnkSpeed,amoutAttack,cloneCooldown);
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
