using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMagneticField : MonoBehaviour
{
    [SerializeField]
    GravityPlane plane;
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            plane.gravity = - plane.gravity;
            if (other.transform.root.gameObject.GetComponent<MovementSpeedController>() != null){
                other.transform.root.gameObject.GetComponent<MovementSpeedController>().slowed = true;
            }        
        }
    }
    void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            plane.gravity = - plane.gravity;
            if (other.transform.root.gameObject.GetComponent<MovementSpeedController>() != null){
                other.transform.root.gameObject.GetComponent<MovementSpeedController>().slowed = false;
            }        
        }
    }
}
