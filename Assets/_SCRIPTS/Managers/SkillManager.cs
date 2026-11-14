using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public RollSkill rollSkill { get; private set;}
    public CloneSkill cloneSkill { get; private set;}
    public SwordSkill swordSkill { get; private set;}
    public BackholeSkill backholiSkill {  get; private set;}
    public CrytalSkill crytalSkill { get; private set;}
    #region Singleton
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;

        rollSkill = GetComponent<RollSkill>();
        cloneSkill = GetComponent<CloneSkill>();
        swordSkill = GetComponent<SwordSkill>();
        backholiSkill = GetComponent<BackholeSkill>();
        crytalSkill = GetComponent<CrytalSkill>();
    }
    #endregion
    private void Start()
    {
        if (rollSkill == null) rollSkill = GetComponent<RollSkill>();
        if (cloneSkill == null) cloneSkill = GetComponent<CloneSkill>();
        if (swordSkill == null) swordSkill = GetComponent<SwordSkill>();
        if (backholiSkill == null) backholiSkill = GetComponent<BackholeSkill>();
        if (crytalSkill == null) crytalSkill = GetComponent<CrytalSkill>();
    }
}
