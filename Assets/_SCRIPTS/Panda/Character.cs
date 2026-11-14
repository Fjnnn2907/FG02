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
    public CharacterDeahState deahState { get; private set; }
    #endregion
    public float jumpFore;
    public float defautJumpFore;
    public float xInput { get; set; }
    public float rollSpeed = 10;
    public float defautRollSpeed;
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
    public float ver2Timer { get; set; }
    private bool wasVer2;
    private float slowMultiplier = 1f;
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
        deahState = new(this, stateMachine, "Deah");
    }
    protected override void Start()
    {
        base.Start();
        skill = SkillManager.instance;

        SetupInitStateVer1();

        stateMachine.StartState(idleState);
    }

    private void SetupInitStateVer1()
    {
        defautSpeed = speed;
        defautJumpFore = jumpFore;
        defautRollSpeed = rollSpeed;
        ver2Timer = 10f;
        isVer2 = false;

        int v1 = anim != null ? anim.GetLayerIndex("Ver1") : -1;
        int v2 = anim != null ? anim.GetLayerIndex("Ver2") : -1;
        if (v1 >= 0) anim.SetLayerWeight(v1, 1);
        if (v2 >= 0) anim.SetLayerWeight(v2, 0);
        if (effectVer2 != null) effectVer2.SetActive(false);
    }

    protected override void Update()
    {
        if (Time.timeScale == 0)
            return;

        base.Update();

        if (stats != null && stats.isDeah)
        {
            xInput = 0;
            stateMachine.currentState.Update();
            return;
        }

        if (skill == null)
            skill = SkillManager.instance;

        CheckInput();
        stateMachine.currentState.Update();
        ChangeState();

        if (Input.GetKeyDown(KeyCode.Q) && skill != null && skill.crytalSkill != null)
            skill.crytalSkill.CanUseSkill();

        if (Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance != null)
            Inventory.instance.UseFlask();
    }
    private void FixedUpdate()
    {
    }
    public override void SlowEntity(float _slowPer, float _slowDuration)
    {
        CancelInvoke("ReturnDefautSpeed");
        slowMultiplier = Mathf.Clamp01(1f - _slowPer);
        ApplyMovementStats();

        Invoke("ReturnDefautSpeed", _slowDuration);
    }
    protected override void ReturnDefautSpeed()
    {
        base.ReturnDefautSpeed();
        slowMultiplier = 1f;
        ApplyMovementStats();
    }
    private void ApplyMovementStats()
    {
        float baseSpd = isVer2 ? 7f : defautSpeed;
        float baseJump = isVer2 ? 900f : defautJumpFore;
        float baseAnim = isVer2 ? 1.2f : 1f;

        speed = baseSpd * slowMultiplier;
        rollSpeed = defautRollSpeed * slowMultiplier;
        jumpFore = baseJump * slowMultiplier;
        if (anim != null)
            anim.speed = baseAnim * slowMultiplier;
    }
    public virtual void AnimationTrigger() => stateMachine.currentState.AminationTrigger();
    public override void Deah()
    {
        base.Deah();

        if (stateMachine.currentState != deahState)
            stateMachine.ChangeState(deahState);
    }
    #region Skill
    public void NewSword(GameObject _newSword)
    {
        sword = _newSword;
    }
    public void CleanSword()
    {
        if (stats == null || !stats.isDeah)
            stateMachine.ChangeState(catchSwordState);

        if (sword != null)
            Destroy(sword);
        sword = null;
    }
    public void ExitBackholeSkill()
    {
        if (stats == null || !stats.isDeah)
            stateMachine.ChangeState(airState);
    }
    #endregion
    #region ChangeStateVer
    public void ChangeVer2()
    {
        isVer2 = true;
        ver2Timer = 10f;
    }
    private void ChangeState()
    {
        if (isVer2)
        {
            if (!wasVer2)
            {
                wasVer2 = true;
                int v1 = anim != null ? anim.GetLayerIndex("Ver1") : -1;
                int v2 = anim != null ? anim.GetLayerIndex("Ver2") : -1;
                if (v1 >= 0) anim.SetLayerWeight(v1, 0);
                if (v2 >= 0) anim.SetLayerWeight(v2, 1);
                if (effectVer2 != null) effectVer2.SetActive(true);
                Ver2Stat();
            }

            ver2Timer -= Time.deltaTime;
            if (ver2Timer <= 0)
                isVer2 = false;
        }
        else if (wasVer2)
        {
            wasVer2 = false;
            ver2Timer = 10f;
            int v1 = anim != null ? anim.GetLayerIndex("Ver1") : -1;
            int v2 = anim != null ? anim.GetLayerIndex("Ver2") : -1;
            if (v1 >= 0) anim.SetLayerWeight(v1, 1);
            if (v2 >= 0) anim.SetLayerWeight(v2, 0);
            if (effectVer2 != null) effectVer2.SetActive(false);
            Ver1Stat();
        }
    }
    public void Ver2Stat()
    {
        ApplyMovementStats();
    }
    public void Ver1Stat()
    {
        ApplyMovementStats();
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
