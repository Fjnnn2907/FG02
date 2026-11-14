using UnityEngine;

public class CharacterAttackState : CharacterState
{
    private int comboCounter;

    private float lastimeAttacked;
    private int comboWindow = 2;

    private float originalDamping;

    private static readonly int comboCounterHash = Animator.StringToHash("ComboCounter");

    public CharacterAttackState(Character character, CharacterStateMachine stateMachine, string animBoolName) : base(character, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        originalDamping = character.rb.linearDamping;
        character.rb.linearDamping = 20;
        startTimer = 1.5f;

        int maxCombo = (character.attackMovement != null && character.attackMovement.Length > 0)
            ? character.attackMovement.Length
            : 2;

        if (comboCounter >= maxCombo || Time.time >= lastimeAttacked + comboWindow)
            comboCounter = 0;

        character.anim.SetInteger(comboCounterHash, comboCounter);

        float moveX = (character.attackMovement != null && comboCounter < character.attackMovement.Length)
            ? character.attackMovement[comboCounter].x
            : 0f;

        character.SetVelocity(moveX * character.facing, character.rb.linearVelocity.y);
    }

    public override void Exit()
    {
        base.Exit();

        character.rb.linearDamping = originalDamping;

        comboCounter++;
        lastimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (triggerCall || startTimer <= 0)
            stateMachine.ChangeState(character.idleState);
    }
}
