using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyState
{
    public Enemy enemy;
    public EnemyStateMachine stateMachine;

    public string isBoolName;
    public bool isCalled;
    public float startTimer;

    public EnemyState(Enemy enemy, EnemyStateMachine stateMachine, string isBoolName)
    {
        this.enemy = enemy;
        this.stateMachine = stateMachine;
        this.isBoolName = isBoolName;
    }
    public virtual void Enter()
    {
        enemy.anim.SetBool(isBoolName,true);
        isCalled = false;
    }
    public virtual void Update()
    {
        startTimer -= Time.deltaTime;
    }
    public virtual void Exit()
    {
        enemy.anim.SetBool(isBoolName, false);
    }
    public void AminationTrigger()
    {
        isCalled = true;
    }
}
