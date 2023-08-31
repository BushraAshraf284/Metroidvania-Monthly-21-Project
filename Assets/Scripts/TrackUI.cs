using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackUI : MonoBehaviour
{
    [SerializeField]
    public Transform subject;
    [SerializeField]
    Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(subject){
            transform.position = cam.WorldToScreenPoint(subject.position);
        }
    }
}
