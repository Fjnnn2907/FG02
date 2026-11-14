using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stat
{
    [SerializeField] private int baseValue;

    [SerializeField] private List<int> modifiers = new List<int>();
    public int GetBaseValue() => baseValue;
    public int GetValue()
    {
        int finalValue = baseValue;
        if (modifiers != null)
        {
            foreach (int modifier in modifiers)
            {
                finalValue += modifier;
            }
        }
        return finalValue;
    }
    public int SetDefaultValue(int _value)
    {
        return baseValue = _value;
    }

    public void AddMotdifier(int _motdifier)
    {
        if (_motdifier == 0)
            return;

        if (modifiers == null)
            modifiers = new List<int>();

        modifiers.Add(_motdifier);
    }
    public void RemoveMotdifier(int _motdifier)
    {
        if (_motdifier == 0 || modifiers == null)
            return;

        modifiers.Remove(_motdifier);
    }
}
