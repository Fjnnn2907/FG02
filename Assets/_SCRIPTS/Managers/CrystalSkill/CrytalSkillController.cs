using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalSkillController : MonoBehaviour
{
    private Animator aim => GetComponent<Animator>();
    private CircleCollider2D cd => GetComponent<CircleCollider2D>();
    private float crystalDuraction;
    private Character character;

    [Header("Explosive")]
    private bool canExplosive;
    private bool canGrow;
    private float growSpeed = 3.5f;
    [Header("Moving")]
    private bool canMoveToEnemy;
    private float moveSpeed;

    private Transform closetEnemy;
    public void SetupScrystal(float _crystalDuraction, bool _canExplosive, bool _canMoveToEnemy, float _moveSpeed,Transform _closetEnemy, Character _character)
    {
        crystalDuraction = _crystalDuraction;
        canExplosive = _canExplosive;
        canMoveToEnemy = _canMoveToEnemy;
        moveSpeed = _moveSpeed;
        closetEnemy = _closetEnemy;
        character = _character;
    }
    private void Update()
    {
        crystalDuraction -= Time.deltaTime;

        if (crystalDuraction < 0)
        {
            FinhishCrystal();
        }

        if (canMoveToEnemy)
        {
            transform.position = Vector2.MoveTowards(transform.position,closetEnemy.position,moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, closetEnemy.position) < 1)
                FinhishCrystal();
        }

        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growSpeed * Time.deltaTime);

    }
    public void ExplodeCrystal()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach (Collider2D hit in colliders)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                character.stats.DoMagicDamage(hit.GetComponent<StatManager>());
            }
        }
    }
    public void FinhishCrystal()
    {
        if (canExplosive)
        {
            canGrow = true;
            aim.SetTrigger("Explode");
        }
        else
            SeftDestroy();
    }

    public void SeftDestroy()
    {
        Destroy(gameObject);
    }
}
