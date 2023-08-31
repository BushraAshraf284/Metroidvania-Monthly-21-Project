using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundIconRotate : MonoBehaviour
{
    Interaction interact; 
    // Start is called before the first frame update
    void Start()
    {
        interact = GameObject.Find("3rd Person Character").GetComponent<Interaction>();
    }

    // Update is called once per frame
    void Update()
    {
        //not working currently
        //Quaternion toRotation2 = Quaternion.LookRotation(ProjectDirectionOnPlane( (this.transform.position - interact.nearestInteractable.position) , CustomGravity.GetUpAxis(this.transform.right)), CustomGravity.GetUpAxis(this.transform.right));
		//this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation2, (999f) * Time.deltaTime);

        //this.gameObject.transform.forward = CustomGravity.GetUpAxis(this.transform.position);
    }
    Vector3 ProjectDirectionOnPlane (Vector3 direction, Vector3 normal) {
		return (direction - normal * Vector3.Dot(direction, normal)).normalized;
	}
}
