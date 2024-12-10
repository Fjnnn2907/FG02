using UnityEngine;

public class CharacterAirState : CharacterState
{
    public CharacterAirState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
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

        character.SetVelocity(character.xInput * character.speed, character.rb.velocity.y);

        if (character.IsGroundCheck())
            stateMachine.ChangeState(character.idleState);

        if (Input.GetKeyDown(KeyCode.U))
        {
            stateMachine.ChangeState(character.jumpSpinState);
        }
    }
}
