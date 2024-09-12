using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SaveData : MonoBehaviour
{
    public static SaveData Instance;

    public string playerName;
    public int highScore;
    public string highScoreText = "Best Score : 0";

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        Load();
    }

    [System.Serializable]
    class SaveFile
    {
        public int highScore;
        public string highScoreText;
    }

    public void Save()
    {
        SaveFile data = new SaveFile();
        data.highScore = highScore;
        data.highScoreText = highScoreText;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.dataPath + "/SaveFile.json", json);
    }

    public void Load()
    {
        string path = Application.dataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveFile data = JsonUtility.FromJson<SaveFile>(json);
            highScore = data.highScore;
            highScoreText = data.highScoreText;
        }
    }
}
