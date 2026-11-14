
public class CharacterChangeState : CharacterState
{
    private float originalDamping;

    public CharacterChangeState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        originalDamping = character.rb.linearDamping;
        character.rb.linearDamping = 20;
        startTimer = 1.2f;
        character.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        character.rb.linearDamping = originalDamping;
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
