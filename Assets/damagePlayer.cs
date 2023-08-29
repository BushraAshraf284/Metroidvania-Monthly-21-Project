using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour
{
    [SerializeField]
    float damageAmount = 5f;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player"){
            Debug.Log("Hoit P{layer}");
            if(other.gameObject.GetComponent<PlayerStats>()!= null){
                Debug.Log("Hoit P{layer} 2 ");
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(damageAmount);
            }
        }
        Destroy(this.transform.parent.gameObject);
    }

}
