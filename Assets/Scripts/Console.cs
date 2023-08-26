using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{   
    public enum consoleType{ DoorsAndPlatforms, Event};
    public consoleType type;
    [SerializeField]
    public List<GameObject> platforms = new List<GameObject>();
    [SerializeField]
    bool oneWay;
    [SerializeField]
    float time;

    public void Interact(){
        Debug.Log("made it into console");
        if(type == consoleType.DoorsAndPlatforms){
            if(platforms.Count > 0){
                foreach (GameObject p in platforms){
                    if(p.GetComponent<platformAnimController>() != null){
                        if(oneWay){
                            Debug.Log("Moving a platform");
                            p.GetComponent<platformAnimController>().Activated();
                        }else{
                            p.GetComponent<platformAnimController>().TempActivation(time);
                        }
                    }
                }
            }
        }
        else if( type == consoleType.Event){
        }
    }
}
