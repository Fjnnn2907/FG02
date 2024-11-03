using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Enemy : MonoBehaviour
{
    public EnemyState enemy {  get; private set; }
    public EnemyStateMachine stateMachine { get; private set; }

    [Header("Collider")]
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [Header("Knock Back")]
    public Vector2 KnockDir;
    public float KnockTimer;
    private bool isKnock;

    protected bool isRight = true;
    public int facing { get; private set; } = 1;

    #region Compoments
    public Animator anim { get; private set; }
    public Rigidbody2D rb { get; private set; }

    public EmtityFx emtityFx { get; private set; }
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        emtityFx = GetComponentInChildren<EmtityFx>();
    }

    protected virtual void Update()
    {

    }

    public void Damge()
    {
        Debug.Log("hit " + gameObject.name);
        //StartCoroutine(KnockBack());
        emtityFx.StartCoroutine("HitFlashFx");

    }

    //public IEnumerator KnockBack()
    //{
    //    isKnock = true;
    //    rb.velocity = new Vector3(KnockDir.x * -facing, KnockDir.y);
    //    yield return new WaitForSeconds(KnockTimer);
    //    isKnock = false;
    //}
    public bool IsGroundCheck() => Physics2D.Raycast(groundCheck.position,
        Vector2.down, groundCheckDistance, whatIsGround);

    public bool IsWallCheck() => Physics2D.Raycast(wallCheck.position,
        Vector2.right * facing, wallCheckDistance, whatIsGround);
    public virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y));
    }

    public void SetVelocity(float xVelocity, float yVelocity)
    {
        if (isKnock)
            return;

        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipCtr(xVelocity);
    }

    public void Flip()
    {
        facing *= -1;
        isRight = !isRight;
        transform.Rotate(0, 180, 0);
    }

    public void FlipCtr(float _x)
    {
        if (_x > 0 && !isRight) Flip();
        else if (_x < 0 && isRight) Flip();

    }
}
