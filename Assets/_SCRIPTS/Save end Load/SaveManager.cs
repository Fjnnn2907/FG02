using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SaveManager : MonoBehaviour
{
    public static SaveManager instance;

    private GameData gameData;
    private List<ISaveManager> saveManagers;
    private FileDataHandler fileDataHandler;

    [SerializeField] private bool isMaHoa;
    [SerializeField] private string fileName;

    private void Awake()
    {
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath,fileName, isMaHoa);
        
        saveManagers = FindAllSaveGame();
        //Debug.Log(Application.persistentDataPath);
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        gameData = fileDataHandler.Load();

        if(gameData == null)
        {
            NewGame();
        }

        foreach(var saveManager in saveManagers)
        {
            saveManager.LoadData(gameData);
        }
    }
    public void SaveGame()
    {
        foreach(ISaveManager saveManager in saveManagers)
        {
            saveManager.SaveData(ref gameData);
        }

        fileDataHandler.Save(gameData);

    }
    private void OnApplicationQuit()
    {
        SaveGame();
    }

    private List<ISaveManager> FindAllSaveGame()
    {
        IEnumerable<ISaveManager> saveManagers = FindObjectsOfType<MonoBehaviour>(true).OfType<ISaveManager>();

        return new List<ISaveManager>(saveManagers);
    }

    [ContextMenu("Delete Data")]
    public void DeleteData()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName, isMaHoa);
        fileDataHandler.Delete();
    }
    public bool HasNoSaveData()
    {
        if(fileDataHandler.Load() != null)
            return true;

        return false;    
    }
}
