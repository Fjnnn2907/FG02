
public class CharacterRunState : CharacterGroundState
{
    public CharacterRunState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        if (AudioManager.instance != null)
            AudioManager.instance.PlaySFX(14, null);
    }

    public override void Exit()
    {
        base.Exit();

        if (AudioManager.instance != null)
            AudioManager.instance.StopSFX(14);
    }

    public override void Update()
    {
        base.Update();

        if (stateMachine.currentState != this)
            return;

        character.SetVelocity(character.speed * character.xInput, character.rb.linearVelocity.y);

        if (character.xInput == 0)
            stateMachine.ChangeState(character.idleState);
    }
}