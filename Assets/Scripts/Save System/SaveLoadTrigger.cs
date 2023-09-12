using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class SaveLoadTrigger: MonoBehaviour
{   
	[SerializeField]
	GameObject rotator;
    public SaveType saveType;
    public sceneType sceneType;
	private string sceneName;
	
	public void SAVE(){
		SaveLoad.SaveProgress();
		if(rotator.GetComponent<Rotator>()!= null){
			rotator.GetComponent<Rotator>().RampRotation();
		}
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (saveType == SaveType.SCENESWITCH)
            {
            	SaveLoad.SaveProgress();
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
           
           
        }
    }
}
public enum SaveType
{
    SCENESWITCH,
    SAVEPOINT
}
