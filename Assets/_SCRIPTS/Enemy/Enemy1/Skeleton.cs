using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    protected override void Awake()
    {
        base.Awake();

        idleState = new(this, this, stateMachine, "Idle");
        moveState = new(this, this, stateMachine, "Move");
        attackState = new(this, this, stateMachine, "Attack");
        battleState = new(this, this, stateMachine, "Move");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.StartState(moveState);
    }
}
