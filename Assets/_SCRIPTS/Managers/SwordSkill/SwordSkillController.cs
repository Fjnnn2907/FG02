using System.Collections.Generic;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private CapsuleCollider2D collider2d;
    private Character character;

    [SerializeField] private float returnSpeed;
    private bool isReturning;
    private bool canRotate = true;

    [Header("Bouncing")]
    private bool isBounsing;
    [SerializeField]private float bounceSpeed;
    private int amoutOfBounce;
    public List<Transform> enemyTarget;
    private int targetIndex;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();
        collider2d = GetComponent<CapsuleCollider2D>();
    }
    public void SetUpSword(Vector2 _dir, float _gravity, Character _character)
    {
        character = _character;
        rb.velocity = _dir;
        rb.gravityScale = _gravity;

        anim.SetBool("Active", true);
    }
    public void SetupBouce(bool _isBoucing, int _amoutOfBouce)
    {
        isBounsing = _isBoucing;
        amoutOfBounce = _amoutOfBouce;

        enemyTarget = new List<Transform>();
    }
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
            Vector2 directionToPlayer = (character.transform.position - transform.position);
            transform.right = directionToPlayer;

            transform.position = Vector2.MoveTowards
                (transform.position, character.transform.position, returnSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, character.transform.position) < .5f)
                character.CleanSword();
        }

        BouceLogic();
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
                targetIndex++;
                amoutOfBounce--;
                if (amoutOfBounce <= 0)
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

    private void StuckInto(Collider2D collision)
    {
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
