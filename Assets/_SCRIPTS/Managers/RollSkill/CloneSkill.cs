using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloneSkill : Skill
{
    [Header("Clone")]
    [SerializeField] private GameObject cloneSkill;
    [SerializeField] private float cloneTime = 1.2f;
    [SerializeField] private bool canAttack;

    [SerializeField] private bool createCloneOnRollStart;
    [SerializeField] private bool createCloneOnRollOver;
    // conect to CharacterRollState
    public void CreateClone(Transform _clonePositon, Vector3 _offset)
    {
        GameObject newClone = Instantiate(cloneSkill);

        newClone.GetComponent<CloneSkillController>().SetUpClone(_clonePositon,cloneTime, canAttack, _offset, FindClosetEnemy(cloneSkill.transform),character);
    }
    
    public void CreateCloneOnRollBegun()
    {
        if (createCloneOnRollStart)
            CreateClone(character.transform,Vector2.zero);
    }
    public void CreateCloneOnRollOver()
    {
        if (createCloneOnRollOver)
            CreateClone(character.transform, Vector2.zero);
    }
}
