using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Created by Andrew Rodriguez with the help of https://www.youtube.com/watch?v=_nRzoTzeyxU&ab_channel=Brackeys
//This class manages most of the logic for our dialogue system. It connects the NpcDialoge and Dialogue script together.
public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    AudioSource textScrollNoise;
    [SerializeField]
    PauseMenu pause;
    [SerializeField]
    Movement movement;
    [SerializeField]
    OrbitCamera cameraMovement;
    public Text nameText; //npc name text object
    public Text dialogueText; //dialogue text object
    private Queue<string> sentences; 
    public GameObject dialogueBox;
    float gate;
    PlayerInteraction playerInt;
    [SerializeField]
    GameObject interactText;
    public bool isDialoging;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Player")){
            if(g.GetComponent<PlayerInteraction>() != null){
                playerInt = g.GetComponent<PlayerInteraction>();
            }
        }
        
        sentences = new Queue<string>();
    }

    public void StartDialogue(AltDialogue dialogue) //creates our queue of sentences, locks the player, enables the cursor, and activates dialogue UI
    {
        isDialoging = true;
        //Debug.Log("Starting conversation with "+dialogue.name);
        interactText.SetActive(false);
        movement.blockMovement();
        cameraMovement.enabled = false;
        Cursor.visible = true; //makes cursor visible
        Cursor.lockState = CursorLockMode.None;//makes cursor moveable
        nameText.text = dialogue.name;
        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialogueBox.SetActive(true);//enable the dialogue box UI
        DisplayNextSentence();
    }
    public void DisplayNextSentence() //displays next sentence in queue
    {
        if(!pause.isPaused){
            if (sentences.Count==0)
            {
                EndDialogue();
                return;
            }
            string curSentence = sentences.Dequeue();
            StopAllCoroutines(); //This makes sure the animating stops if the player clicks the continue button
            StartCoroutine(TypeSentence(curSentence)); //We call our coroutine to display each word at a delay
        }
    }

    IEnumerator TypeSentence (string sentence)//displays words in sentence with a delay
    {
        dialogueText.text = "";
        gate = 5;
        foreach (char letter in sentence.ToCharArray())
        {
            if(gate == 5){
                // NPC Dialogue Audio
                AudioManager.instance.PlayOneShot(FMODEvents.instance.npcSFX, this.transform.position);
                gate = 0;
            }
            else{
                gate += 1;
            }
            dialogueText.text += letter;
            yield return null;
        }
    }
    public void EndDialogue()
    {
        isDialoging = false;
        playerInt.inDialogue = false;
        movement.unblockMovement();
        cameraMovement.enabled = true;

        dialogueBox.SetActive(false); //makes dialogue box disapear
        Cursor.lockState = CursorLockMode.Locked;
    }
    
}
