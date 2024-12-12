using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBackholeState : CharacterState
{
    private float gravityTamp;

    private float flyTime = .4f;
    private bool skillUsed;
    public CharacterBackholeState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        gravityTamp = character.rb.gravityScale;

        skillUsed = false;
        startTimer = flyTime;
        character.rb.gravityScale = 0;
    }

    public override void Exit()
    {
        base.Exit();

        character.rb.gravityScale = gravityTamp;
        character.TanHinh(false);
    }

    public override void Update()
    {
        base.Update();
        if (startTimer > 0)
            character.rb.velocity = new Vector2(0, 8);
        if(startTimer < 0)
        {
            character.rb.velocity = new Vector2(0, -.1f);
            if (!skillUsed)
            {
                if(character.skill.backholiSkill.CanUseSkill())
                skillUsed = true;
            }
        }
    }
}
