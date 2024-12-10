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
    public static Model model;
    public void CheckLogin()
    {
        var user = userName.text;
        var pass = password.text;

        var account = new Account
        {
            email = user,
            password = pass
        };
        // ep chuoi
        //var json = JsonConvert.SerializeObject(account);
        var json = JsonUtility.ToJson(account);
        //Debug.Log(json);

        StartCoroutine(LoginAccount(json));
    }

    IEnumerator LoginAccount(string json)
    {
        var url = "http://localhost:5295/api/LoginAccount";

        var request = new UnityWebRequest(url, "POST");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(json);
        request.uploadHandler = (UploadHandler)new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError ||
            request.result == UnityWebRequest.Result.ProtocolError)
        {
            //Debug.Log(request.error);
            Debug.Log("Ðãng nh?p th?t b?i");
        }
        else
        {
            var text = request.downloadHandler.text;
            //Debug.Log(text);
            model = JsonUtility.FromJson<Model>(text);
            if (model.status)
            {
                Debug.Log(model.data.name);
                Debug.Log("Dang nhap thanh cong");
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("Ðãng nh?p th?t b?i");

            }
        }
    }
}
