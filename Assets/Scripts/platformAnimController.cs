using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformAnimController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    public bool isActivated;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void ResetActivated(){
        anim.SetBool("Activated", false);
        isActivated = false;
    }
    public void Activated(){
        anim.SetBool("Activated", true);
        isActivated = true;
    }
    public void TempActivation(float time){
        isActivated = true;
        anim.SetBool("Activated", true);
        Invoke("ResetActivated", time);
    }
}
