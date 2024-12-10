using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    Skeleton enemy;
    public SkeletonAttackState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;   
    }

    public override void Enter()
    {
        base.Enter();
        
        
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (isCalled)
            stateMachine.ChangeState(enemy.battleState);
    }
}
