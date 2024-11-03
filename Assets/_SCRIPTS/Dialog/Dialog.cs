using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    private bool hasTalkedBefore = false; // Để kiểm tra lần đầu nói chuyện
    private int currentLineIndex = 0;     // Chỉ số của câu thoại hiện tại
    public GameObject dialogBox;          // UI của hộp thoại
    public Text dialogText;               // Text UI để hiển thị thoại

    [TextArea(3, 10)]
    public string[] firstDialogLines = new string[]
    {
        "Xin chào! Tôi là NPC của khu vực này.",
        "Tôi sẽ hướng dẫn bạn cách bắt đầu cuộc hành trình.",
        "Hãy cẩn thận và chúc bạn may mắn!"
    };
    [TextArea(3, 10)]
    public string[] repeatDialogLines = new string[]
    {
        "Lại gặp nhau rồi! Bạn có cần giúp đỡ gì không?",
        "Nhớ rằng tôi luôn ở đây nếu bạn cần hỗ trợ.",
        "Chúc bạn tiếp tục hành trình thành công!"
    };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
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
        currentLineIndex = 0;

        if (!hasTalkedBefore)
        {
            dialogText.text = firstDialogLines[currentLineIndex];
        }
        else
        {
            dialogText.text = repeatDialogLines[currentLineIndex];
        }
    }

    private void DisplayNextLine()
    {
        if (!hasTalkedBefore)
        {
            currentLineIndex++;
            if (currentLineIndex < firstDialogLines.Length)
            {
                dialogText.text = firstDialogLines[currentLineIndex];
            }
            else
            {
                EndDialog();
                hasTalkedBefore = true; // Đánh dấu là đã nói chuyện lần đầu
            }
        }
        else
        {
            currentLineIndex++;
            if (currentLineIndex < repeatDialogLines.Length)
            {
                dialogText.text = repeatDialogLines[currentLineIndex];
            }
            else
            {
                EndDialog();
            }
        }
    }

    private void EndDialog()
    {
        dialogBox.SetActive(false);
        currentLineIndex = 0; // Đặt lại chỉ số để lần sau bắt đầu từ câu đầu tiên
    }
}
