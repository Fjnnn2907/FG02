using System.Collections;
using System.Collections.Generic;
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

    private float baseCooldown;

    protected override bool IsSkillUnlocked() => canScystal;

    protected override void Start()
    {
        baseCooldown = cooldown;
        base.Start();

        if (canScystalButton != null)
            canScystalButton.GetComponent<Button>()?.onClick.AddListener(() => UnlocedCanScystal());
        if (canExplosiveButton != null)
            canExplosiveButton.GetComponent<Button>()?.onClick.AddListener(() => UnlockedCanExplosive());
        if (canCloneCrytalsButton != null)
            canCloneCrytalsButton.GetComponent<Button>()?.onClick.AddListener(() => UnlockedCanCloneCrytals());
        if (canMoveToEnemyButton != null)
            canMoveToEnemyButton.GetComponent<Button>()?.onClick.AddListener(() => UnlocedCanMoveToEnemy());
        if (canUseMultiCrytalButton != null)
            canUseMultiCrytalButton.GetComponent<Button>()?.onClick.AddListener(() => UnlockedCanUseMultiCrytal());
    }
    public override void UseSkill()
    {
        base.UseSkill();

        if (!canScystal || character == null || crystalFrefab == null)
            return;

        if (CanUseMultiCrytal())
            return;

        cooldown = baseCooldown;

        var existingCtrl = currentCrystal != null ? currentCrystal.GetComponent<CrytalSkillController>() : null;
        if (currentCrystal == null || (existingCtrl != null && existingCtrl.IsFinishing))
        {
            currentCrystal = Instantiate(crystalFrefab, character.transform.position, Quaternion.identity);

            var newScriptCrystal = currentCrystal.GetComponent<CrytalSkillController>();
            if (newScriptCrystal != null)
                newScriptCrystal.SetupScrystal(crystalDuraction, canExplosive, canMoveToEnemy, moveSpeed, FindClosetEnemy(currentCrystal.transform), character);
        }
        else
        {
            if (canMoveToEnemy)
                return;

            Vector2 characterPos = character.transform.position;
            character.transform.position = currentCrystal.transform.position;
            currentCrystal.transform.position = characterPos;

            if (canCloneCrytals)
            {
                if (SkillManager.instance != null && SkillManager.instance.cloneSkill != null)
                    SkillManager.instance.cloneSkill.CreateClone(currentCrystal.transform, Vector2.zero);
                Destroy(currentCrystal);
                currentCrystal = null;
            }
            else
            {
                existingCtrl?.FinhishCrystal();
                currentCrystal = null;
            }
        }
    }
    private bool CanUseMultiCrytal()
    {
        if (canUseMultiCrytal)
        {
            if (crystals.Count == 0)
                RefillCrystal();

            if (crystals.Count > 0)
            {
                if (crystals.Count == amoutStacks)
                    Invoke("ResetAbillity", 4f);

                cooldown = 0;
                GameObject crystalToSpawn = crystals[crystals.Count - 1];
                crystals.RemoveAt(crystals.Count - 1);

                if (crystalToSpawn == null)
                    crystalToSpawn = crystalFrefab;

                GameObject newCrystal = Instantiate(crystalToSpawn, character.transform.position, Quaternion.identity);

                newCrystal.GetComponent<CrytalSkillController>()?.SetupScrystal(
                    crystalDuraction, canExplosive, canMoveToEnemy, moveSpeed,
                    FindClosetEnemy(newCrystal.transform), character);

                if (crystals.Count <= 0)
                {
                    CancelInvoke("ResetAbillity");
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
        if (crystalFrefab == null || amoutStacks <= 0)
            return;

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

    protected override void CheckUnlock()
    {
        UnlocedCanScystal();
        UnlockedCanExplosive();
        UnlockedCanCloneCrytals();
        UnlocedCanMoveToEnemy();
        UnlockedCanUseMultiCrytal();
    }
    private void UnlocedCanScystal()
    {
        if (canScystalButton != null && canScystalButton.unlock)
            canScystal = true;
    }

    private void UnlockedCanExplosive()
    {
        if (canExplosiveButton != null && canExplosiveButton.unlock)
            canExplosive = true;
    }
    private void UnlockedCanCloneCrytals()
    {
        if (canCloneCrytalsButton != null && canCloneCrytalsButton.unlock)
            canCloneCrytals = true;
    }
    private void UnlocedCanMoveToEnemy()
    {
        if (canMoveToEnemyButton != null && canMoveToEnemyButton.unlock)
            canMoveToEnemy = true;
    }

    private void UnlockedCanUseMultiCrytal()
    {
        if (canUseMultiCrytalButton != null && canUseMultiCrytalButton.unlock)
        {
            canUseMultiCrytal = true;
            if (crystals.Count == 0)
                RefillCrystal();
        }
    }
    #endregion
}
