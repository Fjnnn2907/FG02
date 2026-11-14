using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class APIManager : MonoBehaviour
{
    public TMP_InputField userName;
    public TMP_InputField password;
    [SerializeField] private string apiUrl = "https://localhost:5295/api/LoginAccount";
    public static Model model;

    private bool isLoggingIn;

    public void CheckLogin()
    {
        if (isLoggingIn || userName == null || password == null)
            return;

        var user = userName.text != null ? userName.text.Trim() : "";
        var pass = password.text ?? "";

        if (string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
        {
            Debug.LogWarning("Vui long nhap day du tai khoan va mat khau.");
            return;
        }

        var loginPayload = new LoginRequest
        {
            email = user,
            password = pass
        };

        var json = JsonUtility.ToJson(loginPayload);
        StartCoroutine(LoginAccount(json));
    }

    IEnumerator LoginAccount(string json)
    {
        isLoggingIn = true;
        var url = string.IsNullOrWhiteSpace(apiUrl) ? "https://localhost:5295/api/LoginAccount" : apiUrl;

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        using (var request = new UnityWebRequest(url, "POST"))
        using (var uploadHandler = new UploadHandlerRaw(bodyRaw))
        using (var downloadHandler = new DownloadHandlerBuffer())
        {
            request.uploadHandler = uploadHandler;
            request.downloadHandler = downloadHandler;
            request.SetRequestHeader("Content-Type", "application/json");
            request.timeout = 15;

            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogWarning("Dang nhap that bai: " + request.error);
            }
            else
            {
                var text = request.downloadHandler != null ? request.downloadHandler.text : "";
                Model parsed = !string.IsNullOrWhiteSpace(text) ? JsonUtility.FromJson<Model>(text) : null;

                if (parsed != null && parsed.status)
                {
                    if (parsed.data != null)
                    {
                        parsed.data.password = null;
                        parsed.data.newPassword = null;
                        Debug.Log(parsed.data.name);
                    }

                    model = parsed;
                    Debug.Log("Dang nhap thanh cong");
                    Time.timeScale = 1f;
                    SceneManager.LoadScene(0);
                }
                else
                {
                    Debug.LogWarning("Dang nhap that bai");
                }
            }
        }

        isLoggingIn = false;
    }
}
