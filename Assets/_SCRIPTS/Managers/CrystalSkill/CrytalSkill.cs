using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CrytalSkill : Skill
{
    [SerializeField] private GameObject crystalFrefab;
    [SerializeField] private GameObject currentCrystal;

    [SerializeField] private float crystalDuraction;

    [Header("Clone Crytals")]
    [SerializeField] private bool canCloneCrytals;

    [Header("Explosive")]
    [SerializeField] private bool canExplosive;

    [Header("Moving")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;

    [Header("Multi Crytal")]
    [SerializeField] private bool canUseMultiCrytal;
    [SerializeField] private int amoutStacks;
    [SerializeField] private float miutiStackCooldown;
    [SerializeField] private List<GameObject> crystals = new List<GameObject>();
    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrytal())
            return;

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalFrefab,character.transform.position,Quaternion.identity);

            var newScriptCrystal = currentCrystal.GetComponent<CrytalSkillController>();

            newScriptCrystal.SetupScrystal(crystalDuraction,canExplosive,canMoveToEnemy,moveSpeed, FindClosetEnemy(currentCrystal.transform));
        }
        else
        {
            Vector2 characterPos = character.transform.position;

            character.transform.position = currentCrystal.transform.position;

            currentCrystal.transform.position = characterPos;

            if(canCloneCrytals)
            {
                SkillManager.instance.cloneSkill.CreateClone(currentCrystal.transform, Vector2.zero);
                Destroy(currentCrystal);
            }
            else
                currentCrystal.GetComponent<CrytalSkillController>()?.FinhishCrystal();
        }
    }
    private bool CanUseMultiCrytal()
    {
        if(canUseMultiCrytal)
        {
            if(crystals.Count > 0)
            {
                if (crystals.Count == amoutStacks)
                    Invoke("ResetAbillity", 1);

                cooldown = 0;
                GameObject crystalToSpawn = crystals[crystals.Count -1];
                GameObject newCrystal = Instantiate(crystalToSpawn, character.transform.position, Quaternion.identity);

                crystals.Remove(crystalToSpawn);

                newCrystal.GetComponent<CrytalSkillController>().SetupScrystal
                    (crystalDuraction, canExplosive, canMoveToEnemy, moveSpeed, 
                    FindClosetEnemy(newCrystal.transform));

                if (crystals.Count <= 0)
                {
                    cooldown = miutiStackCooldown;
                    RefillCrystal();
                }
            }
            
            return true;
        }

        return false;
    }
    public void RefillCrystal()
    {
        int amoutToAdd = amoutStacks - crystals.Count;
        for (int i = 0; i < amoutToAdd; i++)
        {
            crystals.Add(crystalFrefab);
        }
    }
    private void ResetAbillity()
    {
        if (cooldownTimer > 0)
            return;

        cooldownTimer = miutiStackCooldown;
        RefillCrystal();
    }
}
