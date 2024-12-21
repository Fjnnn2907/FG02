using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BuffType
{
    Dame,
    Giap,
}


[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/ItemEffect/Buff Effect")]
public class BuffEffect : ItemEffect
{
    private CharacterStat stats;
    [SerializeField]  private BuffType type;
    [SerializeField] private int buffAmout;
    [SerializeField] private float buffDuration;

    public override void ExecuteEffect(Transform _enemyPos)
    {
        stats = PlayerManager.instance.character.GetComponent<CharacterStat>();

        stats.BuffStats(buffAmout, buffDuration, StatToModify());
    }

    private Stat StatToModify()
    {
        if(type == BuffType.Dame)
            return stats.damage;
        else if(type == BuffType.Giap)
            return stats.giap;

        return null;
    }
}
