using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractables : MonoBehaviour
{
    [SerializeField]
    private Dialogue diag;

    private void Start()
    {
        
    }

    public void NPCInteract()
    {
        //Debug.Log("Interact");
        diag.StartDialogue();
    }
}
