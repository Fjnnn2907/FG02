using UnityEngine;

public class RollSkill : Skill
{
    public override void UseSkill()
    {
        base.UseSkill();

        Debug.Log("useKill");
    }
}
