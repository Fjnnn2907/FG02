using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/ItemEffect/Heal Effect")]
public class HealthEffect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healthPercent;

    public override void ExecuteEffect(Transform _enemyPos)
    {
        if (PlayerManager.instance == null || PlayerManager.instance.character == null)
            return;

        CharacterStat character = PlayerManager.instance.character.GetComponent<CharacterStat>();
        if (character == null)
            return;

        int amout = Mathf.RoundToInt(character.GetMaxHealthValue() * healthPercent);

        character.HoiPhucMau(amout);
    }
}
