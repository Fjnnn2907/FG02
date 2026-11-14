using UnityEngine;


public class CharacterState
{
    public Character character;
    public CharacterStateMachine stateMachine;

    protected string animBoolName;
    protected int animBoolHash;
    protected static readonly int yVelocityHash = Animator.StringToHash("yVelocity");
    protected float startTimer;

    public bool triggerCall;

    public CharacterState(Character character, CharacterStateMachine stateMachine, string animBoolName)
    {
        this.character = character;
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
        this.animBoolHash = !string.IsNullOrEmpty(animBoolName) ? Animator.StringToHash(animBoolName) : 0;
    }
    public virtual void Enter()
    {
        if (animBoolHash != 0 && character.anim != null)
            character.anim.SetBool(animBoolHash, true);
        triggerCall = false;
    }

    public virtual void Update()
    {
        startTimer -= Time.deltaTime;
        if (character.anim != null && character.rb != null)
            character.anim.SetFloat(yVelocityHash, character.rb.linearVelocity.y);
    }

    public virtual void Exit()
    {
        if (animBoolHash != 0 && character.anim != null)
            character.anim.SetBool(animBoolHash, false);

        triggerCall = true;
    }

    public void AminationTrigger()
    {
        triggerCall = true;
    }
}
