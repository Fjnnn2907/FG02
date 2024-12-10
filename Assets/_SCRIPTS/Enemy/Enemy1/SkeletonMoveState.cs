using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Skeleton enemy;

    Transform Player;
    public SkeletonMoveState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        Player = GameObject.Find("Tiny Panda").GetComponent<Transform>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetVelocity(enemy.facing * enemy.speed,enemy.rb.velocity.y);

        if (enemy.IsWallCheck() || !enemy.IsGroundCheck())
        {
            enemy.Flip();
            enemy.stateMachine.ChangeState(enemy.idleState);
        }

        if(enemy.IsPlayerCheck() || Vector2.Distance(enemy.transform.position, Player.position) < 2)
            enemy.stateMachine.ChangeState(enemy.battleState);
    }
}
