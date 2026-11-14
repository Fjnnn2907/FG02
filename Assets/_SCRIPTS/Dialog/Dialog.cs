using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    private static readonly HashSet<Dialog> activeZones = new HashSet<Dialog>();
    public static bool AnyPlayerInDialogZone => activeZones.Count > 0;

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

    private void OnDisable()
    {
        activeZones.Remove(this);
        isDialoging = false;
    }

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

    private string[] GetActiveLines()
    {
        if (!talkBefore)
            return firstDialog;

        if (repeatDialog != null && repeatDialog.Length > 0)
            return repeatDialog;

        return firstDialog;
    }

    private void StartDialog()
    {
        string[] lines = GetActiveLines();
        if (lines == null || lines.Length == 0)
            return;

        dialogBox.SetActive(true);
        currentIndex = 0;
        if (TitleText != null)
            TitleText.text = TitleName;
        if (contantText != null)
            contantText.text = lines[currentIndex];
    }

    private void DisplayNextLine()
    {
        if (TitleText != null)
            TitleText.text = TitleName;

        string[] lines = GetActiveLines();
        if (lines == null || lines.Length == 0)
        {
            EndDialog();
            return;
        }

        currentIndex++;
        if (currentIndex < lines.Length)
        {
            if (contantText != null)
                contantText.text = lines[currentIndex];
        }
        else
        {
            if (!talkBefore)
                talkBefore = true;
            EndDialog();
        }
    }

    private void EndDialog()
    {
        if (dialogBox != null)
            dialogBox.gameObject.SetActive(false);
        currentIndex = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDialoging = true;
            activeZones.Add(this);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isDialoging = false;
            activeZones.Remove(this);
            EndDialog();
        }
    }
}
