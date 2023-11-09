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
	GameObject saveProgressIcon;
	PlayerStats stats;
	
	void Start(){
		stats = GameObject.Find("3rd Person Character").GetComponent<PlayerStats>();
		saveProgressIcon = GameObject.Find("Saving Progress Icon").transform.GetChild(0).gameObject;
	}
	
	void resetIcon(){
		saveProgressIcon.SetActive(false);
	}
	
	public void SAVE(){
		//Debug.Log("SAVE POINT");
		stats.RestoreHP(stats.MaxHP - stats.hp);
		SaveManager.Instance.SavePointInfo();
		SaveLoad.SaveProgress();
		//visual stuff=---------
		saveProgressIcon.SetActive(true);
		Invoke("resetIcon", 5f);
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
            	Debug.Log("SCENE SWITCH");
            	
                switch (sceneType)
                {
                    case sceneType.Ship:
	                    sceneName = "Ship Level Design";
	                    stats.comingFromShip = true;
	                    SaveData.Instance.comingFromShip = true;
                        SaveData.Instance.EnteredWorld2 = true;
						
                        break;

                    case sceneType.Cave:
	                    sceneName = "Cave Level Design";
	                    stats.comingFromCave = true;
	                    SaveData.Instance.comingFromCave = true;
                        SaveData.Instance.EnteredWorld1 = true;
                        break;

                    case sceneType.Hub:
                        sceneName = "Hub Level Design";
                        break;
                }

				PlayerPrefs.SetInt("IsSceneSwitch", 1);
                SaveLoad.SaveProgress();
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
