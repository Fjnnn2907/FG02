using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    [SerializeField] private List<int> modifiers;
    public int GetValue()
    {
        int finalValue = baseValue;
        foreach (int modifier in modifiers)
        {
            finalValue += modifier;
        }
        return finalValue;
    }
    public int SetDefaultValue(int _value)
    {
        return baseValue = _value;
    }

    public void AddMotdifier(int _motdifier)
    {
        modifiers.Add(_motdifier);
    }
    public void RemoveMotdifier(int _motdifier)
    {
        modifiers.Remove(_motdifier);
    }
}
