using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvenTriggerAnim : MonoBehaviour
{
    Character character => GetComponentInParent<Character>();
    public void TriggerAnimation()
    {
        character.AnimationTrigger();
    }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.CompareTag("Enemy"))
    //    {
    //        collision.GetComponent
    //    }
    //}
}
