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

        if (character.rb != null)
            character.rb.linearVelocity = Vector2.zero;

        UI ui = Object.FindFirstObjectByType<UI>();
        if (ui != null)
            ui.SwitchOnEndScreen();
    }

    public override void Update()
    {
        base.Update();
        character.SetZeroVelocity();
    }
}
