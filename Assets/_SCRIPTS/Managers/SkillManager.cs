using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public static SkillManager instance;
    public RollSkill rollSkill { get; private set;}
    public CloneSkill cloneSkill { get; private set;}
    public SwordSkill swordSkill { get; private set;}
    #region Singleton
    private void Awake()
    {
        if(instance != null)
            Debug.LogWarning("Erro Singleton" + instance.name);
        else
            instance = this;
    }
    #endregion
    private void Start()
    {
        rollSkill = GetComponent<RollSkill>();
        cloneSkill = GetComponent<CloneSkill>();
        swordSkill = GetComponent<SwordSkill>();
    }
}
