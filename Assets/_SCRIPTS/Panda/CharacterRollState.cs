
public class CharacterRollState : CharacterState
{
    public CharacterRollState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        startTimer = .286f;

        character.skillManager.cloneSkill.CreateClone(character.transform);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        character.SetVelocity(10 * character.facing,character.rb.velocity.y);

        if (character.isVer2)
            character.SetVelocity(15 * character.facing, character.rb.velocity.y);

        if (startTimer <= 0)
            stateMachine.ChangeState(character.idleState);
    }
}
