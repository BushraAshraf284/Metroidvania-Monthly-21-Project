using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    public Transform nearestInteractable;
    GameObject[] interactables;
    GameObject[] npcs;
    Controls controls;
    [SerializeField]
    float interactRange = 2.5f;
    float minDistance = 999f;

    private void Start()
    {
        controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
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
                if (collider.TryGetComponent(out Interactables interactables))
                {
                    interactables.Interact();
                }
                if (collider.TryGetComponent(out NPCInteractables npcInteractable))
                {
                    npcInteractable.NPCInteract();
                }
                if (collider.TryGetComponent(out Console console))
                {
                    console.Interact();
                }
            }
        }

    }

    public Console GetInteractableConsole()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Console console))
            {
                return console;
            }
        }
        return null;
    }

    public Interactables GetInteractableObject()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out Interactables interactables))
            {
                return interactables;
            }
        }
        return null;
    }

    public NPCInteractables GetNPCInteractableObject()
    {
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        foreach (Collider collider in colliderArray)
        {
            if (collider.TryGetComponent(out NPCInteractables npcInteractable))
            {
                return npcInteractable;
            }
        }
        return null;
    }
}
