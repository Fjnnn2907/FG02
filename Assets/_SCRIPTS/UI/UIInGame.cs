using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIInGame : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private CharacterStat stat;


    [SerializeField] private Image rollImage;
    [SerializeField] private Image swordImage;
    [SerializeField] private Image crystalImage;
    [SerializeField] private Image backholeImage;
    private SkillManager skill;

    private void Start()
    {
        if (stat != null)
            stat.onHealthChanged += UpdateHealth;

        skill = SkillManager.instance;

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            SetCooldown(rollImage);
        if (Input.GetKeyDown(KeyCode.Q))
            SetCooldown(crystalImage);
        if (Input.GetKeyDown(KeyCode.Mouse1))
            SetCooldown(swordImage);
        if (Input.GetKeyDown(KeyCode.R))
            SetCooldown(backholeImage);

        CheckCooldown(rollImage, skill.rollSkill.cooldown);
        CheckCooldown(crystalImage, skill.crytalSkill.cooldown);
        CheckCooldown(swordImage, skill.swordSkill.cooldown);
        CheckCooldown(backholeImage, skill.backholiSkill.cooldown);
    }
    private void UpdateHealth()
    {
        slider.maxValue = stat.MaxHealth.GetValue();
        slider.value = stat.currentHealth;
    }

    private void SetCooldown(Image _image)
    {
        if(_image.fillAmount <= 0)
            _image.fillAmount = 1;
    }
    private void CheckCooldown(Image _image, float _cooldown)
    {
        if(_image.fillAmount > 0)
            _image.fillAmount -= 1 / _cooldown * Time.deltaTime;
    }
}
