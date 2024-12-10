
public class CharacterCounterAttackState : CharacterState
{
    public CharacterCounterAttackState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        startTimer = character.counterAttackTime;

        character.anim.SetBool("SussesfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        character.SetZeroVelocity();
        
        if (startTimer <= 0 || triggerCall)
            stateMachine.ChangeState(character.idleState);


    }
}
