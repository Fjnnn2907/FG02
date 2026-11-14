using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DienController : MonoBehaviour
{
    private CharacterStat CharacterStat;
    private void Awake()
    {
        ResolveCharacterStat();
    }
    private void Start()
    {
        ResolveCharacterStat();
    }
    private void ResolveCharacterStat()
    {
        if (CharacterStat == null && PlayerManager.instance != null && PlayerManager.instance.character != null)
            CharacterStat = PlayerManager.instance.character.GetComponent<CharacterStat>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            ResolveCharacterStat();
            EnemyStat enemy = collision.gameObject.GetComponent<EnemyStat>();

            if (CharacterStat != null && enemy != null)
                CharacterStat.DoMagicDamage(enemy);
        }
    }
}
