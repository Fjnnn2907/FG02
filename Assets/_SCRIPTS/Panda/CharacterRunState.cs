
public class CharacterRunState : CharacterGroundState
{
    public CharacterRunState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        AudioManager.instance.PlaySFX(14, null);
    }

    public override void Exit()
    {
        base.Exit();

        AudioManager.instance.StopSFX(14);
    }

    public override void Update()
    {
        base.Update();

        character.SetVelocity(character.speed * character.xInput, character.rb.velocity.y);

        if(character.xInput == 0)
            stateMachine.ChangeState(character.idleState);
    }
}