using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemEquipment", menuName = "Data/ItemEffect/Hieu Ung Dien")]
public class HieuUngDien : ItemEffect
{
    [SerializeField] private GameObject DienPrefab;
    public override void ExecuteEffect(Transform _enemyPos)
    {
        GameObject newObj = Instantiate(DienPrefab, _enemyPos.position,Quaternion.identity);

        Destroy(newObj,1);
    }
}
