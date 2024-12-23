using UnityEngine;

public class CharacterGroundState : CharacterState
{
    public CharacterGroundState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (!character.IsGroundCheck())
            stateMachine.ChangeState(character.airState);

        if (Input.GetKeyDown(KeyCode.Space) && character.IsGroundCheck())
            stateMachine.ChangeState(character.jumpState);

        if (Input.GetKeyDown(KeyCode.Mouse0) && character.IsGroundCheck())
            stateMachine.ChangeState(character.attackState);

        if (Input.GetKeyDown(KeyCode.H) && character.IsGroundCheck())
        {
            if (character.isVer2) return;
            stateMachine.ChangeState(character.changeState);
        }
        // add singleton Skill
        if (Input.GetKeyDown(KeyCode.LeftShift) && character.IsGroundCheck() && character.skill.rollSkill.CanUseSkill() && character.skill.rollSkill.canRoll)
            stateMachine.ChangeState(character.rollState);

        if (Input.GetKeyDown(KeyCode.I) && character.IsGroundCheck())
            stateMachine.ChangeState(character.jabState);

        //if (Input.GetKeyDown(KeyCode.Q) && character.IsGroundCheck())
        //    stateMachine.ChangeState(character.counterAttackState);

        if (Input.GetKeyDown(KeyCode.Mouse1) && HeNoSword() && character.skill.swordSkill.unlocedSowrd)
            stateMachine.ChangeState(character.aimSwordState);

        if (Input.GetKeyDown(KeyCode.R))
            stateMachine.ChangeState(character.backholeState);
    }

    private bool HeNoSword()
    {
        if(!character.sword)
            return true;

        character.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}
