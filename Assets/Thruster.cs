using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    Rigidbody body;
    bool gate = true;
    Transform target;
    [SerializeField]
    float missileSpeed = 15f;
    // Start is called before the first frame update
    public void StartForce(Transform t){
        gate = false;
        target = t;
    }
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(gate == false){
            this.transform.rotation = Quaternion.LookRotation((transform.position - target.position), CustomGravity.GetUpAxis(this.transform.position));
            body.velocity = (target.position - this.transform.position).normalized * missileSpeed ;
        }
    }
}
