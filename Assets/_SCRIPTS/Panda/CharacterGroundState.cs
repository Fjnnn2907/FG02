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
        {
            stateMachine.ChangeState(character.airState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && character.IsGroundCheck())
        {
            stateMachine.ChangeState(character.jumpState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse0) && character.IsGroundCheck())
        {
            stateMachine.ChangeState(character.attackState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.H) && character.IsGroundCheck())
        {
            if (character.isVer2) return;
            stateMachine.ChangeState(character.changeState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && character.IsGroundCheck() && character.skill != null && character.skill.rollSkill != null && character.skill.rollSkill.canRoll && character.skill.rollSkill.CanUseSkill())
        {
            stateMachine.ChangeState(character.rollState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.I) && character.IsGroundCheck())
        {
            stateMachine.ChangeState(character.jabState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.Mouse1) && character.skill != null && character.skill.swordSkill != null && character.skill.swordSkill.unlocedSowrd && HeNoSword())
        {
            stateMachine.ChangeState(character.aimSwordState);
            return;
        }

        if (Input.GetKeyDown(KeyCode.R) && character.skill != null && character.skill.backholiSkill != null && character.skill.backholiSkill.backhole)
        {
            if (character.skill.backholiSkill.cooldownTimer > 0)
                return;
            stateMachine.ChangeState(character.backholeState);
            return;
        }
    }

    private bool HeNoSword()
    {
        if(!character.sword)
            return true;

        character.sword.GetComponent<SwordSkillController>().ReturnSword();
        return false;
    }
}
