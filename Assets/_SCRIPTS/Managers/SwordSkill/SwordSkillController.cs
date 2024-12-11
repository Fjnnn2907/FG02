using System.Collections;
using System.Collections.Generic;
using System.Xml.Schema;
using UnityEngine;

public class SwordSkillController : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private BoxCollider2D collider2d;
    private Character character;

    [SerializeField] private float returnSpeed;
    private bool isReturning;
    private bool canRotate = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D not found on SwordSkillController");
        }
        anim = GetComponent<Animator>();
        collider2d = GetComponent<BoxCollider2D>();
    }
    public void SetUpSword(Vector2 _dir, float _gravity, Character _character)
    {
        character = _character; 
        rb.velocity = _dir;
        rb.gravityScale = _gravity;
    }
    public void ReturnSword()
    {
        if (isReturning) return; 

        transform.parent = null;
        isReturning = true;
        rb.isKinematic = false;
    }
    private void Update()
    {
        if(canRotate)
            transform.right = rb.velocity;

        if (isReturning)
        {
            transform.position = Vector2.MoveTowards
                (transform.position,character.transform.position,returnSpeed * Time.deltaTime);
            if(Vector2.Distance(transform.position,character.transform.position) < .5f)
            {
                character.CleanSword();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        canRotate = false;
        collider2d.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        transform.parent = collision.transform;
    }
}
