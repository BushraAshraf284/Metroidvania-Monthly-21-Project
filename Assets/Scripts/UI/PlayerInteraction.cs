using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    Controls controls;
    [SerializeField]
    float interactRange = 2.5f;

    private void Start()
    {
        controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
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
