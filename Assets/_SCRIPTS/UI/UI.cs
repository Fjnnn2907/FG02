using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI : MonoBehaviour
{
    public UI_ToolTip toolTip;
    public UICraftWindow craftWindow;


    [SerializeField] private GameObject character;
    [SerializeField] private GameObject skilltree;
    [SerializeField] private GameObject cratf;
    [SerializeField] private GameObject option;

    private void Start()
    {
        switchTo(null);
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
            return;
        }

        switchTo(_menu);
    }

}
