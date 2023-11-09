//Author: Brian Meginness + Travis Parks
//Debugging: Brian Meginness
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controls : MonoBehaviour
{
    public Dictionary<string, KeyCode> keys;
    public Dictionary<KeyCode, bool> inUse;

    public static bool exists = false;

    private void Start()
    {
        exists = true;
    }

    private void Awake()
    {
        if (!exists)
        {
            GameObject.DontDestroyOnLoad(this);

            if (SaveData.Instance.controlData == null || SaveData.Instance.controlData.keys.Count == 0)

            {
	            //Debug.Log("No Save data found for bindings");
                //A dictionary containing game actions and associated keys
                keys = new Dictionary<string, KeyCode>()
            {
                {"walkUp",KeyCode.W},
                {"walkDown",KeyCode.S},
                {"walkLeft",KeyCode.A},
                {"walkRight",KeyCode.D},
                {"jump",KeyCode.Space},
                {"dash",KeyCode.LeftShift},
                {"interact",KeyCode.F},
                {"attack",KeyCode.Mouse0},
                //{"aim",KeyCode.Mouse1},
                {"zoom",KeyCode.Mouse1},
                {"leftWeaponMenu",KeyCode.Q},
                {"rightWeaponMenu",KeyCode.E},
                {"switchCam", KeyCode.Mouse2}
            };

                //Dictionary for what keys on the keyboard are in use
                inUse = new Dictionary<KeyCode, bool>();

                //FOR all possible keys, set to not in use
                foreach (KeyCode key in System.Enum.GetValues(typeof(KeyCode)))
                {
                    try
                    {
                        inUse.Add(key, false);


                    }
                    catch
                    {

                    }

                }

                //FOR each key being used, set in use to true
                foreach (KeyCode key in keys.Values)
                {
                    inUse[key] = true;
                }

                SaveKeyBindings();
                SaveLoad.SaveProgress();


            }
            else
            {
	            //Debug.Log("Save data found for bindings");
                keys = new Dictionary<string, KeyCode>();
                inUse = new Dictionary<KeyCode, bool>();

                foreach (var key in SaveData.Instance.controlData.keys)
                {
                    keys.Add(key.key, (KeyCode)key.keyCode);
                }

                foreach (var val in SaveData.Instance.controlData.inUse)
                {
                    inUse.Add((KeyCode)val.keyCode, val.inUse);
                }
            }

            


        }
        else
        {
            Destroy(this);
        }
    }

    public void SaveKeyBindings()
    {
	    //Debug.Log(" Saving keybinds");
        SaveData.Instance.controlData = new ControlData();
        foreach (var kvp in inUse)
        {
            
            SaveData.Instance.controlData.inUse.Add(new InUse((int)kvp.Key, kvp.Value));
        }
        foreach (var kvp in keys)
        {
            SaveData.Instance.controlData.keys.Add(new Keys(kvp.Key, (int)kvp.Value));
        }
    }
}
