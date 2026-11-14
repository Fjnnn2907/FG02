using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public string SceneName;
    private bool isTransitioning;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isTransitioning || string.IsNullOrWhiteSpace(SceneName))
            return;

        if (collision.GetComponentInParent<Character>() != null)
        {
            isTransitioning = true;
            Time.timeScale = 1f;
            if (SaveManager.instance != null)
                SaveManager.instance.SaveGame();
            SceneManager.LoadScene(SceneName);
        }
    }
}
