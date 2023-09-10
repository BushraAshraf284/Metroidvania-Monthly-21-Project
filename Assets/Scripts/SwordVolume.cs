using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordVolume : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Shatter>()!=null){
            other.gameObject.GetComponent<Shatter>().takeDamage();
        }
    }
}
