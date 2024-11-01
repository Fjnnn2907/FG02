using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterHeliState : CharacterState
{
    public CharacterHeliState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTimer = 1f;
        CameraShake.instance.ShakeCamera(.5f, 1f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        
        if(startTimer <= 0)
            stateMachine.ChangeState(character.idleState);


    }
}
