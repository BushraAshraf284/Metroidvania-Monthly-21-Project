using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    [SerializeField]
    bool isSpike;
    [SerializeField]
    GameObject stuckSpikePrefab;
    [SerializeField]
    bool isMissile;
    Rigidbody body;
    bool gate = true;
    bool burstGate = true;
    Transform target;
    [SerializeField]
    float missileSpeed = 15f;
    [SerializeField]
    float spikeSpeed = 80f;
    [SerializeField]
    float missilePower, missileRadius, missileUpModifier;
    Vector3 missileExplosionPos;
    // Start is called before the first frame update
    public void StartForce(Transform t){
        //Debug.Log("Accellerating Rocket! " + target);
        gate = false;
        target = t;
    }
    public void StartBurstForce(Transform t){
        burstGate = false;
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
        if(burstGate == false){
            this.transform.rotation = Quaternion.LookRotation((transform.position - target.position), CustomGravity.GetUpAxis(this.transform.position));
            body.velocity = (target.position - this.transform.position).normalized * spikeSpeed;
            burstGate = true;
        }
    }

    void OnCollisionEnter(Collision other)
    {
        if(isSpike){
            if(other.gameObject.GetComponent<Shatter>()!= null){
                other.gameObject.GetComponent<Shatter>().oneShot(0);
            }
            else{
                GameObject stuckSpike = Instantiate(stuckSpikePrefab);
                stuckSpike.transform.position = transform.position;
                stuckSpike.transform.forward = -transform.forward;
                //stuckSpike.transform.SetParent(other.collider.transform, true);
                stuckSpike.transform.parent = other.collider.transform;
                Destroy(gameObject);
            }

        }
        if(isMissile){
            missileExplosionPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(missileExplosionPos, missileRadius);
            Collider[] colliders2 = Physics.OverlapSphere(missileExplosionPos, missileRadius/5);
            Collider[] colliders3 = Physics.OverlapSphere(missileExplosionPos, missileRadius/25);
            foreach (Collider hit in colliders3)
        	{
                if (hit.gameObject.GetComponent<Shatter>() != null){
                    hit.gameObject.GetComponent<Shatter>().oneShot(0);
                }
            }
            foreach (Collider hit in colliders2)
        	{
                if (hit.gameObject.GetComponent<Shatter>() != null){
                    hit.gameObject.GetComponent<Shatter>().takeDamage();
                }
            }
        	foreach (Collider hit in colliders)
        	{
                Rigidbody rb = hit.GetComponent<Rigidbody>();
                //Explosive forse! spawn explosion, knock stuff back, etc
                if (rb != null){
                    rb.AddExplosionForce(missilePower, missileExplosionPos, missileRadius, missileUpModifier);
                }
                Destroy(gameObject);
            }
        }
        

    }
}
