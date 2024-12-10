using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    public Character character;
    private void Awake()
    {
        instance = this;
    }
}
