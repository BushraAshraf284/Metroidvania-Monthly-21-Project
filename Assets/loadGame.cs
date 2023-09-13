using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class loadGame : MonoBehaviour
{
	public void LOAD(){
		SaveManager.Instance.LoadGameAfterDeath();
	}
	
}
