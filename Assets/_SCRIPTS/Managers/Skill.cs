using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    public float cooldown = 5f;
    public float cooldownTimer;
    
    protected Character character;

    protected virtual void Start()
    {
        if (PlayerManager.instance != null)
            character = PlayerManager.instance.character;

        CheckUnlock();
    }
    protected virtual void Update()
    {
        if (cooldownTimer > -1f)
            cooldownTimer -= Time.deltaTime;
    }

    public void RefreshUnlockState() => CheckUnlock();

    protected virtual bool IsSkillUnlocked() => true;

    public virtual bool CanUseSkill()
    {
        if (!IsSkillUnlocked())
            return false;

        if (cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        return false;
    }
    public virtual void UseSkill()
    {
        if (character == null && PlayerManager.instance != null)
            character = PlayerManager.instance.character;
    }
    protected virtual void CheckUnlock()
    {

    }
    protected virtual Transform FindClosetEnemy(Transform _checkTransform)
    {
        if (_checkTransform == null)
            return null;

        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        Transform targetToEnemy = null;

        float cloneDistance = Mathf.Infinity;
        foreach (Collider2D hit in collider)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null && (enemy.stats == null || !enemy.stats.isDeah))
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);
                if (distanceToEnemy < cloneDistance)
                {
                    cloneDistance = distanceToEnemy;
                    targetToEnemy = hit.transform;
                }
            }
        }

        return targetToEnemy;
    }
}
