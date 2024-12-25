using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Character character;

    [Header("Money")]
    public int coint;
    private void Awake()
    {
        if (instance != null)
            Debug.LogWarning("Erro Singleton" + instance.name);
        else
            instance = this;
    }

    public bool HaveMoney(int _coint)
    {
        if (_coint < 0 || _coint > coint)
            return false;

        coint -= _coint;
        return true;
    }

    public int GetMoney()
    {
        return coint;
    }

    public void LoadData(GameData _data)
    {
        coint = _data.money;
    }

    public void SaveData(ref GameData _data)
    {
        _data.money = coint;
    }
}
