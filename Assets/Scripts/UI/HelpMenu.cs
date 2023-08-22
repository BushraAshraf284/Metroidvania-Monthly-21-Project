//Author: Brian Meginness
//Debugging: Brian Meginness, Travis Parks
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpMenu : MonoBehaviour
{
    //Menu images
    Image aim;
    Image moveLeft;
    Image moveRight;
    Image moveUp;
    Image moveDown;
    Image jump;
    Image dash;
    Image interact;
    Image attack;
    Image leftweaponmenu;
	Image rightweaponmenu;
	Image switchcam;
    GameObject errTxt;
    GameObject promptTxt;
    GameObject rebindTxt;

    Controls controls;

    private static IEnumerator err;

    private void Start()
    {
        //Assign components
        controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
        aim = GameObject.Find("Aim").GetComponent<Image>();
        moveLeft = GameObject.Find("WalkLeft").GetComponent<Image>();
        moveRight = GameObject.Find("WalkRight").GetComponent<Image>();
        moveUp = GameObject.Find("WalkForward").GetComponent<Image>();
        moveDown = GameObject.Find("WalkBack").GetComponent<Image>();
        jump = GameObject.Find("Jump").GetComponent<Image>();
        dash = GameObject.Find("Dash").GetComponent<Image>();
        interact = GameObject.Find("Interact").GetComponent<Image>();
        attack = GameObject.Find("Attack").GetComponent<Image>();
        leftweaponmenu = GameObject.Find("LeftWeaponMenu").GetComponent<Image>();
	    rightweaponmenu = GameObject.Find("RightWeaponMenu").GetComponent<Image>();
	    switchcam = GameObject.Find("SwitchCam").GetComponent<Image>();
        errTxt = GameObject.Find("ErrorText");
        promptTxt = GameObject.Find("PromptText");
        rebindTxt = GameObject.Find("RebindText");


        //Update controls
        if (controls)
        {
            draw();
        }
    }

    //Update controls when menu is opened
    private void OnEnable()
    {
        if (controls)
        {
            draw();
        }
    }

    //When a control button is pressed
    public void changeControl(string action)
    {
        StartCoroutine(WaitForKeyPress(action));
    }

    //Coroutine to wait for user input
    private IEnumerator WaitForKeyPress(string action)
    {
        errTxt.SetActive(false);
        try
        {
            StopCoroutine(err);
        }
        catch { }

        StartCoroutine(prompt());

        //Save previous key for the specified action, set to not in use
        KeyCode oldKey = controls.keys[action];
        controls.inUse[oldKey] = false;

        // Wait for key press
        while (!Input.anyKeyDown)
        {
            yield return null;
        }

        //Figure out what key was pressed
        foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
        {

            if (Input.GetKey(key) && !controls.inUse[key])
            {
                Debug.Log(action + " changed to " + key);
                //Update controls with new key
                controls.keys[action] = key;
                controls.inUse[key] = true;
                draw();
            }
            else if (Input.GetKey(key) && controls.inUse[key])
            {
                err = ErrorMsg();
                StartCoroutine(err);
            }
        }

    }

    private IEnumerator ErrorMsg()
    {
        errTxt.SetActive(true);
        yield return new WaitForSecondsRealtime(3);
        errTxt.SetActive(false);
        rebindTxt.SetActive(true);
    }

    private IEnumerator prompt()
    {
        rebindTxt.SetActive(false);
        promptTxt.SetActive(true);
        yield return new WaitUntil(() => Input.anyKey);
        promptTxt.SetActive(false);
    }

    private void draw()
    {
        //Get Keys
        KeyCode aimKey = controls.keys["aim"];
        KeyCode leftKey = controls.keys["walkLeft"];
        KeyCode rightKey = controls.keys["walkRight"];
        KeyCode upKey = controls.keys["walkUp"];
        KeyCode downKey = controls.keys["walkDown"];
        KeyCode jumpKey = controls.keys["jump"];
        KeyCode dashKey = controls.keys["dash"];
        KeyCode interKey = controls.keys["interact"];
        KeyCode attackKey = controls.keys["attack"];
        KeyCode leftWeaponMenuKey = controls.keys["leftWeaponMenu"];
	    KeyCode rightWeaponMenuKey = controls.keys["rightWeaponMenu"];
	    KeyCode switchKey = controls.keys["switchCam"];

        errTxt.SetActive(false);
        promptTxt.SetActive(false);
        rebindTxt.SetActive(true);

	    //Set images to associated key sprite, or blank if associated sprite doesn't exist
	    switchcam.sprite = Resources.Load<Sprite>(switchKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(switchKey.ToString()); 
        leftweaponmenu.sprite = Resources.Load<Sprite>(leftWeaponMenuKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(leftWeaponMenuKey.ToString()); 
        rightweaponmenu.sprite = Resources.Load<Sprite>(rightWeaponMenuKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(rightWeaponMenuKey.ToString()); 
        aim.sprite = Resources.Load<Sprite>(aimKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(aimKey.ToString()); 
        moveLeft.sprite = Resources.Load<Sprite>(leftKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(leftKey.ToString());
        moveRight.sprite = Resources.Load<Sprite>(rightKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(rightKey.ToString());
        moveUp.sprite = Resources.Load<Sprite>(upKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(upKey.ToString());
        moveDown.sprite = Resources.Load<Sprite>(downKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(downKey.ToString());
        jump.sprite = Resources.Load<Sprite>(jumpKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(jumpKey.ToString());
        dash.sprite = Resources.Load<Sprite>(dashKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(dashKey.ToString());
        interact.sprite = Resources.Load<Sprite>(interKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(interKey.ToString());
        attack.sprite = Resources.Load<Sprite>(attackKey.ToString()) == null ? Resources.Load<Sprite>("Blank") : Resources.Load<Sprite>(attackKey.ToString());
    }
}
