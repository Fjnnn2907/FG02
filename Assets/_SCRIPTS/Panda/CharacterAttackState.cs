using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAttackState : CharacterState
{
    public CharacterAttackState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.rb.drag = 20;
        character.SetZeroVelocity();
        //SoundManager.instance.PlaySFX(SoundManager.instance.Attack);
    }

    public override void Exit()
    {
        base.Exit();

        character.rb.drag = 0;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCall)
            stateMachine.ChangeState(character.idleState);
    }
}
