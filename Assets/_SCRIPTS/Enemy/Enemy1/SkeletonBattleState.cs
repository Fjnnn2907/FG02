using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    Skeleton enemy;

    private Transform player;
    public int moveDir;
    public SkeletonBattleState(Enemy enemyBase, Skeleton enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Tiny Panda").GetComponent<Transform>();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = 0;
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerCheck())
        {
            startTimer = enemy.battleTime;

            if (enemy.IsPlayerCheck().distance <= enemy.radiusAttack)
            {
                if (CanAttack())
                {
                    stateMachine.ChangeState(enemy.attackState);
                    return;
                }           
            }
        }
        else
        {
            if(startTimer <= 0 || Vector2.Distance(enemy.transform.position,player.position) > 7)
                stateMachine.ChangeState(enemy.idleState);
        }

        if (player.position.x > enemy.transform.position.x)
            moveDir = 1;
        else if(player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.speed * moveDir,enemy.rb.velocity.y);
    }
    private bool CanAttack()
    {
        if (Time.time >= enemyBase.lastTimeAttacked + enemyBase.attackCoolDown)
        {
            enemyBase.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
