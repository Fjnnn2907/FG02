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

    protected override bool IsSkillUnlocked() => canRoll;

    protected override void Start()
    {
        base.Start();

        if (rollSkillTreeSlot != null)
            rollSkillTreeSlot.GetComponent<Button>()?.onClick.AddListener(() => CanRoll());
        if (roolCanAttackSkillTreeSlot != null)
            roolCanAttackSkillTreeSlot.GetComponent<Button>()?.onClick.AddListener(() => RollCanAttack());
    }
    #region Skill Tree
    protected override void CheckUnlock()
    {
        CanRoll();
        RollCanAttack();
    }
    private void CanRoll()
    {
        if (rollSkillTreeSlot != null && rollSkillTreeSlot.unlock)
            canRoll = true;
    }
    private void RollCanAttack()
    {
        if (roolCanAttackSkillTreeSlot != null && roolCanAttackSkillTreeSlot.unlock)
            rollCanAttack = true;
    }
    public void CloneOnRoll()
    {
    }
    public void CreateCloneOnRollOver()
    {
        if (rollCanAttack && SkillManager.instance != null && SkillManager.instance.cloneSkill != null && character != null)
            SkillManager.instance.cloneSkill.CreateClone(character.transform, Vector2.zero);
    }
    #endregion
}
