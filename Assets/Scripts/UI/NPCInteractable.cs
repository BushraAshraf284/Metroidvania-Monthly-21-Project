using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractables : MonoBehaviour
{
	[SerializeField]
	GameObject UIPrompt;
	[SerializeField]
	GameObject notEnoughKitsUI;
    [SerializeField]
    private DialogueManager diag;

	public NpcDialogue npcDiag;
	[SerializeField]
	public bool repaired = false;
	[SerializeField]
	public bool doneIntro;
	GameObject player;
	OrbitCamera cameraMovement;
	
	public void DismissUI(){
		UIPrompt.SetActive(false);
		notEnoughKitsUI.SetActive(false);
		player.GetComponent<Movement>().unblockMovement();
		cameraMovement.enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		player.GetComponent<PlayerInteraction>().inDialogue = false;
	}
	
	public void NPCRepair(){
		if(!player.GetComponent<PlayerInteraction>().inDialogue){
			Debug.Log("entered NPC repair");
			if(player.GetComponent<UpgradeTracker>().repairKitCount > 0){
				Debug.Log("repaired NPC!");
				player.GetComponent<UpgradeTracker>().repairKitCount--;
				repaired = true;
				DismissUI();
			}
			else{
				Debug.Log("not enough kits!");
				DismissUI();
				notEnoughKitsUI.SetActive(true);
				player.GetComponent<Movement>().blockMovement();
				cameraMovement.enabled = false;
				Cursor.visible = true; //makes cursor visible
				Cursor.lockState = CursorLockMode.None;//makes cursor moveable
				player.GetComponent<PlayerInteraction>().inDialogue = true;
			}
		}
	}

    private void Start()
	{
		cameraMovement = GameObject.Find("3rd Person Camera Empty").GetComponent<OrbitCamera>();
	    npcDiag = this.GetComponent<NpcDialogue>();
	    foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")){
	    	if(g.GetComponent<UpgradeTracker>() != null){
	    		player = g;
	    	}
	    }
    }

    public void NPCInteract()
	{
		if(!player.GetComponent<PlayerInteraction>().inDialogue){
			if(repaired){
				Debug.Log("entered repaired npc dialogue");
				if(!doneIntro){
					diag.StartDialogue(npcDiag.introDialogue);
					doneIntro = true;
				}
				else{
					if(player.GetComponent<UpgradeTracker>().enteredWorld1){
						diag.StartDialogue(npcDiag.enteredWorld1Dialgue);
					}
					else if(player.GetComponent<UpgradeTracker>().enteredWorld2){
						diag.StartDialogue(npcDiag.enteredWorld2Dialgue);
					}
					else if(player.GetComponent<UpgradeTracker>().enteredBossArea){
						diag.StartDialogue(npcDiag.preBossFightDialogue);
					}
					else{
						diag.StartDialogue(npcDiag.hubWorldDialogue);
					}
				}
			}
			else{
				Debug.Log("initiated NPC repair");
				player.GetComponent<Movement>().blockMovement();
				cameraMovement.enabled = false;
				Cursor.visible = true; //makes cursor visible
				Cursor.lockState = CursorLockMode.None;//makes cursor moveable
				UIPrompt.SetActive(true);
				player.GetComponent<PlayerInteraction>().inDialogue = true;
			}
		}
    }
}
