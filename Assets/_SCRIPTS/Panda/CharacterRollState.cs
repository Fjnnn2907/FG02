
using UnityEngine;

public class CharacterRollState : CharacterState
{
    public CharacterRollState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTimer = .286f;

        if (character.skill != null && character.skill.rollSkill != null)
            character.skill.rollSkill.CloneOnRoll();
    }

    public override void Exit()
    {
        base.Exit();

        if (character.skill != null && character.skill.rollSkill != null)
            character.skill.rollSkill.CreateCloneOnRollOver();
    }

    public override void Update()
    {
        base.Update();

        float currentRollSpeed = character.isVer2 ? character.rollSpeed * 1.5f : character.rollSpeed;
        character.SetVelocity(currentRollSpeed * character.facing, character.rb.linearVelocity.y);

        if (startTimer <= 0)
            stateMachine.ChangeState(character.IsGroundCheck() ? character.idleState : character.airState);
    }
}
