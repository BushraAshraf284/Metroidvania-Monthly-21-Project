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
		Debug.Log("SAVE POINT");
		stats.RestoreHP(stats.MaxHP - stats.hp);
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
		Debug.Log("SCENE SWITCH");
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
