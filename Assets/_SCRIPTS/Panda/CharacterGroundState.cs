using System.Collections;
using System.Collections.Generic;
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

        if (Input.GetKeyDown(KeyCode.J) && character.IsGroundCheck())
            stateMachine.ChangeState(character.attackState);

        if (Input.GetKeyDown(KeyCode.H) && character.IsGroundCheck())
        {
            if (character.isVer2) return;
            stateMachine.ChangeState(character.changeState);
        }

        if (Input.GetKeyDown(KeyCode.L) && character.IsGroundCheck())
            stateMachine.ChangeState(character.rollState);

        if (Input.GetKeyDown(KeyCode.I) && character.IsGroundCheck())
            stateMachine.ChangeState(character.jabState);
    }
}
