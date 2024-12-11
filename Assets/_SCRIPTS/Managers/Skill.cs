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
        if(cooldownTimer <= 0)
        {
            UseSkill();
            cooldownTimer = cooldown;
            return true;
        }

        return false;
    }
    public virtual void UseSkill()
    {

    }
}
