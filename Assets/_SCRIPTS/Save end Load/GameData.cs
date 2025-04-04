using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameData
{
    public int money;
    public SerializableDic<string, int> inventory;
    public SerializableDic<string, bool> skillTree;
    public List<string> equipmentID;


    public SerializableDic<string, bool> checkPoints;
    public string closestCheckPointID;

    public SerializableDic<string,float> audioSetting;
    public GameData()
    {
        this.money = 0;
        inventory = new();
        equipmentID = new();
        skillTree = new();

        closestCheckPointID = string.Empty;
        checkPoints = new();

        audioSetting = new();
    }
}
