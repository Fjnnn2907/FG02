using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalSkillController : MonoBehaviour
{
    private Animator aim;
    private CircleCollider2D cd;
    private float crystalDuraction;
    private Character character;

    private void Awake()
    {
        aim = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }

    [Header("Explosive")]
    private bool canExplosive;
    private bool canGrow;
    private float growSpeed = 3.5f;
    [Header("Moving")]
    private bool canMoveToEnemy;
    private float moveSpeed;

    private Transform closetEnemy;
    public bool IsFinishing { get; private set; }

    public void SetupScrystal(float _crystalDuraction, bool _canExplosive, bool _canMoveToEnemy, float _moveSpeed, Transform _closetEnemy, Character _character)
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

        if (crystalDuraction < 0 && !IsFinishing)
        {
            FinhishCrystal();
        }

        if (canMoveToEnemy && !IsFinishing)
        {
            if (closetEnemy != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, closetEnemy.position, moveSpeed * Time.deltaTime);

                if (Vector2.Distance(transform.position, closetEnemy.position) < 1)
                {
                    FinhishCrystal();
                    canMoveToEnemy = false;
                }
            }
        }

        if (canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(3, 3), growSpeed * Time.deltaTime);
    }
    public void ExplodeCrystal()
    {
        float worldRadius = cd != null ? cd.radius * Mathf.Max(transform.lossyScale.x, transform.lossyScale.y) : 1.5f;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, worldRadius);

        foreach (Collider2D hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                if (character != null && character.stats != null)
                    character.stats.DoMagicDamage(hit.GetComponent<StatManager>());

                if (Inventory.instance != null)
                {
                    var itemEqupment = Inventory.instance.GetItemEquipment(EquipmentType.Non);
                    if (itemEqupment != null)
                        itemEqupment.ItemEffect(hit.transform);
                }
            }
        }
    }
    public void FinhishCrystal()
    {
        if (IsFinishing)
            return;
        IsFinishing = true;

        if (canExplosive && aim != null)
        {
            canGrow = true;
            aim.SetTrigger("Explode");
            Destroy(gameObject, 1.5f);
        }
        else
            SeftDestroy();
    }

    public void SeftDestroy()
    {
        Destroy(gameObject);
    }
}
