using UnityEngine;

public class Character : Entity
{
    #region State
    public CharacterState character { get; private set; }
    public CharacterStateMachine stateMachine { get; private set; }

    public CharacterIdleState idleState { get; private set; }
    public CharacterRunState runState { get; private set; }
    public CharacterAttackState attackState { get; private set; }
    public CharacterJumpState jumpState { get; private set; }
    public CharacterAirState airState { get; private set; }
    public CharacterJumpSpinState jumpSpinState { get; private set; }
    public CharacterHeliState heliState { get; private set; }
    public CharacterChangeState changeState { get; private set; }
    public CharacterRollState rollState { get; private set; }
    public CharacterJabState jabState { get; private set; }
    public CharacterCounterAttackState counterAttackState { get; private set; }
    public CharacterAimSwordState aimSwordState { get; private set; }
    public CharacterCatchSwordState catchSwordState { get; private set; }
    public CharacterBackholeState backholeState { get; private set; }
    #endregion
    public float jumpFore = 12;
    public float xInput { get; set; }
    #region SKill
    public SkillManager skill { get; private set; }
    public GameObject sword; //{  get; private set; }  
        #endregion
    [Header("Attack")]
    public Vector2[] attackMovement;
    private bool isHited;
    public float counterAttackTime =2f;

    [Header("Stat")]
    public int health;
    public int maxHealth;
    public GameObject effectVer2;
    #region Ver2
    public bool isVer2 { get; set; } = false;
    public float ver2Timer { get;set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new();
        idleState = new(this, stateMachine, "Idle");
        runState = new(this, stateMachine, "Run");
        attackState = new(this, stateMachine, "Attack");
        jumpState = new(this, stateMachine, "Jump");
        airState = new(this, stateMachine, "Jump");
        jumpSpinState = new(this, stateMachine, "JumpSpin");
        heliState = new(this, stateMachine, "Heli");
        changeState = new(this, stateMachine, "ChangeState");
        rollState = new(this, stateMachine, "Roll");
        jabState = new(this, stateMachine, "Jab");
        counterAttackState = new(this, stateMachine, "CounterAttack");
        aimSwordState = new(this, stateMachine, "AimSword");
        catchSwordState = new(this, stateMachine, "CatchSword");
        backholeState = new(this, stateMachine, "Jump");
    }
    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;
        stateMachine.StartState(idleState);
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
        ChangeState();

        if (Input.GetKeyDown(KeyCode.Q))
            skill.crytalSkill.UseSkill();
    }
    private void FixedUpdate()
    {
        CheckInput();
    }
    public virtual void AnimationTrigger() => stateMachine.currentState.AminationTrigger();
    #region Skill
    public void NewSword(GameObject _newSword)
    {
        sword = _newSword;
    }
    public void CleanSword()
    {
        stateMachine.ChangeState(catchSwordState);
        Destroy(sword);      
    }
    public void ExitBackholeSkill()
    {
        stateMachine.ChangeState(airState);
    }
    #endregion
    #region ChangeStateVer
    public void ChangeVer2()
    {
        isVer2 = true;
    }
    private void ChangeState()
    {
        if (isVer2)
        {
            anim.SetLayerWeight(anim.GetLayerIndex("Ver1"), 0);
            anim.SetLayerWeight(anim.GetLayerIndex("Ver2"), 1);
            effectVer2.SetActive(true);
            Ver2Stat();
            ver2Timer -= Time.deltaTime;
            if (ver2Timer <= 0)
                isVer2 = false;
        }
        else
        {
            ver2Timer = 10;
            anim.SetLayerWeight(anim.GetLayerIndex("Ver1"), 1);
            anim.SetLayerWeight(anim.GetLayerIndex("Ver2"), 0);
            effectVer2.SetActive(false);
            Ver1Stat();
        }
    }
    public void Ver2Stat()
    {
        speed = 7;
        jumpFore = 900;
        anim.speed = 1.2f;
    }
    public void Ver1Stat()
    {
        speed = 5;
        jumpFore = 700;
        anim.speed = 1;
    }
    #endregion
    #region Input
    public void CheckInput()
    {
        if (Input.GetKey(KeyCode.A))
            xInput = -1;
        else if (Input.GetKey(KeyCode.D))
            xInput = 1;
        else
            xInput = 0;
    }
    #endregion
    #region Velocity
    public void SetAddForce(float xVelocity, float yVelocity)
    {
        rb.AddForce(new Vector2(xVelocity, yVelocity));
    }
    #endregion
   
}
