using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJumpSpinState : CharacterState
{
    public CharacterJumpSpinState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        character.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        character.SetVelocity(0, -5);
        if(character.isVer2)
            character.SetVelocity(0, -10);
        if (character.IsGroundCheck())
            stateMachine.ChangeState(character.heliState);
    }
}
