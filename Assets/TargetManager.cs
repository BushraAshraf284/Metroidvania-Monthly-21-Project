using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
    [SerializeField]
    int targetAmount;
    [SerializeField]
    int targetsHit;
    // Start is called before the first frame update

    public void HitTarget(){
        targetsHit += 1;
        if(targetsHit >= targetAmount){
            Debug.Log("SUCCESS!!");
            if(GetComponent<Console>()!=null){
                GetComponent<Console>().Interact();
            }
        }
    }
    public void ResetTarget(){
        targetsHit -= 1;
        if(targetsHit <= 0){
            targetsHit = 0;
        }
    }

}
