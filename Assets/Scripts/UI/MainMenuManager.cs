using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    private string sceneName;
    public void StartGame()
    {
        sceneName = "Hub Level Design";
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }
}
