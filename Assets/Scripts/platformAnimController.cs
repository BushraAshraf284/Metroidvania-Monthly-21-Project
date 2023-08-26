using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformAnimController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void ResetActivated()
    {
        anim.SetBool("Activated", false);
    }
    public void Activated()
    {
        anim.SetBool("Activated", true);
    }
    public void TempActivation(float time)
    {
        anim.SetBool("Activated", true);
        Invoke("ResetActivated", time);
    }
}