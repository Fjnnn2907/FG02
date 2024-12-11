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

 
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        collider2d = GetComponent<BoxCollider2D>();
    }
    public void SetUpSword(Vector2 _dir, float _gravity)
    {
        rb.velocity = _dir;
        rb.gravityScale = _gravity;
    }
}
