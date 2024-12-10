
public class CharacterChangeState : CharacterState
{
    public CharacterChangeState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        character.rb.drag = 20;
        startTimer = 1.2f;
        character.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        character.rb.drag = 0;
    }

    public override void Update()
    {
        base.Update();

        if(startTimer <= 0)
        {
            stateMachine.ChangeState(character.idleState);
            character.ChangeVer2();
        }
    }
}
