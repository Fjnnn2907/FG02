using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone")]
    [SerializeField] private GameObject cloneSkill;
    [SerializeField] private float cloneTime = 1.2f;
    [SerializeField] private bool canAttack;
    // conect to CharacterRollState
    public void CreateClone(Transform _clonePositon)
    {
        GameObject newClone = Instantiate(cloneSkill);

        newClone.GetComponent<CloneSkillController>().SetUpClone(_clonePositon,cloneTime, canAttack);
    }
}
