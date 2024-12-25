using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterDeahState : CharacterState
{
    public CharacterDeahState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        character.SetZeroVelocity();

        GameObject.Find("Canvas").GetComponent<UI>().SwitchOnEndScreen();
    }

    public override void Update()
    {
        base.Update();
    }
}
