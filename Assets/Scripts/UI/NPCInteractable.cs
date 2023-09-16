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
	[SerializeField]
	public bool doneWorld1;
	[SerializeField]
	public bool doneWorld2;
	GameObject player;
	OrbitCamera cameraMovement;
	public bool hasEvent;
	[SerializeField]
	bool hasWakeAnim;
	
	[SerializeField]
	public bool nonDiagPopUp = false;
	
	protected void Awake()
	{
		doneWorld1 = SaveData.Instance.doneWorld1;
		doneWorld2 = SaveData.Instance.doneWorld2;
		if(repaired){
			if(hasWakeAnim){
				GetComponent<Animator>().SetBool("Activated", true);
			}
			
		}
	}
	
	public void DismissUI(){
		UIPrompt.SetActive(false);
		notEnoughKitsUI.SetActive(false);
		player.GetComponent<Movement>().unblockMovement();
		cameraMovement.enabled = true;
		Cursor.lockState = CursorLockMode.Locked;
		player.GetComponent<PlayerInteraction>().inDialogue = false;
		nonDiagPopUp = false;
	}
	
	public void NPCRepair(){
		Debug.Log("entered NPC repair");
		if(player.GetComponent<UpgradeTracker>().repairKitCount > 0){
			Debug.Log("repaired NPC!");
			AudioManager.instance.PlayOneShot(FMODEvents.instance.npcRepairSuccessSFX, this.transform.position);
			player.GetComponent<UpgradeTracker>().repairKitCount--;
			SaveData.Instance.RepairKitCount--;
			repaired = true;
			DismissUI();
			if(hasWakeAnim){
				GetComponent<Animator>().SetBool("Activated", true);
			}
			SaveManager.Instance.SaveNPCsData();
		}
		else{
			Debug.Log("not enough kits!");
			AudioManager.instance.PlayOneShot(FMODEvents.instance.npcRepairFailSFX, this.transform.position);
			DismissUI();
			notEnoughKitsUI.SetActive(true);
			player.GetComponent<Movement>().blockMovement();
			cameraMovement.enabled = false;
			Cursor.visible = true; //makes cursor visible
			Cursor.lockState = CursorLockMode.None;//makes cursor moveable
			player.GetComponent<PlayerInteraction>().inDialogue = true;
			nonDiagPopUp = true;
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
		if(repaired){
			if(hasWakeAnim){
				GetComponent<Animator>().SetBool("Activated", true);
			}
			
		}
    }

    public void NPCInteract()
	{
		if(repaired){	
			nonDiagPopUp = false;
			Debug.Log("entered repaired npc dialogue");
			if(!doneIntro){
				diag.StartDialogue(npcDiag.introDialogue);
				doneIntro = true;
                SaveManager.Instance.SaveNPCsData();
            }
			else{
				
				if((player.GetComponent<UpgradeTracker>().enteredWorld1 && player.GetComponent<UpgradeTracker>().enteredWorld2) && (!doneWorld2 && !doneWorld1)){
					diag.StartDialogue(npcDiag.enteredWorld1and2Dialgue);
					doneWorld2 = true;
					SaveData.Instance.doneWorld2 = true;
					doneWorld1 = true;
					SaveData.Instance.doneWorld1 = true;
				}
				else if((player.GetComponent<UpgradeTracker>().enteredWorld1)&&(!doneWorld1)){
					diag.StartDialogue(npcDiag.enteredWorld1Dialgue);
					doneWorld1 = true;
					SaveData.Instance.doneWorld1 = true;
				}
				else if((player.GetComponent<UpgradeTracker>().enteredWorld2) && (!doneWorld2)){
					diag.StartDialogue(npcDiag.enteredWorld2Dialgue);
					doneWorld2 = true;
					SaveData.Instance.doneWorld2 = true;
				}

				else{
					diag.StartDialogue(npcDiag.hubWorldDialogue);
				}
			}
		}
		else{
			nonDiagPopUp = true;
			Debug.Log("initiated NPC repair");
			player.GetComponent<Movement>().blockMovement();
			cameraMovement.enabled = false;
			AudioManager.instance.PlayOneShot(FMODEvents.instance.npcPreChatSFX, this.transform.position);
			Cursor.visible = true; //makes cursor visible
			Cursor.lockState = CursorLockMode.None;//makes cursor moveable
			UIPrompt.SetActive(true);
			player.GetComponent<PlayerInteraction>().inDialogue = true;
		}
	}
}
