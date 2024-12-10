using UnityEngine;


public class CharacterState
{
    public Character character;
    public CharacterStateMachine stateMachine;

    protected string animBoolName;
    protected float startTimer;

    public bool triggerCall;

    public CharacterState(Character character, CharacterStateMachine stateMachine, string animBoolName)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }
    public virtual void Enter()
    {
        character.anim.SetBool(animBoolName, true);
        triggerCall = false;
    }

    public virtual void Update()
    {
        startTimer -= Time.deltaTime;
        character.anim.SetFloat("yVelocity", character.rb.velocity.y);
    }

    public virtual void Exit()
    {
        character.anim.SetBool(animBoolName, false);

        triggerCall = true;
    }

    public void AminationTrigger()
    {
        triggerCall = true;
    }
}
