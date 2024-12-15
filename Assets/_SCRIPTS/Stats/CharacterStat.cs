using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : StatManager
{
    private Character character;
    protected override void Start()
    {
        base.Start();

        character = GetComponent<Character>();
    }

    public override void TakeDamage(int _damege)
    {
        base.TakeDamage(_damege);

        character.DamageEffect();
    }
    protected override void Deah()
    {
        base.Deah();

        character.Deah();   
    }
}
