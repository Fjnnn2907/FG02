using UnityEngine;

public class CharacterAttackState : CharacterState
{
    private int comboCounter;

    private float lastimeAttacked;
    private int comboWindow = 2;

    public CharacterAttackState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.rb.drag = 20;
       

        if(comboCounter >= 2 || Time.time >= lastimeAttacked + comboWindow)
            comboCounter = 0;

        character.anim.SetInteger("ComboCounter",comboCounter);

        character.SetVelocity(character.attackMovement[comboCounter].x * 
            character.facing, character.rb.velocity.y);

    }

    public override void Exit()
    {
        base.Exit();

        character.rb.drag = 0;
        
        comboCounter++;
        lastimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCall)        
            stateMachine.ChangeState(character.idleState);
        
    }
}
