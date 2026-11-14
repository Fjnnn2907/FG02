using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/ItemEffect/Buff Effect")]
public class BuffEffect : ItemEffect
{
    [SerializeField] private BuffType type;
    [SerializeField] private int buffAmout;
    [SerializeField] private float buffDuration;

    public override void ExecuteEffect(Transform _enemyPos)
    {
        if (PlayerManager.instance == null || PlayerManager.instance.character == null)
            return;

        CharacterStat stats = PlayerManager.instance.character.GetComponent<CharacterStat>();
        if (stats != null)
            stats.BuffStats(buffAmout, buffDuration, stats.StatToModify(type));
    }
}
