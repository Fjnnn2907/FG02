using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Character character;

    [Header("Money")]
    [SerializeField] private int coint;
    private void Awake()
    {
        if(instance != null)
            Debug.LogWarning("Erro Singleton" + instance.name);
        else
        instance = this;
    }

    public bool HaveMoney(int _coint)
    {
        if (_coint < 0 || _coint > coint) 
            return false;

        coint  -=_coint;
        return true;
    }
}
