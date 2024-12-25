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
        if(!SaveManager.instance.HasNoSaveData())
            contiueButton.SetActive(false);
    }
    public void NextScene()
    {
        StartCoroutine(LoadSceneEffect(1.5f));
    }

    public void NewScene()
    {
        SaveManager.instance.DeleteData();
        StartCoroutine(LoadSceneEffect(1.5f));
    }

    public void ExitGame()
    {
        Debug.Log("Thoat game");
        //Application.Quit();
    }

    IEnumerator LoadSceneEffect(float _delay)
    {
        uIDrakScreen.FaceOutDrakScreen();

        yield return new WaitForSeconds(_delay);

        SceneManager.LoadScene(nextScene);
    }
}
