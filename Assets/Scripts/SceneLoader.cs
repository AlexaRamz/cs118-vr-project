using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public void LoadSceneByIndex(int sceneIndex)
    {
        GameManager.Instance.LoadSceneByIndex(sceneIndex);
    }

    public void LoadSceneByName(string sceneName)
    {
        GameManager.Instance.LoadSceneByName(sceneName);
    }
}
