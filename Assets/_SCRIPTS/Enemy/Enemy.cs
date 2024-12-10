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
    [Header("Knock Back")]
    public Vector2 KnockDir;
    public float KnockTimer;
    private bool isKnock;

    [Header("Stat")]
    public float speedBattle = 5f;
    public int maxHP;
    private int HP;

    [Header("Attack")]
    [HideInInspector]public float lastTimeAttacked;
    public float attackCoolDown;
    public float battleTime;
    public float attackDistace;

    #region Compoments
    public EmtityFx emtityFx { get; private set; }
    public EnemyState enemy { get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new();
    }
    protected override void Start()
    {
        base.Start();
        emtityFx = GetComponentInChildren<EmtityFx>();
        HP = maxHP;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    public void Damge(int damege)
    {
        HP -= damege;
        Debug.Log("hit " + gameObject.name);
        StartCoroutine(KnockBack());
        emtityFx.StartCoroutine("HitFlashFx");

    }
    public IEnumerator KnockBack()
    {
        isKnock = true;
        rb.velocity = new Vector3(KnockDir.x * -facing, KnockDir.y);
        yield return new WaitForSeconds(KnockTimer);
        isKnock = false;
    }
    public bool IsWallCheck() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facing, wallCheckDistance, whatIsGround);

    public RaycastHit2D IsPlayerCheck() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facing, attackDistace, WhatIsPlayer);

    public void AnimationTrigger() => stateMachine.currentState.AminationTrigger();

    public override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facing, wallCheck.position.y));
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistace * facing, transform.position.y));
    }

}