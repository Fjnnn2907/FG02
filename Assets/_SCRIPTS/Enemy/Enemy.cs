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
    }
    protected override void Start()
    {      
        base.Start();

        HP = maxHP;
        speedTamp = speed;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }
    #region check
    public bool IsWallCheck() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facing, wallCheckDistance, whatIsGround);

    public RaycastHit2D IsPlayerCheck() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facing, attackDistace, WhatIsPlayer);

    public void AnimationTrigger() => stateMachine.currentState.AminationTrigger();
    public virtual void OpenCounterAttack()
    {
        canBeStuned = true;
        counterImage.SetActive(true);
    }
    public virtual void CloseCounterAttack()
    {
        canBeStuned = false;
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
        speed = speed * (1 - _slowPer);
        anim.speed = anim.speed * (1 - _slowPer);

        Invoke("ReturnDefautSpeed", _slowDuration);
    }
    protected override void ReturnDefautSpeed()
    {
        base.ReturnDefautSpeed();
        speed = speedTamp;
    }
    public virtual void FreezeTime(bool _timeFreeze)
    {
        if (_timeFreeze)
        {
            anim.speed = 0;
            speed = 0;  
        }
        else
        {
            anim.speed = 1;
            speed = speedTamp;
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
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facing, wallCheck.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistace * facing, transform.position.y));
    }

}