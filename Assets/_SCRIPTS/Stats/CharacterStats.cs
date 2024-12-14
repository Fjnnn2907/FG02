using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int damege = 10;
    public int MaxHealth = 100;

    [SerializeField] protected int currentHealth;

    private void Start()
    {
        currentHealth = MaxHealth;
    }
    public void TakeDamge(int _damege)
    {
        currentHealth -= _damege;

        if (currentHealth < 0)
            Deah();
    }

    private void Deah()
    {
        throw new NotImplementedException();
    }
}
