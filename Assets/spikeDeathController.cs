using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spikeDeathController : MonoBehaviour
{
	[SerializeField]
	GameObject deathVolume;
	void HideVolume(){
		deathVolume.SetActive(false);
	}
	void ShowVolume(){
		deathVolume.SetActive(true);
	}
}
