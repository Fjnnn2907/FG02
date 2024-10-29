using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class CharacterJumpState : CharacterState
{
    public CharacterJumpState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        character.SetAddForce(character.rb.velocity.x, character.jumpFore);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        character.SetVelocity(character.xInput * character.speed, character.rb.velocity.y);


        if (character.rb.velocity.y < 0)
            stateMachine.ChangeState(character.airState);
    }
}
