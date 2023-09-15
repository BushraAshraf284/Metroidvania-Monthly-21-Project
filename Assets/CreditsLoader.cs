using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreditsLoader : MonoBehaviour
{
	[SerializeField]
	GameObject creditsScreen;
	[SerializeField]
	Movement movement;
	[SerializeField]
	
	OrbitCamera cameraMovement;
	// Start is called before the first frame update
	protected void OnTriggerEnter(Collider other)
	{
		creditsScreen.SetActive(true);
		movement.blockMovement();
		cameraMovement.enabled = false;
		Cursor.visible = true; //makes cursor visible
		Cursor.lockState = CursorLockMode.None;
	}

	public void DismissUI(){
		
		creditsScreen.SetActive(false);
		movement.unblockMovement();
		cameraMovement.enabled = true;
		Cursor.visible = false; //makes cursor visible
		Cursor.lockState = CursorLockMode.Locked;
		
	}
}
