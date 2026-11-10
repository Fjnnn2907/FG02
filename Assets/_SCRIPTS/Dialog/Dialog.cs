using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    private bool talkBefore = false; 
    private int currentIndex = 0;   
    public string TitleName;
    
    public GameObject dialogBox;
    public TextMeshProUGUI TitleText; 
    public TextMeshProUGUI contantText; 
    public bool isDialoging = false;

    [TextArea(3, 10)]
    public string[] firstDialog;

    [TextArea(3, 10)]
    public string[] repeatDialog;

    private void Update()
    {
        if (dialogBox == null) return;
        if (Input.GetKeyDown(KeyCode.E) && isDialoging)
        {
            if (!dialogBox.activeInHierarchy)
            {
                StartDialog();
            }
            else
            {
                DisplayNextLine();
            }
        }
    }

    private void StartDialog()
    {
        dialogBox.SetActive(true);
        currentIndex = 0;
        TitleText.text = TitleName;
        if (!talkBefore)
        {
            contantText.text = firstDialog[currentIndex];
        }
        else
        {
            contantText.text = repeatDialog[currentIndex];
        }
    }

    private void DisplayNextLine()
    {
        TitleText.text = TitleName;

        if (!talkBefore)
        {
            currentIndex++;
            if (currentIndex < firstDialog.Length)
            {
                contantText.text = firstDialog[currentIndex];
            }
            else
            {
                EndDialog();
                talkBefore = true; 
            }
        }
        else
        {
            currentIndex++;
            if (currentIndex < repeatDialog.Length)
            {
                contantText.text = repeatDialog[currentIndex];
            }
            else
            {
                EndDialog();
            }
        }
    }

    private void EndDialog()
    {
        dialogBox.gameObject.SetActive(false);
        currentIndex = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDialoging = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDialoging = false;
            EndDialog();
        }
    }
}
