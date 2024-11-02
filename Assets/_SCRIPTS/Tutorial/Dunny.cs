using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dunny : MonoBehaviour
{
    private Animator anim;
    public GameObject effect;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Attack"))
        {
            anim.SetTrigger("IsHurt");
            StartCoroutine(Effect());
        }
    }
    IEnumerator Effect()
    {
        effect.SetActive(true);
        yield return new WaitForSeconds(.6f);
        effect.SetActive(false);
    }
}
