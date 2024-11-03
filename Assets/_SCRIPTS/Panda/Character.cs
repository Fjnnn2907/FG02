using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Character : MonoBehaviour
{
    #region State
    public CharacterState character { get; private set; }
    public CharacterStateMachine stateMachine { get; private set; }
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public CharacterIdleState idleState { get; private set; }
    public CharacterRunState runState { get; private set; }
    public CharacterAttackState attackState { get; private set; }
    public CharacterJumpState jumpState { get; private set; }
    public CharacterAirState airState { get; private set; }
    public CharacterJumpSpinState jumpSpinState {  get; private set; }
    public CharacterHeliState heliState { get; private set; }
    public CharacterChangeState changeState { get; private set; }
    public CharacterRollState rollState { get; private set; }
    public CharacterJabState jabState { get; private set; }
    #endregion
    public float speed = 10;

    protected bool isRight = true;
    public int facing { get; private set; } = 1;

    public float xInput { get; set; }
    public bool isGrounded { get; set; }

    public float jumpFore = 12;
    [Header("Attack")]
    public Transform attackCheck;
    public float attackDistance;
    public LayerMask whatIsEnemy;
    public Vector2[] attackMovement;
    private bool isHited;
    [Header("Stat")]
    public int health;
    public int maxHealth;
    public Transform spawnPoint;
    private bool isDead = false;
    private bool isHit = false;
    private SpriteRenderer sr;
    public GameObject effectVer2;
    [Header("Collider")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public bool isVer2 = false;
    public float ver2Timer;
    public Vector2 boxSize;

    protected virtual void Awake()
    {
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

    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        stateMachine.StartState(idleState);
    }
    protected virtual void Update()
    {
        stateMachine.currentState.Update();

        ChangeState();


    }
    private void FixedUpdate()
    {
        CheckInput();
    }
    public void AnimationTrigger() => stateMachine.currentState.AminationTrigger();

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
    public void CheckInput()
    {
        if (Input.GetKey(KeyCode.A))
            xInput = -1;
        else if (Input.GetKey(KeyCode.D))
            xInput = 1;
        else
            xInput = 0;
    }
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position + Vector3.down * groundCheckDistance / 2, boxSize);
        //Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        //Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
        //Gizmos.DrawWireSphere(attackCheck.position, radiusAttack);
    }
    public bool IsGroundCheck() => Physics2D.BoxCast(groundCheck.position, boxSize, 0f, Vector2.down, groundCheckDistance, whatIsGround);
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);

        FlipCtrl(xVelocity);
    }
    public void SetAddForce(float xVelocity, float yVelocity)
    {
        rb.AddForce(new Vector2(xVelocity, yVelocity));
        FlipCtrl(xVelocity);
    }
    public void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
    }
    public void Flip()
    {
        facing *= -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }
    public void FlipCtrl(float x)
    {
        if (x > 0 && !isRight) Flip();
        else if (x < 0 && isRight) Flip();
    }
}
