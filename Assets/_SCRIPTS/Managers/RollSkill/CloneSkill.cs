using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CloneSkill : Skill
{
    [Header("Clone")]
    [SerializeField] private GameObject cloneSkill;
    [SerializeField] private float cloneTime = 1.2f;
    //[SerializeField] private bool canAttack;
    public bool canAttack;

    [SerializeField] private UISkillTreeSlot cloneButton;

    [SerializeField] private bool createCloneOnRollStart;
    [SerializeField] private bool createCloneOnRollOver;
    // conect to CharacterRollState
    public void CreateClone(Transform _clonePositon, Vector3 _offset)
    {
        GameObject newClone = Instantiate(cloneSkill);

        newClone.GetComponent<CloneSkillController>().SetUpClone(_clonePositon,cloneTime, canAttack, _offset, FindClosetEnemy(cloneSkill.transform),character);
    }

    protected override void Start()
    {
        base.Start();

        cloneButton.GetComponent<Button>().onClick.AddListener(() => CanCloneAttack());
    }
    
    private void CanCloneAttack()
    {
        if(cloneButton.unlock)
            canAttack = true;
    }
    protected override void CheckUnlock()
    {
        CanCloneAttack();
    }
}
