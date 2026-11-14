using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStatValue;
    [SerializeField] private BuffType buffType;

    private void OnEnable()
    {
        UpdateStatValue();
    }
    private void Start()
    {
        UpdateStatValue();
    }
    public void UpdateStatValue()
    {
        if (textStatValue == null || PlayerManager.instance == null || PlayerManager.instance.character == null)
            return;

        CharacterStat character = PlayerManager.instance.character.GetComponent<CharacterStat>();

        if (character != null)
        {
            Stat stat = character.StatToModify(buffType);
            if (stat != null)
                textStatValue.text = stat.GetValue().ToString();
        }
    }
}
