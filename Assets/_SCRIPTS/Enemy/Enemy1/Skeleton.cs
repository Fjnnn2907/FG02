
using UnityEngine;

public class Skeleton : Enemy
{
    #region States
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonMoveState moveState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonStunState stunState { get; private set; }
    public SkeletonDeahState deahState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();

        idleState = new(this, this, stateMachine, "Idle");
        moveState = new(this, this, stateMachine, "Move");
        attackState = new(this, this, stateMachine, "Attack");
        battleState = new(this, this, stateMachine, "Battle");
        stunState = new(this, this, stateMachine, "Stun");
        deahState = new(this, this, stateMachine, "Deah");
    }
    protected override void Start()
    {
        base.Start();
        stateMachine.StartState(moveState);
    }
    protected override void Update()
    {
        base.Update();

        //if(Input.GetKeyDown(KeyCode.Q))
        //    stateMachine.ChangeState(stunState);

        if (Input.GetKeyDown(KeyCode.Q))
            CheckCurrentState();
    }

    private bool hasDied;

    public override bool CanBeStuned()
    {
        if (hasDied || (stats != null && stats.isDeah))
            return false;

        if (base.CanBeStuned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;
    }
    public override void Deah()
    {
        if (hasDied)
            return;
        hasDied = true;

        base.Deah();
        CloseCounterAttack();
        stateMachine.ChangeState(deahState);

        Collider2D[] cols = GetComponentsInChildren<Collider2D>();
        foreach (var col in cols)
        {
            col.enabled = false;
        }
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
            rb.bodyType = UnityEngine.RigidbodyType2D.Kinematic;
        }

        if (itemDrop != null)
            itemDrop.GrenerateDrop();

        Destroy(gameObject, 1);
    }

    private void CheckCurrentState()
    {
        Debug.Log(stateMachine.currentState.ToString());
    }
}
