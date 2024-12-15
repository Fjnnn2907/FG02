using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CloneSkillController : MonoBehaviour
{
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
        if(cloneTimer <= 0)
        {
            sr.color = new Color(1,1,1,sr.color.a -(Time.deltaTime) * aColorSpeed);
            if (sr.color.a <= 0)
                Destroy(gameObject);

            //TODO: Pool
        }
    }
    public void SetUpClone(Transform _newTransform, float _cloneTime,bool _canAttack, Vector3 _offset,Transform _closetEnemy)
    {
        if(_canAttack)
            anim.SetInteger("CloneSkillNumber",Random.Range(1,3));
        transform.position = _newTransform.position + _offset;
        cloneTimer = _cloneTime;

        targetToEnemy = _closetEnemy;
        FaceCloneTargetEnemy();
    }
    public void TriggerAnimation()
    {
        cloneTimer = -.1f;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (collision.GetComponent<Enemy>() == null) return;
            collision.GetComponent<Enemy>().DamageEffect();
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
