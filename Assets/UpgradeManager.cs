using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public sceneType type;
    [SerializeField]
    public List<GameObject> Upgrades = new List<GameObject>();

	void LateAwake(){
		SaveManager.Instance.LoadUpgrades(type);
	}
    private void Awake()
    {
	    Invoke("LateAwake", .1f);
    }

}
