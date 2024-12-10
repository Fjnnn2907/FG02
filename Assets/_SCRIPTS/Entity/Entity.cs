using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region State
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion
    public float speed = 10;

    protected bool isRight = true;
    public int facing { get; private set; } = 1;
    [Header("Collider")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    public Vector2 boxSize;
    

    protected virtual void Awake()
    {
        
    }
    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Update()
    {
    }
    public virtual bool IsGroundCheck() => Physics2D.BoxCast(groundCheck.position, boxSize, 0f, Vector2.down, groundCheckDistance, whatIsGround);
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(groundCheck.position + Vector3.down * groundCheckDistance / 2, boxSize);
    }
    #region Velocity
    public virtual void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);

        FlipCtrl(xVelocity);
    }
    public virtual void SetZeroVelocity()
    {
        rb.velocity = Vector2.zero;
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
}
