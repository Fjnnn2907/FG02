using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CrytalSkill : Skill
{
    [SerializeField] private GameObject crystalFrefab;
    [SerializeField] private GameObject currentCrystal;

    [SerializeField] private float crystalDuraction;

    [Header("Scystal")]
    public bool canScystal;
    [SerializeField] private UISkillTreeSlot canScystalButton;

    [Header("Clone Crytals")]
    [SerializeField] private bool canCloneCrytals;
    [SerializeField] private UISkillTreeSlot canCloneCrytalsButton;

    [Header("Explosive")]
    [SerializeField] private bool canExplosive;
    [SerializeField] private UISkillTreeSlot canExplosiveButton;

    [Header("Moving")]
    [SerializeField] private bool canMoveToEnemy;
    [SerializeField] private float moveSpeed;
    [SerializeField] private UISkillTreeSlot canMoveToEnemyButton;
    [Header("Multi Crytal")]
    [SerializeField] private bool canUseMultiCrytal;
    [SerializeField] private int amoutStacks;
    [SerializeField] private float miutiStackCooldown;
    [SerializeField] private UISkillTreeSlot canUseMultiCrytalButton;
    [SerializeField] private List<GameObject> crystals = new List<GameObject>();
    

    protected override void Start()
    {
        base.Start();

        canScystalButton.GetComponent<Button>().onClick.AddListener(() => UnlocedCanScystal());
        canExplosiveButton.GetComponent<Button>().onClick.AddListener(() => UnlockedCanExplosive());
        canCloneCrytalsButton.GetComponent<Button>().onClick.AddListener(() => UnlockedCanCloneCrytals());
        canMoveToEnemyButton.GetComponent<Button>().onClick.AddListener(() => UnlocedCanMoveToEnemy());
        canUseMultiCrytalButton.GetComponent<Button>().onClick.AddListener(() => UnlockedCanUseMultiCrytal());  
    }
    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMultiCrytal())
            return;

        if(!canScystal)
            return;

        if (currentCrystal == null)
        {
            currentCrystal = Instantiate(crystalFrefab,character.transform.position,Quaternion.identity);

            var newScriptCrystal = currentCrystal.GetComponent<CrytalSkillController>();

            newScriptCrystal.SetupScrystal(crystalDuraction,canExplosive,canMoveToEnemy,moveSpeed, FindClosetEnemy(currentCrystal.transform), character);
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
                    FindClosetEnemy(newCrystal.transform),character);

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

#region Skill Tree
    private void UnlocedCanScystal()
    {
        if(canScystalButton.unlock)
            canScystal = true;
    }

    private void UnlockedCanExplosive()
    {
        if(canExplosiveButton.unlock)
            canExplosive = true;
    }
    private void UnlockedCanCloneCrytals()
    {
        if (canCloneCrytalsButton.unlock)
            canCloneCrytals = true;
    }
    private void UnlocedCanMoveToEnemy()
    {
        if(canMoveToEnemyButton.unlock)
            canMoveToEnemy = true;  
    }

    private void UnlockedCanUseMultiCrytal()
    {
        if(canUseMultiCrytalButton.unlock)
            canUseMultiCrytal = true;
    }
    #endregion
}
