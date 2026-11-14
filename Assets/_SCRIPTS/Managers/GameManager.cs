using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, ISaveManager
{
    public static GameManager instance;
    [SerializeField] private CheckPoint[] checkPoints;
    [SerializeField] private string loadedCheckPointID;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        checkPoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);
    }
    private void Start()
    {
        if (checkPoints == null || checkPoints.Length == 0)
            checkPoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);
    }
    public void RestartScene()
    {
        Time.timeScale = 1f;
        if (SaveManager.instance != null)
            SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        if (checkPoints == null || checkPoints.Length == 0)
            checkPoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);

        foreach (var pair in _data.checkPoints)
        {
            foreach (var item in checkPoints)
            {
                if (item != null && item.checkPointID == pair.Key && pair.Value == true)
                    item.ActiveCheckPoint(true);
            }
        }

        loadedCheckPointID = _data.closestCheckPointID;

        Invoke("PlaceChacracAtCheckPoint", .2f);
    }

    private void PlaceChacracAtCheckPoint()
    {
        if (string.IsNullOrEmpty(loadedCheckPointID) || PlayerManager.instance == null || PlayerManager.instance.character == null)
            return;

        foreach (var checkPoint in checkPoints)
        {
            if (checkPoint != null && loadedCheckPointID == checkPoint.checkPointID)
            {
                PlayerManager.instance.character.transform.position = checkPoint.transform.position;
                break;
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        if (checkPoints == null || checkPoints.Length == 0)
            checkPoints = FindObjectsByType<CheckPoint>(FindObjectsSortMode.None);

        CheckPoint closest = FindClosesCheckPoint();
        _data.closestCheckPointID = closest != null ? closest.checkPointID : "";
        _data.checkPoints.Clear();

        foreach (var item in checkPoints)
        {
            if (item != null && !string.IsNullOrEmpty(item.checkPointID) && !_data.checkPoints.ContainsKey(item.checkPointID))
                _data.checkPoints.Add(item.checkPointID, item.actived);
        }
    }

    private CheckPoint FindClosesCheckPoint()
    {
        if (PlayerManager.instance == null || PlayerManager.instance.character == null || checkPoints == null)
            return null;

        float closesDistance = Mathf.Infinity;
        CheckPoint closesCheckPoint = null;

        foreach (var checkPoint in checkPoints)
        {
            if (checkPoint == null || !checkPoint.actived || string.IsNullOrEmpty(checkPoint.checkPointID))
                continue;

            float distanceToCheckPoint = Vector2.Distance(PlayerManager.instance.character.transform.position, checkPoint.transform.position);

            if (distanceToCheckPoint < closesDistance)
            {
                closesDistance = distanceToCheckPoint;
                closesCheckPoint = checkPoint;
            }
        }
        return closesCheckPoint;
    }

    public void PasueGame(bool _pause)
    {
        if (_pause)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;
    }
}
