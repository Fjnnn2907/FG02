using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonDeahState : EnemyState
{
    private Skeleton enemy;
    public SkeletonDeahState(Enemy enemyBase, Skeleton _enemy, EnemyStateMachine stateMachine, string isBoolName) : base(enemyBase, stateMachine, isBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetZeroVelocity();
    }

    public override void Update()
    {
        base.Update();

    }
}
