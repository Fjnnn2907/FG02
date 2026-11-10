
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

        character.skill.rollSkill.CloneOnRoll();
        character.skill.rollSkill.CreateCloneOnRollOver();
    }

    public override void Exit()
    {
        base.Exit();

        //character.skill.cloneSkill.CreateCloneOnRollOver();
    }

    public override void Update()
    {
        base.Update();

        character.SetVelocity(character.rollSpeed * character.facing,character.rb.linearVelocity.y);

        if (character.isVer2)
            character.SetVelocity(15 * character.facing, character.rb.linearVelocity.y);

        if (startTimer <= 0)
            stateMachine.ChangeState(character.idleState);
    }
}
