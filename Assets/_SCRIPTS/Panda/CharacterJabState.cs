
public class CharacterJabState : CharacterState
{
    private float originalDamping;

    public CharacterJabState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        originalDamping = character.rb.linearDamping;
        character.rb.linearDamping = 20;
        startTimer = 1.5f;
    }

    public override void Exit()
    {
        base.Exit();
        character.rb.linearDamping = originalDamping;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCall || startTimer <= 0)
            character.stateMachine.ChangeState(character.idleState);
    }
}
