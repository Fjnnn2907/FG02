using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class TriggerEnemy : MonoBehaviour
{
    private Enemy enemy;

    void Start()
    {
        enemy = GetComponentInParent<Enemy>();
    }

    public void AnimationTrigger()
    {
        if (enemy != null)
        {
            enemy.AnimationTrigger();
        }
        else
        {
            Debug.LogWarning("Sekeleton component not found in parent.");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CharacterStat targetStat = collision.GetComponent<CharacterStat>();
            enemy.stats.DoDamage(targetStat);
        }
    }
    protected void OpenCounterAttack()
    {
        enemy.OpenCounterAttack();
    }
    protected void CloseCounterAttack()
    {
        enemy.CloseCounterAttack();
    }
}
