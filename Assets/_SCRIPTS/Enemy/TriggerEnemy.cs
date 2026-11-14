using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEnemy : MonoBehaviour
{
    private Enemy enemy;

    private void Awake()
    {
        enemy = GetComponentInParent<Enemy>();
    }
    void Start()
    {
        if (enemy == null)
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
        if (enemy == null || enemy.stats == null || enemy.stats.isDeah)
            return;

        if (collision.CompareTag("Player"))
        {
            CharacterStat targetStat = collision.GetComponent<CharacterStat>();
            if (targetStat != null)
                enemy.stats.DoDamage(targetStat);
        }
    }
    protected void OpenCounterAttack()
    {
        if (enemy != null)
            enemy.OpenCounterAttack();
    }
    protected void CloseCounterAttack()
    {
        if (enemy != null)
            enemy.CloseCounterAttack();
    }
}
