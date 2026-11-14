using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TiaSetController : MonoBehaviour
{
    [SerializeField] private StatManager targetStats;
    [SerializeField] private float speed;
    [SerializeField] private int damage;

    private Animator anim;
    private bool trigged;
    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }
    private void Start()
    {
        if (anim == null)
            anim = GetComponentInChildren<Animator>();
    }
    public void SetUpTiaSet(int _damage, StatManager _targetStats)
    {
        damage = _damage;
        targetStats = _targetStats;
    }
    private void Update()
    {
        if (trigged) return;

        if (targetStats == null || targetStats.isDeah)
        {
            Destroy(gameObject);
            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, targetStats.transform.position, speed * Time.deltaTime);
        Vector2 delta = transform.position - targetStats.transform.position;
        if (delta.sqrMagnitude > 0.0001f)
            transform.right = delta;

        if (Vector2.Distance(transform.position, targetStats.transform.position) < .1f)
        {
            if (anim != null)
            {
                anim.transform.localPosition = new Vector2(0, .5f);
                anim.transform.localRotation = Quaternion.identity;
                anim.SetTrigger("Hit");
            }

            transform.localScale = new Vector2(3, 3);
            transform.localRotation = Quaternion.identity;

            Invoke("DamgeAndDesTroy", .2f);
            trigged = true;
        }
    }

    private void DamgeAndDesTroy()
    {
        if (targetStats != null && !targetStats.isDeah)
            targetStats.TakeDamage(damage);

        Destroy(gameObject, .4f);
    }
}
