using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class FileDataHandler
{
    public string dataDirPath = "";
    public string dataFileName = "";

    private bool encryptData = false;
    private string codeWord = "fin";

    public FileDataHandler(string _dataDirPath, string _dataFileName, bool _encryptData)
    {
        dataDirPath = _dataDirPath;
        dataFileName = _dataFileName;
        encryptData = _encryptData;
    }

    public void Save(GameData _gameData)
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            string dataToStore = JsonUtility.ToJson(_gameData, true);
            
            if(encryptData)
                dataToStore = MaHoa(dataToStore);

            using (FileStream stream = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter witer = new StreamWriter(stream))
                {
                    witer.Write(dataToStore);
                }
            }

        }
        catch(Exception e)
        {
            Debug.LogError("du lieu khong the luu" + e.Message);  
        }
    }

    public GameData Load()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        GameData _gameData = null;

        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";

                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        dataToLoad = sr.ReadToEnd();
                    }


                }

                if(encryptData)
                    dataToLoad = MaHoa(dataToLoad);

                _gameData = JsonUtility.FromJson<GameData>(dataToLoad);
            }
            catch(Exception e)
            {
                Debug.Log("Khong the tai data " + e.Message);
            }
        }

        return _gameData;
    }

    public void Delete()
    {
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        if(File.Exists(fullPath))
            File.Delete(fullPath);
    }
    private string MaHoa(string _data)
    {
        string modifiedData = "";

        for(int i = 0; i < _data.Length; i++)
        {
            modifiedData += (char)(_data[i] ^ codeWord[i % codeWord.Length]);
        }
        return modifiedData;
    }
}
