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
        // Loading Data
        AudioManager.instance.PlayOneShot(FMODEvents.instance.startGameAudio, this.transform.position);
        SaveData.Instance = new SaveData();
        SaveLoad.LoadProgress();

        sceneName = "Hub Level Design";
        SceneManager.LoadScene(sceneName);
    }

    /* Audio for options
     For entering the options menu:
        AudioManager.instance.PlayOneShot(FMODEvents.instance.pauseGameAudio, this.transform.position);
     For exiting the options menu:
        AudioManager.instance.PlayOneShot(FMODEvents.instance.resumeGameAudio, this.transform.position);
     */

    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
        #endif
    }
}
