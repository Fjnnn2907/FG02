using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    Skeleton enemy1;

    private Transform player;
    public int moveDir;
    public float moveSpeed;
    public SkeletonBattleState(Enemy enemy, Skeleton enemy1, EnemyStateMachine stateMachine, string isBoolName) : base(enemy, stateMachine, isBoolName)
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
            if (enemy1.IsPlayerCheck().distance <= enemy1.attackDistace)
            {
                Debug.Log("Attack");
                enemy1.SetZeroVelocity();
                return;
            }
        }

        if (player.position.x > enemy1.transform.position.x)
            moveDir = 1;
        else if(player.position.x < enemy1.transform.position.x)
            moveDir = -1;

        enemy1.SetVelocity(enemy1.speed * moveDir,enemy1.rb.velocity.y);
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
