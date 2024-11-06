using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1BattleState : EnemyState
{
    Enemy1 enemy1;

    private Transform player;
    public int moveDir;
    public Enemy1BattleState(Enemy enemy, Enemy1 enemy1, EnemyStateMachine stateMachine, string isBoolName) : base(enemy, stateMachine, isBoolName)
    {
        this.enemy1 = enemy1;
    }

    public override void Enter()
    {
        base.Enter();

        player = GameObject.Find("Tiny Panda").GetComponent<Transform>();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy1.IsPlayerCheck())
        {
            startTimer = enemy.battleTime;
            if (enemy1.IsPlayerCheck().distance <= enemy1.distanceAttack)
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy1.attackState);
            }
            return;
        }
        else
        {
            if (startTimer < 0 || Vector3.Distance(enemy1.transform.position, player.position) > 10)
                stateMachine.ChangeState(enemy1.idleState);
        }


        if (enemy1.transform.position.x < player.position.x)
            moveDir = 1;
        else if (enemy1.transform.position.x > player.position.x)
            moveDir = -1;

        enemy1.SetVelocity(enemy1.speed * moveDir, enemy1.rb.velocity.y);
    }
    private bool CanAttack()
    {
        if (Time.time >= enemy.lastTimeAttacked + enemy.attackCoolDown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }
        return false;
    }
}
