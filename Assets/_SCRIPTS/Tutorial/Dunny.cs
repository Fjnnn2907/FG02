using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dunny : MonoBehaviour
{
    private Animator anim;
    public GameObject effect;
    private Coroutine effectCoroutine;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
    private void Start()
    {
        if (anim == null)
            anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            if (anim != null)
                anim.SetTrigger("IsHurt");

            if (effect != null)
            {
                if (effectCoroutine != null)
                    StopCoroutine(effectCoroutine);
                effectCoroutine = StartCoroutine(Effect());
            }
        }
    }
    IEnumerator Effect()
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(.6f);
        if (effect != null)
            effect.SetActive(false);
        effectCoroutine = null;
    }
}
