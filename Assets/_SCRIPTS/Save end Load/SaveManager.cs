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
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        EnsureFileDataHandler();
    }

    private void EnsureFileDataHandler()
    {
        if (fileDataHandler == null)
        {
            string safeFileName = string.IsNullOrWhiteSpace(fileName) ? "savegame.json" : fileName;
            fileDataHandler = new FileDataHandler(Application.persistentDataPath, safeFileName, isMaHoa);
        }
    }

    private void Start()
    {
        EnsureFileDataHandler();

        saveManagers = FindAllSaveGame();
        LoadGame();
    }

    public void NewGame()
    {
        gameData = new GameData();
    }

    public void LoadGame()
    {
        EnsureFileDataHandler();
        if (saveManagers == null)
            saveManagers = FindAllSaveGame();

        gameData = fileDataHandler.Load();

        if (gameData == null)
        {
            NewGame();
        }

        foreach (var saveManager in saveManagers)
        {
            if (saveManager != null)
                saveManager.LoadData(gameData);
        }

        if (SkillManager.instance != null)
        {
            SkillManager.instance.rollSkill?.RefreshUnlockState();
            SkillManager.instance.cloneSkill?.RefreshUnlockState();
            SkillManager.instance.swordSkill?.RefreshUnlockState();
            SkillManager.instance.backholiSkill?.RefreshUnlockState();
            SkillManager.instance.crytalSkill?.RefreshUnlockState();
        }
    }
    public void SaveGame()
    {
        EnsureFileDataHandler();
        if (saveManagers == null)
            saveManagers = FindAllSaveGame();
        if (gameData == null)
            NewGame();

        foreach (ISaveManager saveManager in saveManagers)
        {
            if (saveManager != null)
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
        IEnumerable<ISaveManager> found = FindObjectsByType<MonoBehaviour>(FindObjectsInactive.Include, FindObjectsSortMode.None).OfType<ISaveManager>();

        return new List<ISaveManager>(found);
    }

    [ContextMenu("Delete Data")]
    public void DeleteData()
    {
        EnsureFileDataHandler();
        fileDataHandler.Delete();
    }
    public bool HasSaveData()
    {
        EnsureFileDataHandler();
        return fileDataHandler.Load() != null;
    }
    public bool HasNoSaveData()
    {
        return HasSaveData();
    }
}
