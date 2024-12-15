using System.Collections;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region State
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    public EntityFX fx { get; private set; }
    public SpriteRenderer sr { get; private set; }
    public StatManager stats { get; private set; }
    #endregion
    
    public float speed = 10;
    protected bool isRight = true;
    public int facing { get; private set; } = 1;
    [Header("Collider")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public Vector2 boxSize;
    
    [Header("Knock Back")]
    public Vector2 KnockDir = new Vector2(7,12);
    public float KnockTimer = .07f;
    protected bool isKnocked;

    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        fx = GetComponent<EntityFX>();
        sr = GetComponentInChildren<SpriteRenderer>();
        stats = GetComponent<StatManager>();
    }
    protected virtual void Update()
    {
    }
    #region Battle
    public virtual void DamageEffect()
    {
        fx.StartCoroutine("HitFlashFx");
        StartCoroutine("KnockBack");
    }
    protected virtual IEnumerator KnockBack()
    {
        isKnocked = true;
        rb.velocity = new Vector2(KnockDir.x * -facing, KnockDir.y);
        yield return new WaitForSeconds(KnockTimer);
        isKnocked = false;
    }
    #endregion
    public virtual bool IsGroundCheck() => Physics2D.BoxCast(groundCheck.position, boxSize, 0f, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position + Vector3.down * groundCheckDistance / 2, boxSize);
    }
    #region Velocity
    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        if(isKnocked) return;

        rb.velocity = new Vector2(xVelocity, yVelocity);

        FlipCtrl(xVelocity);
    }
    public virtual void SetZeroVelocity()
    {
        if (isKnocked) return;
        rb.velocity = new Vector2(0,rb.velocity.y);
    }
    #endregion
    #region Flip
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
    #endregion
    public void TanHinh(bool _isTanHinh)
    {
        if(_isTanHinh)
            sr.color = Color.clear;
        else
            sr.color = Color.white;
    }
    public virtual void Deah()
    {

    }
}
