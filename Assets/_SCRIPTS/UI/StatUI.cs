using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textStatValue;
    [SerializeField] private BuffType buffType;

    private void Start()
    {
        UpdateStatValue();
    }
    public void UpdateStatValue()
    {
        CharacterStat character = PlayerManager.instance.character.GetComponent<CharacterStat>();

        if(character != null)
        {
            textStatValue.text = character.StatToModify(buffType).GetValue().ToString();
        }
    }
}
