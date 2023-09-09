using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    Controls controls;
    [SerializeField]
    float interactRange = 2.5f;
    public bool inDialogue;
	DialogueManager manager;
	public bool interactiveConvo;
	public bool nonDiagPopUp;
	public Console tempConsole;
	

    private void Start()
    {
        controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
        manager = GameObject.Find("Dialogue Manager").GetComponentInChildren<DialogueManager>();
    }

    private void Update()
    {
        
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        if (Input.GetKeyDown(controls.keys["interact"]))
        {
		        foreach (Collider collider in colliderArray)
		        {
		        	if(collider.gameObject.tag == "Interactable" || collider.gameObject.tag == "NPC"){
			            if (collider.TryGetComponent(out Interactables interactables))
			            {
			                interactables.Interact();
			            }
			            if (collider.TryGetComponent(out NPCInteractables npcInteractable))
			            {
				            if(!inDialogue){
					            nonDiagPopUp = false;
			                    npcInteractable.NPCInteract();
	                            inDialogue = true;
	                            if(npcInteractable.hasEvent){
		                            interactiveConvo = true;
		                            if(npcInteractable.gameObject.GetComponent<Console>() != null){
			                            tempConsole = npcInteractable.gameObject.GetComponent<Console>();
			                            npcInteractable.hasEvent = false;
		                            }
	                            }
                            }
                            else{
                            	if(npcInteractable.nonDiagPopUp){
                            		nonDiagPopUp = true;
                            	}
                            	else{
                            		nonDiagPopUp = false;
                            		manager.DisplayNextSentence();
                            	}
                                
                            }

			            }
			            if (collider.TryGetComponent(out Console console))
			            {
			            	if(collider.gameObject.tag != "NPC"){
				            	console.Interact();
			            	}
			            }
		        	}
		        }
        }
    }

    public Console GetInteractableConsole()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
        {
        	if(collider.gameObject.tag == "Interactable" || collider.gameObject.tag == "NPC"){
	            if (collider.TryGetComponent(out Console console))
	            {
	                return console;
	            }
        	}
        }
        return null;
    }

    public Interactables GetInteractableObject()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
        {
        	if(collider.gameObject.tag == "Interactable" || collider.gameObject.tag == "NPC"){
	            if (collider.TryGetComponent(out Interactables interactables))
	            {
	                return interactables;
	            }
        	}
        }
        return null;
    }

    public NPCInteractables GetNPCInteractableObject()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
        {
        	if(collider.gameObject.tag == "Interactable" || collider.gameObject.tag == "NPC"){
	            if (collider.TryGetComponent(out NPCInteractables npcInteractable))
	            {
	                return npcInteractable;
	            }
        	}
        }
        return null;
    }
}
