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
    private void Start()
    {
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

        transform.position = Vector2.MoveTowards(transform.position,targetStats.transform.position,speed * Time.deltaTime);
        transform.right = transform.position - targetStats.transform.position;

        if(Vector2.Distance(transform.position, targetStats.transform.position) < .1f)
        {

            anim.transform.localPosition = new Vector2(0, .5f);
            anim.transform.localRotation = Quaternion.identity;

            transform.localScale = new Vector2(3, 3);
            transform.localRotation = Quaternion.identity;

            Invoke("DamgeAndDesTroy", .2f);
            trigged = true;
            anim.SetTrigger("Hit");
        }
    }

    private void DamgeAndDesTroy()
    {
        targetStats.TakeDamage(damage);
        Destroy(gameObject, .4f);
    }
}
