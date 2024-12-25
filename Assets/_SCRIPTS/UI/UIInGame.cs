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
        if (stat != null)
            stat.onHealthChanged += UpdateHealth;

        skill = SkillManager.instance;

    }
    private void Update()
    {
        if (momeyAmout < PlayerManager.instance.GetMoney())
            momeyAmout += Time.deltaTime * speedTomomeyAmout;
        else
            momeyAmout = PlayerManager.instance.GetMoney();


        //moneyText.text = PlayerManager.instance.GetMoney().ToString("#,#");

        moneyText.text = ((int)momeyAmout).ToString();

        if (Input.GetKeyDown(KeyCode.LeftShift) && skill.rollSkill.canRoll)
            SetCooldown(rollImage);
        if (Input.GetKeyDown(KeyCode.Q) && skill.crytalSkill.canScystal)
            SetCooldown(crystalImage);
        if (Input.GetKeyDown(KeyCode.Mouse1) && skill.swordSkill.unlocedSowrd)
            SetCooldown(swordImage);
        if (Input.GetKeyDown(KeyCode.R) && skill.backholiSkill.backhole)
            SetCooldown(backholeImage);
        //if(Input.GetKeyDown(KeyCode.Alpha1) && Inventory.instance.GetItemEquipment(EquipmentType.Flask) != null)
        //    SetCooldown(backholeImage);

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
