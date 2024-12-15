using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStat : StatManager
{
    private Enemy enemy;
    protected override void Start()
    {
        base.Start();

        enemy = GetComponent<Enemy>();
    }

    public override void TakeDamage(int _damege)
    {
        base.TakeDamage(_damege);

        enemy.DamageEffect();
    }
    protected override void Deah()
    {
        base.Deah();

        enemy.Deah();
    }
}
