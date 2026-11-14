using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStat : StatManager
{
    private Character character;
    protected override void Awake()
    {
        base.Awake();
        character = GetComponent<Character>();
    }
    protected override void Start()
    {
        base.Start();

        if (character == null)
            character = GetComponent<Character>();
    }

    public override void TakeDamage(int _damege)
    {
        base.TakeDamage(_damege);
    }
    protected override void Deah()
    {
        base.Deah();

        if (character != null)
            character.Deah();
    }
}
