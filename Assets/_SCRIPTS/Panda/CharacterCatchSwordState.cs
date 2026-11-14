
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
        startTimer = 1f;

        if (character.sword != null)
        {
            sword = character.sword.transform;

            if (character.transform.position.x > sword.position.x && character.facing == 1)
                character.Flip();
            else if (character.transform.position.x < sword.position.x && character.facing == -1)
                character.Flip();
        }

        character.rb.linearVelocity = new Vector2(3 * -character.facing, character.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCall || startTimer <= 0)
            stateMachine.ChangeState(character.IsGroundCheck() ? character.idleState : character.airState);
    }
}
