using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    [Header("Healbar")]
    [SerializeField] private Slider slider;
    [SerializeField] private CharacterStat stat;

    [Header("Image SKill Cooldown")]
    [SerializeField] private Image rollImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image backholeImage;
    private SkillManager skill;

    [Header("Money")]
    [SerializeField] private TextMeshProUGUI moneyText;
    [SerializeField] private float momeyAmout;
    [SerializeField] private float speedTomomeyAmout = 1000;

    private void Start()
    {
        if (stat == null && PlayerManager.instance != null && PlayerManager.instance.character != null)
            stat = PlayerManager.instance.character.GetComponent<CharacterStat>();

        if (stat != null)
        {
            stat.onHealthChanged += UpdateHealth;
            UpdateHealth();
        }

        skill = SkillManager.instance;
    }
    private void OnDestroy()
    {
        if (stat != null)
            stat.onHealthChanged -= UpdateHealth;
    }
    private void Update()
    {
        if (PlayerManager.instance != null && moneyText != null)
        {
            int targetMoney = PlayerManager.instance.GetMoney();
            if (momeyAmout < targetMoney)
                momeyAmout = Mathf.Min(targetMoney, momeyAmout + Time.deltaTime * speedTomomeyAmout);
            else
                momeyAmout = targetMoney;

            moneyText.text = ((int)momeyAmout).ToString();
        }

        if (skill == null)
            skill = SkillManager.instance;
        if (skill == null)
            return;

        SyncSkillCooldown(rollImage, skill.rollSkill);
        SyncSkillCooldown(crystalImage, skill.crytalSkill);
        SyncSkillCooldown(swordImage, skill.swordSkill);
        SyncSkillCooldown(backholeImage, skill.backholiSkill);
    }
    private void UpdateHealth()
    {
        if (slider == null || stat == null)
            return;

        slider.maxValue = stat.GetMaxHealthValue();
        slider.value = stat.currentHealth;
    }

    private void SyncSkillCooldown(Image _image, Skill _skill)
    {
        if (_image == null || _skill == null)
            return;

        if (_skill.cooldown > 0f && _skill.cooldownTimer > 0f)
            _image.fillAmount = Mathf.Clamp01(_skill.cooldownTimer / _skill.cooldown);
        else
            _image.fillAmount = 0f;
    }
}
