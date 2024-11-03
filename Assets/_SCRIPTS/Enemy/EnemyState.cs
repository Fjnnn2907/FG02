using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState : MonoBehaviour
{
    public Character character;
    public CharacterStateMachine stateMachine;

    protected string animBoolName;
    protected float startTimer;

    public bool triggerCall;

    public EnemyState(Character character, CharacterStateMachine stateMachine, string animBoolName)
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
