﻿//Author: Brian Meginness
//Debugging: Brian Meginness, Travis Parks, Andrew Rodriguez
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    // Components for each UI screen
    GameObject pauseUI;
    GameObject settingUI;
    GameObject helpUI;
	GameObject backgrnd;
	PlayerInteraction playerInt;

    //Flipflop bool to pause and unpause with the same button
    public bool isPaused = false;


    // Start is called before the first frame update
    void Start()
    {
	    //Get components, make sure the game can't start paused
	    playerInt = GameObject.Find("3rd Person Character").GetComponent<PlayerInteraction>();
        pauseUI = gameObject.transform.Find("Top").gameObject;
        pauseUI.SetActive(false);
        settingUI = gameObject.transform.Find("Settings").gameObject;
        settingUI.SetActive(false);
        helpUI = gameObject.transform.Find("Help").gameObject;
        helpUI.SetActive(false);
        backgrnd = gameObject.transform.Find("Background").gameObject;
        //Debug.Log(backgrnd);
        backgrnd.SetActive(false);
        //backgrnd.transform.localScale = new Vector3(Screen.width, Screen.height, 1);
    }

    private void Update()
    {
        //IF pause key is pressed
        if (Input.GetKeyDown(KeyCode.Escape))
        {
        	if(!playerInt.inDialogue){
	            //IF not paused, pause
	            if (!isPaused)
	            {
	                pause();
	            }
	            //IF paused, resume
	            else
	            {
	                resume();
	            }
        	}
            
        }
    }

    //Pause the game by freezing time and enabling the menu
    void pause()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.pauseGameAudio, this.transform.position);
        Time.timeScale = 0;
        pauseUI.SetActive(true);
        backgrnd.SetActive(true);
        isPaused = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    //Unfreeze time, hide the menu
    public void resume()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.resumeGameAudio, this.transform.position);
        Time.timeScale = 1;
        pauseUI.SetActive(false);
        helpUI.SetActive(false);
        settingUI.SetActive(false);
        backgrnd.SetActive(false);
        isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    //Show settings menu
    public void settings()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.pauseGameAudio, this.transform.position);
        pauseUI.SetActive(false);
        settingUI.SetActive(true);
    }

    //Show help menu
    public void help()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.pauseGameAudio, this.transform.position);
        pauseUI.SetActive(false);
        helpUI.SetActive(true);
    }

    //Return to top level pause menu
    public void back()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.resumeGameAudio, this.transform.position);
        pauseUI.SetActive(true);
        helpUI.SetActive(false);
        settingUI.SetActive(false);
    }

    //Return to main menu
    public void mainMenu()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.pauseGameAudio, this.transform.position);
        Time.timeScale = 1;
        isPaused = false;
        SceneManager.LoadScene(0);
    }

    //Quit game
    public void quit()
    {
        Debug.Log("Quitting game from pause");
        Application.Quit();
    }
}
