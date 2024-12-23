using UnityEngine;
using UnityEngine.UI;

public class RollSkill : Skill
{
    [Header("Open SKill Roll")]
    public bool canRoll;
    [SerializeField] private UISkillTreeSlot rollSkillTreeSlot;

    [Header("Open Can Attack Roll")]
    public bool rollCanAttack;

    [SerializeField] private UISkillTreeSlot roolCanAttackSkillTreeSlot;

    public override void UseSkill()
    {
        base.UseSkill();

    }

    protected override void Start()
    {
        base.Start();

        rollSkillTreeSlot.GetComponent<Button>().onClick.AddListener(() => CanRoll());
        roolCanAttackSkillTreeSlot.GetComponent<Button>().onClick.AddListener(() => RollCanAttack());
    }
    private void CanRoll()
    {
        if(rollSkillTreeSlot.unlock)
            canRoll = true;
    }
    private void RollCanAttack()
    {
        if (roolCanAttackSkillTreeSlot.unlock)
        {
            rollCanAttack = true;
            SkillManager.instance.cloneSkill.canAttack = true;
        }
    }
    public void CloneOnRoll()
    {
        //if (canRoll)
        //    SkillManager.instance.cloneSkill.CreateClone(character.transform, Vector2.zero);
    }
    public void CreateCloneOnRollOver()
    {
        if (rollCanAttack)            
            SkillManager.instance.cloneSkill.CreateClone(character.transform, Vector2.zero);
        
    }
}
