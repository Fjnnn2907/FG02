
using UnityEngine;

public class CharacterAimSwordState : CharacterState
{
    public CharacterAimSwordState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        character.SetZeroVelocity();

        character.skill.swordSkill.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(character.idleState);
    }
}
