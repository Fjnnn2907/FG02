using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrytalSkill : Skill
{
    [SerializeField] private GameObject crystalFrefab;
    [SerializeField] private GameObject currentCrystal;

    [SerializeField] private float crystalDuraction;
    public override void UseSkill()
    {
        base.UseSkill();

        if(currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalFrefab,character.transform.position,Quaternion.identity);

            var newScriptCrystal = currentCrystal.GetComponent<CrytalSkillController>();

            newScriptCrystal.SetupScrystal(crystalDuraction);
        }
        else
        {
            character.transform.position = currentCrystal.transform.position;
            Destroy(currentCrystal);
        }
    }


}
