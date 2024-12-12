
using UnityEngine;

public class CharacterAimSwordState : CharacterState
{
    public CharacterAimSwordState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


        character.skill.swordSkill.DotsActive(true);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        character.SetZeroVelocity();
        if (Input.GetKeyUp(KeyCode.Mouse1))
            stateMachine.ChangeState(character.idleState);

        Vector2 mousePos = Camera.main.ViewportToScreenPoint(Input.mousePosition);

        if (character.transform.position.x > mousePos.x && character.facing == 1)
            character.Flip();
        else if(character.transform.position.x < mousePos.x && character.facing == -1)
            character.Flip();
    }
}
