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
        if(instance != null)
            Destroy(instance.gameObject);
        else
            instance = this;
    }
    private void Start()
    {
        checkPoints = FindObjectsOfType<CheckPoint>();

        
    }
    public void RestartScene()
    {
        SaveManager.instance.SaveGame();
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void LoadData(GameData _data)
    {
        foreach (var pair in _data.checkPoints)
        {
            foreach (var item in checkPoints)
            {
                if (item.checkPointID == pair.Key && pair.Value == true)
                    item.ActiveCheckPoint();
            }
        }

        loadedCheckPointID = _data.closestCheckPointID;

        Invoke("PlaceChacracAtCheckPoint",.2f);
    }

    private void PlaceChacracAtCheckPoint()
    {
        foreach (var checkPoint in checkPoints)
        {
            if (loadedCheckPointID == checkPoint.checkPointID)
            {
                PlayerManager.instance.character.transform.position = checkPoint.transform.position;
            }
        }
    }

    public void SaveData(ref GameData _data)
    {
        _data.closestCheckPointID = FindClosesCheckPoint().checkPointID;
        _data.checkPoints.Clear();

        foreach (var item in checkPoints)
        {
            _data.checkPoints.Add(item.checkPointID,item.actived);
        }
    }

    private CheckPoint FindClosesCheckPoint()
    {
        float closesDistance = Mathf.Infinity;
        CheckPoint closesCheckPoint = null;

        foreach(var checkPoint in checkPoints)
        {
            float distanceToCheckPoint = Vector2.Distance(PlayerManager.instance.character.transform.position, checkPoint.transform.position);

            if(distanceToCheckPoint < closesDistance && checkPoint.actived == true)
            {
                closesDistance = distanceToCheckPoint;
                closesCheckPoint = checkPoint;
            }
        }
        return closesCheckPoint;
    }
}
