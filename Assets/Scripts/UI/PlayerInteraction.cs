using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interaction : MonoBehaviour
{
    
    private void Update()
    {
        float interactRange = 5f;
        Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);

        if (Input.GetKeyDown(KeyCode.F))
        {
            foreach (Collider collider in colliderArray)
            {
                if (collider.TryGetComponent(out Interactables interactables))
                {
                    interactables.Interact();
                }
            }
        }
        
    }

    public Interactables GetInteractableObject()
    {
        float interactRange = 5f;
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
}
