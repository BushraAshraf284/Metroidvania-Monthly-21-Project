using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMagneticField : MonoBehaviour
{
    [SerializeField]
    [Tooltip("true = make gravity positive, false = make gravity negative")]
    bool polarity;
    [SerializeField]
    GravityPlane plane;
    void Start(){

    }
    void OnTriggerEnter(Collider other)
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.gravitySwitchSound, this.transform.position);
        if (polarity){
            if(other.gameObject.tag == "Player"){
                if(plane.gravity < 0){
                    plane.gravity = plane.gravity * -1f;
                }
                if (other.transform.root.gameObject.GetComponent<MovementSpeedController>() != null){
                    other.transform.root.gameObject.GetComponent<MovementSpeedController>().slowed = false;
                }        
            }
        }
        else{
            if(other.gameObject.tag == "Player"){
                if(plane.gravity > 0){
                    plane.gravity = plane.gravity * -1f;
                }
                if (other.transform.root.gameObject.GetComponent<MovementSpeedController>() != null){
                    other.transform.root.gameObject.GetComponent<MovementSpeedController>().slowed = true;
                }        
            }
        }
    }

}
