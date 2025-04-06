using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Tooltip("The key to exit the game and return to the main menu")]
    public KeyCode exitKey;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadSceneByIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
        #elif UNITY_WEBPLAYER
            Application.OpenURL(webplayerQuitURL);
        #else
            Application.Quit();
        #endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(exitKey) && SceneManager.GetActiveScene().buildIndex != 0) // Load main menu only if not already in that scene
        {
            LoadSceneByIndex(0);
        }
    }
}
