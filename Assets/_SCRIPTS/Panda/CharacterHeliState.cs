
public class CharacterHeliState : CharacterState
{
    public CharacterHeliState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTimer = 1.5f;

        if (CameraShake.instance != null)
            CameraShake.instance.ShakeCamera(.5f, .2f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCall || startTimer <= 0)
            stateMachine.ChangeState(character.idleState);
    }
}
