using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/ItemEffect/Hieu Ung Dien")]
public class HieuUngDien : ItemEffect
{
    [SerializeField] private GameObject DienPrefab;
    public override void ExecuteEffect(Transform _enemyPos)
    {
        if (DienPrefab == null)
            return;

        Transform target = _enemyPos;
        if (target == null && PlayerManager.instance != null && PlayerManager.instance.character != null)
            target = PlayerManager.instance.character.transform;

        if (target == null)
            return;

        GameObject newObj = Instantiate(DienPrefab, target.position, Quaternion.identity);

        Destroy(newObj, 1);
    }
}
