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
        rb.linearVelocity = _dir;
        rb.gravityScale = _gravity;
        freezeTime = _freeTime;
        returnSpeed = _returnSpeed;

        if (pierceAmout <= 0)
            anim.SetBool("Active", true);

        spinDir = Mathf.Clamp(rb.linearVelocity.x, -1f, 1f);
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
        if (rb != null)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
        isBounsing = false;
        isSprinng = false;
        isReturning = true;
    }
    private void Update()
    {
        if (character == null)
        {
            Destroy(gameObject);
            return;
        }

        if (canRotate && rb != null && rb.linearVelocity.sqrMagnitude > 0.001f)
            transform.right = rb.linearVelocity;

        if (Vector2.Distance(transform.position, character.transform.position) > 30)
        {
            Destroy(gameObject);
            return;
        }

        if (isReturning)
        {
            canRotate = false;

            transform.position = Vector2.MoveTowards(
                transform.position, character.transform.position, returnSpeed * Time.deltaTime);

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
                    ReturnSword();
                    return;
                }

                hitTimer -= Time.deltaTime;
                if (hitTimer < 0)
                {
                    hitTimer = hitCooldown;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                    foreach (Collider2D hit in colliders)
                    {
                        Enemy hitEnemy = hit.GetComponent<Enemy>();
                        if (hitEnemy != null)
                            SwordSkillDamege(hitEnemy);
                    }
                }
            }
        }
    }

    private void StopWhenSpin()
    {
        wasStopped = true;
        if (rb != null)
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        spinTimer = spinDuration;
    }

    private void BouceLogic()
    {
        if (isBounsing && enemyTarget != null)
        {
            enemyTarget.RemoveAll(t => t == null);
            if (enemyTarget.Count <= 0)
            {
                ReturnSword();
                return;
            }

            if (targetIndex >= enemyTarget.Count)
                targetIndex = 0;

            if (anim != null)
                anim.SetBool("Active", true);

            Transform currentTarget = enemyTarget[targetIndex];
            transform.position = Vector2.MoveTowards(transform.position,
                currentTarget.position, bounceSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, currentTarget.position) < .1f)
            {
                Enemy hitEnemy = currentTarget.GetComponent<Enemy>();
                if (hitEnemy != null)
                    SwordSkillDamege(hitEnemy);

                targetIndex++;
                bouceAmout--;
                if (bouceAmout <= 0 || enemyTarget.Count <= 1)
                {
                    ReturnSword();
                    return;
                }

                if (targetIndex >= enemyTarget.Count)
                    targetIndex = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning) return;

        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (isBounsing && enemyTarget != null && enemyTarget.Count <= 0)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                foreach (Collider2D hit in colliders)
                {
                    Enemy nearby = hit.GetComponent<Enemy>();
                    if (nearby != null && (nearby.stats == null || !nearby.stats.isDeah) && !enemyTarget.Contains(nearby.transform))
                        enemyTarget.Add(nearby.transform);
                }
            }
        }

        StuckInto(collision);
    }

    private void SwordSkillDamege(Enemy enemy)
    {
        if (enemy == null || character == null || character.stats == null)
            return;

        var enemyTarget = enemy.GetComponent<StatManager>();
        if (enemyTarget == null || enemyTarget.isDeah)
            return;

        character.stats.DoDamage(enemyTarget);
        character.stats.DoMagicDamage(enemyTarget);

        if (character.skill != null && character.skill.swordSkill != null)
        {
            if (character.skill.swordSkill.timeStop)
                enemy.StartCoroutine("FreezeTimeFor", freezeTime);

            if (character.skill.swordSkill.xuyenGiap)
                enemyTarget.LayXuyenGiap(freezeTime);
        }

        if (Inventory.instance != null)
        {
            var itemEqupment = Inventory.instance.GetItemEquipment(EquipmentType.Non);
            if (itemEqupment != null)
                itemEqupment.ItemEffect(enemy.transform);
        }
    }

    private void StuckInto(Collider2D collision)
    {
        Enemy hitEnemy = collision.GetComponent<Enemy>();

        if (pierceAmout > 0 && hitEnemy != null)
        {
            SwordSkillDamege(hitEnemy);
            pierceAmout--;
            return;
        }

        if (isSprinng)
        {
            StopWhenSpin();
            return;
        }

        canRotate = false;
        if (collider2d != null)
            collider2d.enabled = false;
        if (rb != null)
        {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        if (isBounsing && enemyTarget != null && enemyTarget.Count > 0)
            return;

        if (hitEnemy != null)
            SwordSkillDamege(hitEnemy);

        transform.parent = collision.transform;
        if (anim != null)
            anim.SetBool("Active", false);
    }
}
