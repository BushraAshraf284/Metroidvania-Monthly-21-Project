using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteractables : MonoBehaviour
{
    [SerializeField]
    private DialogueManager diag;

    public NpcDialogue npcDiag;

    private void Start()
    {
        npcDiag = this.GetComponent<NpcDialogue>();
    }

    public void NPCInteract()
    {
        //Debug.Log("Interact");
        diag.StartDialogue(npcDiag.dialogue);
    }
}
