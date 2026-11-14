using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Collider")]
    public Transform attackCheck;
    public float radiusAttack;
    [SerializeField] protected LayerMask WhatIsPlayer;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;

    [Header("Stat")]
    public float speedBattle = 5f;
    public int maxHP;
    private int HP;
    private float speedTamp;
    private float speedBattleTamp;
    private bool isFrozen;
    private float slowMultiplier = 1f;

    [Header("Attack")]
    public float attackCoolDown;
    public float battleTime;
    public float attackDistace;
    [HideInInspector]public float lastTimeAttacked;
    [Header("Stun")]
    public float stunTime;
    public Vector2 stunDir;
    protected bool canBeStuned;
    [SerializeField] protected GameObject counterImage;
    #region Compoments
    public EnemyState enemy { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }

    public ItemDrop itemDrop;
    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new();
        if (itemDrop == null)
            itemDrop = GetComponent<ItemDrop>();
    }
    protected override void Start()
    {
        base.Start();

        HP = maxHP;
        speedTamp = speed;
        speedBattleTamp = speedBattle;
    }
    protected override void Update()
    {
        base.Update();
        if (stateMachine != null && stateMachine.currentState != null)
            stateMachine.currentState.Update();
    }
    #region check
    public bool IsWallCheck() => wallCheck != null && Physics2D.Raycast(wallCheck.position,
        Vector2.right * facing, wallCheckDistance, whatIsGround);

    public RaycastHit2D IsPlayerCheck() => wallCheck != null
        ? Physics2D.Raycast(wallCheck.position, Vector2.right * facing, attackDistace, WhatIsPlayer)
        : default;

    public void AnimationTrigger() => stateMachine.currentState.AminationTrigger();
    public virtual void OpenCounterAttack()
    {
        canBeStuned = true;
        if (counterImage != null)
            counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttack()
    {
        canBeStuned = false;
        if (counterImage != null)
            counterImage.SetActive(false);
    }
    public virtual bool CanBeStuned()
    {
        if (canBeStuned)
        {
            CloseCounterAttack();
            return true;
        }
        return false;
    }
    #endregion
    public override void SlowEntity(float _slowPer, float _slowDuration)
    {
        CancelInvoke("ReturnDefautSpeed");
        slowMultiplier = Mathf.Clamp01(1f - _slowPer);
        ApplySpeedState();

        Invoke("ReturnDefautSpeed", _slowDuration);
    }
    protected override void ReturnDefautSpeed()
    {
        base.ReturnDefautSpeed();
        slowMultiplier = 1f;
        ApplySpeedState();
    }
    public virtual void FreezeTime(bool _timeFreeze)
    {
        isFrozen = _timeFreeze;
        ApplySpeedState();
    }
    private void ApplySpeedState()
    {
        if (isFrozen)
        {
            if (anim != null) anim.speed = 0;
            speed = 0;
            speedBattle = 0;
        }
        else
        {
            if (anim != null) anim.speed = slowMultiplier;
            speed = speedTamp * slowMultiplier;
            speedBattle = speedBattleTamp * slowMultiplier;
        }
    }
    public virtual IEnumerator FreezeTimeFor(float _seconds)
    {
        FreezeTime(true);
        yield return new WaitForSeconds(_seconds);
        FreezeTime(false);
    }
    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        if (wallCheck != null)
        {
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facing, wallCheck.position.y));
            Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + attackDistace * facing, wallCheck.position.y));
        }
    }

}