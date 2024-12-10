using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Skeleton enemy1;
    public SkeletonMoveState(Enemy enemy, Skeleton enemy1, EnemyStateMachine stateMachine, string isBoolName) : base(enemy, stateMachine, isBoolName)
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
    }

    public override void Update()
    {
        base.Update();

        enemy1.SetVelocity(enemy1.facing * enemy1.speed,enemy1.rb.velocity.y);

        if (enemy1.IsWallCheck() || !enemy1.IsGroundCheck())
            enemy1.stateMachine.ChangeState(enemy1.idleState);

        if(enemy1.IsPlayerCheck())
            enemy1.stateMachine.ChangeState(enemy1.battleState);
    }
}
