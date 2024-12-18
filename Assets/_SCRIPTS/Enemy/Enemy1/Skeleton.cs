
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
        battleState = new(this, this, stateMachine, "Move");
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
    }

    public override bool CanBeStuned()
    {
        if(base.CanBeStuned())
        {
            stateMachine.ChangeState(stunState);
            return true;
        }
        return false;

    }
    public override void Deah()
    {
        base.Deah();
        stateMachine.ChangeState(deahState);

        itemDrop.GrenerateDrop();
        Destroy(gameObject, 1);
    }
}
