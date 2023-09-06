using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIconRotate : MonoBehaviour
{
    PlayerInteraction interact; 
    // Start is called before the first frame update
    //float lockPos = 0f;
    float xStore;
    float yStore;
    void Start()
    {
        interact = GameObject.Find("3rd Person Character").GetComponent<PlayerInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawRay(this.transform.position, this.transform.forward, Color.blue, 1f);
        //not working currently
        //Quaternion toRotation2 = Quaternion.LookRotation(ProjectDirectionOnPlane( (this.transform.position - interact.nearestInteractable.position) , CustomGravity.GetUpAxis(this.transform.right)), CustomGravity.GetUpAxis(this.transform.right));
		//this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation2, (999f) * Time.deltaTime);
        xStore = transform.rotation.x;
        yStore = transform.rotation.y;
        //transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, lockPos, lockPos);
        transform.forward = (interact.nearestInteractable.position - this.transform.position);
        //this.gameObject.transform.up = ProjectDirectionOnPlane(this.gameObject.transform.up, CustomGravity.GetUpAxis(this.transform.position));
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z);
        //this.gameObject.transform.up = ProjectDirectionOnPlane((interact.nearestInteractable.position - this.transform.position) , CustomGravity.GetUpAxis(this.transform.position));
    }
    Vector3 ProjectDirectionOnPlane (Vector3 direction, Vector3 normal) {
		return (direction - normal * Vector3.Dot(direction, normal)).normalized;
	}
}
