using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/ItemEffect/Heal Effect")]
public class HealthEffect : ItemEffect
{
    [Range(0f, 1f)]
    [SerializeField] private float healthPercent;

    public override void ExecuteEffect(Transform _enemyPos)
    {
        CharacterStat character = PlayerManager.instance.character.GetComponent<CharacterStat>();

        int amout = Mathf.RoundToInt(character.GetMaxHealthValue() * healthPercent);

        character.HoiPhucMau(amout);
    }
}
