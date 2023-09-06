using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public Transform nearestInteractable;
    GameObject[] interactables;
    GameObject[] npcs;
    Controls controls;
    [SerializeField]
    float interactRange = 2.5f;
    float minDistance = 999f;
    public bool inDialogue;
    DialogueManager manager;

    private void Start()
    {
        controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
        manager = GameObject.Find("Dialogue Manager").GetComponentInChildren<DialogueManager>();
        interactables = GameObject.FindGameObjectsWithTag("Interactable");
        npcs = GameObject.FindGameObjectsWithTag("NPC");
        foreach(GameObject g in interactables){
            if(Vector3.Distance(this.transform.position, g.transform.position) < minDistance){
                nearestInteractable = g.transform;
                minDistance = Vector3.Distance(this.transform.position, g.transform.position);
            }
        }
        foreach(GameObject g in npcs){
            if(Vector3.Distance(this.transform.position, g.transform.position) < minDistance){
                nearestInteractable = g.transform;
                minDistance = Vector3.Distance(this.transform.position, g.transform.position);
            }
        }
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
			                    npcInteractable.NPCInteract();
                                inDialogue = true;
                            }
                            else{
                                manager.DisplayNextSentence();
                            }
			            }
			            if (collider.TryGetComponent(out Console console))
			            {
			                console.Interact();
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
