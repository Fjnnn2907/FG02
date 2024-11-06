using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : Enemy
{
    public Enemy1IdleState idleState {  get; private set; }
    public Enemy1MoveState moveState { get; private set; }
    public Enemy1AttackState attackState { get; private set; }
    public Enemy1BattleState battleState { get; private set; }
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
