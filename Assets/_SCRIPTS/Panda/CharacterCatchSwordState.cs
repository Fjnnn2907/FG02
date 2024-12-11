
using UnityEngine;

public class CharacterCatchSwordState : CharacterState
{
    Transform sword;
    public CharacterCatchSwordState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        sword = character.sword.transform;

        if (character.transform.position.x > sword.position.x && character.facing == 1)
            
            character.Flip();
        else if (character.transform.position.x < sword.position.x && character.facing == -1)
            character.Flip();

        character.rb.velocity = new Vector2(3 * -character.facing,character.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if(triggerCall)
            stateMachine.ChangeState(character.idleState);
    }
}
