using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    Skeleton enemy;

    Transform Player;
    public SkeletonIdleState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        startTimer = Random.Range(1, 3);
       

        Player = GameObject.Find("Tiny Panda").GetComponent<Transform>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();

        if (startTimer <= 0)
            enemy.stateMachine.ChangeState(enemy.moveState);         

        if (enemy.IsPlayerCheck() || Vector2.Distance(enemy.transform.position, Player.position) < 2)
            enemy.stateMachine.ChangeState(enemy.battleState);
    }
}
