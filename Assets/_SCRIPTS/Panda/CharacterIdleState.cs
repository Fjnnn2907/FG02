
public class CharacterIdleState : CharacterGroundState
{
    public CharacterIdleState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        character.SetZeroVelocity();

    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (character.xInput != 0)
            stateMachine.ChangeState(character.runState);
    }
}