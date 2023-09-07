using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Console : MonoBehaviour
{
    public enum consoleType { DoorsAndPlatforms, Event };
    public consoleType type;
    [SerializeField]
    public List<GameObject> platforms = new List<GameObject>();
    [SerializeField]
    bool oneWay;
    [SerializeField]
    float time;
    bool gate = true;

    public void Interact()
    {
        Debug.Log("made it into console");
        if (type == consoleType.DoorsAndPlatforms)
        {
            if (platforms.Count > 0)
            {
                foreach (GameObject p in platforms)
                {
                    if (p.GetComponent<platformAnimController>() != null)
                    {
                        if (oneWay)
                        {
                            AudioManager.instance.PlayOneShot(FMODEvents.instance.interactSFX, this.transform.position);
                            //p.GetComponent<platformAnimController>().sound.start()
                            gate = false;
                            Debug.Log("Moving a platform");
                            p.GetComponent<platformAnimController>().ForceActivated();
                        }
                        else
                        {
                            // loop
                            //p.GetComponent<platformAnimController>().sound.start()
                            gate = false;
                            p.GetComponent<platformAnimController>().TempActivation(time);
                        }
                    }
                }
            }
        }
        else if (type == consoleType.Event)
        {
        }
    }

    void Update()
    {
        if (!gate)
        {
            foreach (GameObject p in platforms)
            {
                if (p.GetComponent<platformAnimController>() != null)
                {
                    if (!p.GetComponent<platformAnimController>().AnimatorIsPlaying())
                    {
                        //p.GetComponent<platformAnimController>().sound.stop()
                    }
                }
            }
        }
    }
}