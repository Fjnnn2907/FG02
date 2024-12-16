using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D collider2d;
    private Character character;

    private float returnSpeed;
    private bool isReturning;
    private bool canRotate = true;
    private float freezeTime;

    [Header("Bouncing")]
    private float bounceSpeed;
    private bool isBounsing;
    private int bouceAmout;
    public List<Transform> enemyTarget;
    private int targetIndex;
    
    [Header("Pierce")]
    private int pierceAmout;

    [Header("Spin")]
    private float maxTravelDistace;
    private float spinDuration;
    private float spinTimer;
    private bool wasStopped;
    private bool isSprinng;
    private float spinDir;

    private float hitTimer;
    private float hitCooldown;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        collider2d = GetComponent<CapsuleCollider2D>();
    }

    #region Set up Sword
    public void SetUpSword(Vector2 _dir, float _gravity, Character _character, float _freeTime, float _returnSpeed)
    {
        character = _character;
        rb.velocity = _dir;
        rb.gravityScale = _gravity;
        freezeTime = _freeTime;
        returnSpeed = _returnSpeed;

        if (pierceAmout <= 0)
            anim.SetBool("Active", true);

        spinDir = Mathf.Clamp(rb.velocity.x, -1f, 1f);
    }
    #endregion

    #region Set up Bouce
    public void SetupBouce(bool _isBoucing, int _amoutOfBouce,float _bouceSpeed)
    {
        isBounsing = _isBoucing;
        bouceAmout = _amoutOfBouce;
        bounceSpeed = _bouceSpeed;

        enemyTarget = new List<Transform>();
    }
    #endregion

    #region Set up Pierce
    public void SetUpPierce(int _pierceAmout)
    {
        pierceAmout = _pierceAmout;
    }
    #endregion

    #region Set up Spin
    public void SetupSpin(bool _isSpinning, float _maxTravelDistace, float _spinDuration, float _hitCooldown)
    {
        isSprinng = _isSpinning;
        maxTravelDistace = _maxTravelDistace;
        spinDuration = _spinDuration;
        hitCooldown = _hitCooldown;
    }
    #endregion
    public void ReturnSword()
    {
        if (isReturning) return;

        transform.parent = null;
        rb.isKinematic = false;
        isReturning = true;
    }
    private void Update()
    {
        if (canRotate)
            transform.right = rb.velocity;

        if (Vector2.Distance(transform.position, character.transform.position) > 30)
            Destroy(gameObject);

        if (isReturning)
        {
            canRotate = false;
            //Vector2 directionToPlayer = (character.transform.position - transform.position);
            //transform.right = directionToPlayer;

            transform.position = Vector2.MoveTowards
                (transform.position, character.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, character.transform.position) < .5f)
                character.CleanSword();
        }

        BouceLogic();

        SpinLogic();
    }

    private void SpinLogic()
    {
        if (isSprinng)
        {
            if (Vector2.Distance(character.transform.position, transform.position) > maxTravelDistace && !wasStopped)
            {
                StopWhenSpin();
            }
            if (wasStopped)
            {
                transform.position = Vector2.MoveTowards(transform.position, 
                    new Vector2(transform.position.x + spinDir, transform.position.y), 
                    1.5f * Time.deltaTime);

                spinTimer -= Time.deltaTime;
                if (spinTimer < 0)
                {
                    isReturning = true;
                    isSprinng = false;
                }

                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (Collider2D hit in colliders)
                    {
                        if (hit.GetComponent<Enemy>() != null)
                            SwordSkillDamege(hit.GetComponent<Enemy>());
                    }
                }
            }
        }
    }

    private void StopWhenSpin()
    {
        wasStopped = true;
        rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BouceLogic()
    {
        if (isBounsing && enemyTarget.Count > 0)
        {
            anim.SetBool("Active", true);
            transform.position = Vector2.MoveTowards(transform.position,
                enemyTarget[targetIndex].position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, enemyTarget[targetIndex].position) < .1f)
            {
                SwordSkillDamege(enemyTarget[targetIndex].GetComponent<Enemy>());

                targetIndex++;
                bouceAmout--;
                if (bouceAmout <= 0)
                {
                    isBounsing = false;
                    isReturning = true;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning) return;
        // Bouce


        //if(collision.GetComponent<Enemy>() != null)
        //{
        //    Enemy enemy = collision.GetComponent<Enemy>();
        //    SwordSkillDamege(enemy);
        //}

        if (collision.GetComponent<Enemy>() != null)
        {
            if (isBounsing && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (Collider2D hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                        enemyTarget.Add(hit.transform);
                }
            }
        }

        StuckInto(collision);
    }

    private void SwordSkillDamege(Enemy enemy)
    {
        character.stats.DoMagicDamage(enemy.GetComponent<StatManager>());
        enemy.StartCoroutine("FreezeTimeFor", freezeTime);
    }

    private void StuckInto(Collider2D collision)
    {
        // Price
        if (pierceAmout > 0 && collision.GetComponent<Enemy>() != null)
        {
            SwordSkillDamege(collision.GetComponent<Enemy>());
            pierceAmout--;
            return;
        }

        // Spin
        if (isSprinng)
        {
            StopWhenSpin();
            return;
        }

        canRotate = false;
        collider2d.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //Bouce
        if (isReturning && enemyTarget.Count > 0) return;

        transform.parent = collision.transform;
        anim.SetBool("Active", false);
    }
}
