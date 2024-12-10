using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonMoveState : EnemyState
{
    Skeleton enemy1;

    Transform Player;
    public SkeletonMoveState(Enemy enemy, Skeleton enemy1, EnemyStateMachine stateMachine, string isBoolName) : base(enemy, stateMachine, isBoolName)
    {
        this.enemy1 = enemy1;
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

        enemy1.SetVelocity(enemy1.facing * enemy1.speed,enemy1.rb.velocity.y);

        if (enemy1.IsWallCheck() || !enemy1.IsGroundCheck())
        {
            enemy1.Flip();
            enemy1.stateMachine.ChangeState(enemy1.idleState);
        }

        if(enemy1.IsPlayerCheck() || Vector2.Distance(enemy1.transform.position, Player.position) < 2)
            enemy1.stateMachine.ChangeState(enemy1.battleState);
    }
}
