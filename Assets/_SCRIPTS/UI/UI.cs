using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    [SerializeField] private UIDrakScreen uIDrakScreen;
    [SerializeField] private GameObject endText;
    [SerializeField] private GameObject restartButton;

    [Space]
    public UI_ToolTip toolTip;
    public UICraftWindow craftWindow;
    public UiSkillTreeToolTip toolTipSkillTree;

    [SerializeField] private GameObject character;
    [SerializeField] private GameObject skilltree;
    [SerializeField] private GameObject cratf;
    [SerializeField] private GameObject option;
    [SerializeField] private GameObject inGame;

    private void Awake()
    {
        switchTo(skilltree);

        uIDrakScreen.gameObject.SetActive(true);
    }

    private void Start()
    {
        switchTo(inGame);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            SwithToInput(character);
        else if(Input.GetKeyDown(KeyCode.T))
            SwithToInput(skilltree);
        else if(Input.GetKeyDown(KeyCode.O))
            SwithToInput(option);
        else if(Input.GetKeyDown(KeyCode.C))
            SwithToInput(cratf);
    }

    public void switchTo(GameObject _menu)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            bool faceScreen = transform.GetChild(i).GetComponent<UIDrakScreen>() != null;

            if(!faceScreen)
                transform.GetChild(i).gameObject.SetActive(false);
        }

        if(_menu != null)
            _menu.SetActive(true);
    }

    public void SwithToInput(GameObject _menu)
    {
        if(_menu != null && _menu.activeSelf)
        {
            _menu.SetActive(false);
            CheckInGameUI();
            return;
        }

        switchTo(_menu);
    }

    private void CheckInGameUI()
    {
        for (int i = 0;i < transform.childCount; i++)
        {
            if(transform.GetChild(i).gameObject.activeSelf && transform.GetChild(i).GetComponent<UIDrakScreen>() == null)
                return;
        }
        switchTo(inGame);
    }
    
    public void SwitchOnEndScreen()
    {
        uIDrakScreen.FaceOutDrakScreen();
        StartCoroutine(EndScreen());
    }
    
    IEnumerator EndScreen()
    {
        yield return new WaitForSeconds(1);
        endText.SetActive(true);
        yield return new WaitForSeconds(1);
        restartButton.SetActive(true);
    }

    public void RestartButton()
    {
        GameManager.instance.RestartScene();
    }
}
