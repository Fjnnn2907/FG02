using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new ItemData", menuName = "Data/ItemEffct")]
public class ItemEffect : ScriptableObject
{
    public virtual void ExecuteEffect(Transform _enemyPos)
    {
        Debug.Log("a");
    }
}
