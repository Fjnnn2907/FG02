using UnityEngine;

public class PlayerManager : MonoBehaviour,ISaveManager
{
    public static PlayerManager instance;
    public Character character;

    [Header("Money")]
    public int coint;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public bool CanAfford(int _coint) => _coint >= 0 && coint >= _coint;

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
