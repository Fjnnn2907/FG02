using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    Skeleton enemy1;
    public SkeletonIdleState(Enemy enemy, Skeleton enemy1, EnemyStateMachine stateMachine, string isBoolName) : base(enemy, stateMachine, isBoolName)
    {
        this.enemy1 = enemy1;
    }

    public override void Enter()
    {
        base.Enter();

        startTimer = Random.Range(1, 3);
        enemy1.SetZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (startTimer <= 0)
        {
            enemy1.Flip();
            enemy1.stateMachine.ChangeState(enemy1.moveState);

            if (enemy1.IsPlayerCheck())
                enemy1.stateMachine.ChangeState(enemy1.battleState);
        }
    }
}
