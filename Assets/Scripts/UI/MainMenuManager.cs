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
        sceneType scene = (sceneType) SaveData.Instance.LastSaveScene;
        switch (scene)
        {
            case sceneType.Ship:
                sceneName = "Ship Level Design";
                break;

            case sceneType.Cave:
                sceneName = "Cave Level Design";
                break;

            case sceneType.Hub:
                sceneName = "Hub Level Design";
                break;
        }
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
    public void DeleteProgress()
    {
        SaveLoad.DeleteProgress();
    }
}
