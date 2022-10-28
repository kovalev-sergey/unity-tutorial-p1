using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    public InputField playerName;
    public Text bestScore;
    // Start is called before the first frame update
    void Start()
    {
        playerName.onEndEdit.AddListener(delegate {InitStorage();});
    }

    void InitStorage() {
        StorageManager.Instance.Load(playerName.text);
        if (StorageManager.Instance.PlayerName.Length > 0) {
            bestScore.text = $"Best Score: {StorageManager.Instance.BestScore}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartGame() {
        StorageManager.Instance.PlayerName = playerName.text;
        
        SceneManager.LoadScene(1);
    }

    public void ExitGame() {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
        #else
        Application.Exit();
        #endif
    }
}
