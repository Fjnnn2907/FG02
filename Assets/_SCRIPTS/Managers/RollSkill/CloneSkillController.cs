using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkillController : MonoBehaviour
{
    private Character character;
    private SpriteRenderer sr;
    private Animator anim;
    [SerializeField] private float aColorSpeed = 1;
    private float cloneTimer;
    private Transform targetToEnemy;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        cloneTimer -= Time.deltaTime;
        if (cloneTimer <= 0 && sr != null)
        {
            sr.color = new Color(1, 1, 1, sr.color.a - Time.deltaTime * aColorSpeed);
            if (sr.color.a <= 0)
                Destroy(gameObject);
        }
    }
    public void SetUpClone(Transform _newTransform, float _cloneTime, bool _canAttack, Vector3 _offset, Transform _closetEnemy, Character _character)
    {
        if (_newTransform != null)
            transform.position = _newTransform.position + _offset;

        if (_canAttack && anim != null)
            anim.SetInteger("CloneSkillNumber", Random.Range(1, 4));

        cloneTimer = _cloneTime;
        character = _character;
        targetToEnemy = _closetEnemy;
        FaceCloneTargetEnemy();
    }
    public void TriggerAnimation()
    {
        cloneTimer = -.1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Enemy enemy = collision.GetComponent<Enemy>();
        if (enemy != null && character != null && character.stats != null)
        {
            StatManager enemyStats = collision.GetComponent<StatManager>();
            if (enemyStats != null)
            {
                character.stats.DoDamage(enemyStats);
                character.stats.DoMagicDamage(enemyStats);
            }
        }
    }
    private void FaceCloneTargetEnemy()
    {
        

        if(targetToEnemy != null)
        {
            if (transform.position.x > targetToEnemy.position.x)
                transform.Rotate(0, 180, 0);
        }
    }
}
