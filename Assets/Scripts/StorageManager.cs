using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class StorageManager : MonoBehaviour
{
    static public StorageManager Instance;

    public string PlayerName;
    public int bestScore;

    void Awake() {
        if (StorageManager.Instance) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int BestScore {
        get { return bestScore; }
        set {
            if (bestScore < value) bestScore = value;
        }
    }

    [System.Serializable]
    class Data {
        public int bestScore;
        public string name;
    }

    public void Save() {
        Data data = new()
        {
            bestScore = BestScore,
            name = PlayerName
        };
        string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + $"/{PlayerName}-data.json", json);
    }

    public void Load(string playerName) {
        if (PlayerName.Length == 0 || bestScore == 0) return;
        string path = Application.persistentDataPath + $"/{playerName}-data.json";
        if (File.Exists(path)) {
            string json = File.ReadAllText(path);
            Debug.Log(json);
            Data data = JsonUtility.FromJson<Data>(json);
            BestScore = data.bestScore;
            PlayerName = data.name;
        }
    }
}
