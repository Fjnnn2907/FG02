using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1AttackState : EnemyState
{
    Enemy1 enemy1;
    public Enemy1AttackState(Enemy enemy, Enemy1 enemy1, EnemyStateMachine stateMachine, string isBoolName) : base(enemy, stateMachine, isBoolName)
    {
        this.enemy1 = enemy1;   
    }

    public override void Enter()
    {
        base.Enter();
        
        
    }

    public override void Exit()
    {
        base.Exit();

        enemy1.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy1.SetZeroVelocity();

        if (isCalled)
            stateMachine.ChangeState(enemy1.battleState);
    }
}
