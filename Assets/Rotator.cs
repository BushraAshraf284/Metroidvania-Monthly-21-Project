using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
 

	//adjust this to change speed
	[SerializeField]
	float speed = 5f;
	//adjust this to change how high it goes
	[SerializeField]
	float height = 0.5f;
	float tempRotation;
	[SerializeField]
	float rotation  = 15f;
	Vector3 pos;
	float accelleratedRotation;
	[SerializeField]
	float boostAmount = 200f;
	void resetAccellertedRotation(){
		accelleratedRotation = tempRotation;
	}
	private void Start()
	{
		tempRotation = rotation;
		accelleratedRotation = rotation;
		pos = transform.position;
	}
	void Update()
	{
		if(rotation != accelleratedRotation){
			rotation = Mathf.Lerp(rotation, accelleratedRotation, Time.deltaTime);
		}
		//calculate what the new Y position will be
		float newY = Mathf.Sin(Time.time * speed) * height + pos.y;
		//set the object's Y to the new calculated Y
		transform.position = new Vector3(transform.position.x, newY, transform.position.z) ;
		transform.Rotate(new Vector3(0, 0, rotation) * Time.deltaTime);
	} 
	public void RampRotation(){
		//Debug.Log("Ramping Rotation");
		accelleratedRotation = boostAmount;
		Invoke("resetAccellertedRotation", .2f);
	}
}
