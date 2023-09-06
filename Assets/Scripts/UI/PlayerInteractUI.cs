﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField]
    GameObject interactableIcon;
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private Interaction playerInteraction;
    DialogueManager manager;
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        manager = GameObject.Find("Dialogue Manager").GetComponent<DialogueManager>();
    }
    private void Update()
    {
	    if (playerInteraction.GetInteractableObject() != null || playerInteraction.GetNPCInteractableObject() != null || playerInteraction.GetInteractableConsole() != null)
        {
            if(manager.isDialoging){
                Hide();
            }
            else{
                Show();
            }
            
        } 
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        containerGameObject.SetActive(true);
        interactableIcon.SetActive(true);
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
        interactableIcon.SetActive(false);
    }
}
