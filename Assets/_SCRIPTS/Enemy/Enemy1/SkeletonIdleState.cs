using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : EnemyState
{
    Skeleton enemy1;

    Transform Player;
    public SkeletonIdleState(Enemy enemy, Skeleton enemy1, EnemyStateMachine stateMachine, string isBoolName) : base(enemy, stateMachine, isBoolName)
    {
        this.enemy1 = enemy1;
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

        enemy1.SetZeroVelocity();

        if (startTimer <= 0)
            enemy1.stateMachine.ChangeState(enemy1.moveState);         

        if (enemy1.IsPlayerCheck() || Vector2.Distance(enemy1.transform.position, Player.position) < 2)
            enemy1.stateMachine.ChangeState(enemy1.battleState);
    }
}
