using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DienController : MonoBehaviour
{
    private CharacterStat CharacterStat;
    private void Start()
    {
        CharacterStat = PlayerManager.instance.character.GetComponent<CharacterStat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            Debug.Log("a");
            EnemyStat enemy = collision.gameObject.GetComponent<EnemyStat>();
            
            CharacterStat.DoMagicDamage(enemy);

        }
    }
}
