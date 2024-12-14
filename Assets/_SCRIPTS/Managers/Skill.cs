using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill : MonoBehaviour
{
    [SerializeField] protected float cooldown = 5f;
    protected float cooldownTimer;
    
    protected Character character;

    protected virtual void Start()
    {
        character = PlayerManager.instance.character;
    }
    protected virtual void Update()
    {
        cooldownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(cooldownTimer < 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }
        Debug.Log("Skill on colldown");
        return false;
    }
    public virtual void UseSkill()
    {
        
    }

    protected virtual Transform FindClosetEnemy(Transform _checkTransform)
    {
        Collider2D[] collider = Physics2D.OverlapCircleAll(_checkTransform.position, 25);

        Transform targetToEnemy = null;

        float cloneDistance = Mathf.Infinity;
        foreach (Collider2D hit in collider)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distanceToEnemy = Vector2.Distance(_checkTransform.position, hit.transform.position);
                // ke thu gan nhat
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
