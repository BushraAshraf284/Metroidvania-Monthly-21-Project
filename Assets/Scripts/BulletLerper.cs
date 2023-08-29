using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BulletLerper : MonoBehaviour
{
    bool gate = true;
    Vector3 dest;
    [SerializeField]
    float time = .001f;
    [SerializeField]
    float bulletLerpDistance = .005f;
    Rigidbody body;
    Vector3 start;
    // Start is called before the first frame update
    void Start()
    {
        body = this.GetComponent<Rigidbody>();
        Destroy(this.gameObject, 5f);
    }

    [SerializeField]
    float damageAmount = 5f;
    void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == "Player"){
            if(other.gameObject.GetComponent<PlayerStats>()!= null){
                other.gameObject.GetComponent<PlayerStats>().TakeDamage(damageAmount);
            }
        }
        Destroy(this.gameObject);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!gate){
            
            body.MovePosition(this.transform.position + start * (Time.deltaTime * time));
            //body.velocity = (dest - start).normalized * (Time.deltaTime * time) ;
        }
    }
    public void Lerp(Vector3 dest2, Vector3 origin){
        gate = false;
        dest = dest2;
        start = origin;
        this.transform.rotation = Quaternion.LookRotation((transform.position - dest), CustomGravity.GetUpAxis(this.transform.position));
    }
}
