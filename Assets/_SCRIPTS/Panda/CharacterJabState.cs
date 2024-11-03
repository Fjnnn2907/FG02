using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterJabState : CharacterState
{
    public CharacterJabState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.rb.drag = 20;
    }

    public override void Exit()
    {
        base.Exit();
        character.rb.drag = 0;
    }

    public override void Update()
    {
        base.Update();

        if(triggerCall)
            character.stateMachine.ChangeState(character.idleState);
    }
}
