using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameObject;
    [SerializeField] private Interaction playerInteraction;

    private void Update()
    {
        if (playerInteraction.GetInteractableObject() != null)
        {
            Show();
        } 
        else if(playerInteraction.GetInteractableConsole() != null){
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        containerGameObject.SetActive(true);
    }

    private void Hide()
    {
        containerGameObject.SetActive(false);
    }
}
