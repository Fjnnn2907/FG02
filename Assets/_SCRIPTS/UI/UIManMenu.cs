using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManMenu : MonoBehaviour
{
    [SerializeField] private string nextScene = "TutorialRoom";

    [SerializeField] private GameObject contiueButton;
    [SerializeField] private UIDrakScreen uIDrakScreen;
    private void Start()
    {
        Time.timeScale = 1f;
        if (SaveManager.instance != null && !SaveManager.instance.HasSaveData() && contiueButton != null)
            contiueButton.SetActive(false);
    }
    public void NextScene()
    {
        StartCoroutine(LoadSceneEffect(1.5f));
    }

    public void NewScene()
    {
        if (SaveManager.instance != null)
            SaveManager.instance.DeleteData();
        StartCoroutine(LoadSceneEffect(1.5f));
    }

    public void ExitGame()
    {
        Debug.Log("Thoat game");
        Application.Quit();
    }

    IEnumerator LoadSceneEffect(float _delay)
    {
        Time.timeScale = 1f;
        if (uIDrakScreen != null)
            uIDrakScreen.FaceOutDrakScreen();

        yield return new WaitForSecondsRealtime(_delay);

        if (!string.IsNullOrEmpty(nextScene))
            SceneManager.LoadScene(nextScene);
    }
}
