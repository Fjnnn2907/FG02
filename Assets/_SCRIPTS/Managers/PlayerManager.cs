using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Character character;
    private void Awake()
    {
        if(instance != null)
            Debug.LogWarning("Erro Singleton" + instance.name);
        else
        instance = this;
    }
}
