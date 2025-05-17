using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.SceneManagement;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public bool gameIsPaused;

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
        ResetSceneState();
        SceneManager.LoadScene(sceneIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        ResetSceneState();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadNextScene()
    {
        ResetSceneState();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    void ResetSceneState()
    {
        RemoveFirstPersonEffects();
        if (gameIsPaused)
        {
            ResumeGame();
        }
    }

    void RemoveFirstPersonEffects()
    {
        // Unlock and show cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void AddFirstPersonEffects()
    {
        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    internal void EnterUIMode()
    {
        RemoveFirstPersonEffects();
    }


    internal void EnterGameplayMode()
    {
        AddFirstPersonEffects();
    }

    public void PauseGame()
    {
        gameIsPaused = true;
        Time.timeScale = 0;
    }
    
    public void ResumeGame()
    {
        gameIsPaused = false;
        Time.timeScale = 1;
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
}
