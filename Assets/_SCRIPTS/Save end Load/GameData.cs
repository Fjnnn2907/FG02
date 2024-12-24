using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money;
    public SerializableDic<string, int> inventory;
    public List<string> equipmentID;

    public GameData()
    {
        this.money = 0;
        inventory = new();
        equipmentID = new List<string>();
    }
}
