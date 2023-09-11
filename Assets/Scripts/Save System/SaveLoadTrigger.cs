using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class SaveLoadTrigger: MonoBehaviour
{   
    public SaveType saveType;
    public sceneType sceneType;
    private string sceneName;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            SaveLoad.SaveProgress();
            if (saveType == SaveType.SCENESWITCH)
            {
                switch (sceneType)
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
            else
                Destroy(gameObject);
           
           
        }
    }
}
public enum SaveType
{
    SCENESWITCH,
    SAVEPOINT
}
