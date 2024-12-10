
public class CharacterHeliState : CharacterState
{
    public CharacterHeliState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        CameraShake.instance.ShakeCamera(.5f, .2f);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCall)
            stateMachine.ChangeState(character.idleState);


    }
}
