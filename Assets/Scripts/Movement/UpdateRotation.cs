using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//updates what direction the player model is facing
//should make it diffeerentiate between normal rotations and gravity shift rotations so I can assign them different values
//this script also handles the aiming logic, controlling how fast hte player rotates, what they rotate around, etc. 
public class UpdateRotation : MonoBehaviour

{
	[SerializeField]
	GameObject point;
	[SerializeField]
	float rotationSpeed = 720f;
	[SerializeField]
	float airRotationSpeed = 240f;
	[SerializeField]
	float gravRotationSpeed = 240f;
	[SerializeField]
	float camRotationSpeed = 240f;
	[SerializeField]
    GameObject player = default;
    Movement sphere; 
    Rigidbody body;
    Vector3 DummyGrav;
    bool Gate = true;
    bool gravSwap;
	[SerializeField]
	public bool isAiming;
	[SerializeField]
	GameObject aimingCamera;
	[SerializeField]
	Transform aimPoint;
	[SerializeField]
	Transform basePoint;
	Camera controllingCam;
	[SerializeField]
	float cameraBlendRate = 500f;
    // Start is called before the first frame update
    void Start()
    {
		if(aimingCamera.GetComponent<OrbitCamera>() != null){
			controllingCam = aimingCamera.GetComponent<OrbitCamera>().controllingCam.GetComponent<Camera>();
		}
		transform.rotation = Quaternion.LookRotation( transform.forward , CustomGravity.GetUpAxis(transform.position));
        sphere = player.GetComponent<Movement>();
        body = player.GetComponent<Rigidbody>();
    }
//	void resetGate(){
	//	Gate = false;
	//}

    void Update() {
        //transform.Rotate(0,5,0);
		if(aimingCamera.GetComponent<OrbitCamera>() != null){
			if(isAiming){
				if(controllingCam.fieldOfView > aimingCamera.GetComponent<OrbitCamera>().aimFOV){
					controllingCam.fieldOfView -= Time.deltaTime * cameraBlendRate;
				}
				if(controllingCam.fieldOfView < aimingCamera.GetComponent<OrbitCamera>().aimFOV){
					controllingCam.fieldOfView += Time.deltaTime * cameraBlendRate;
				}
				if(aimingCamera.GetComponent<OrbitCamera>().focus != aimPoint){
					aimingCamera.GetComponent<OrbitCamera>().focus = aimPoint;
				}

				if(this.transform.up != CustomGravity.GetUpAxis(this.transform.position)){
					//Debug.Log("Gravity Mismatch!");
					//if(Gate == false){
						Quaternion toRotation = Quaternion.LookRotation( transform.forward , CustomGravity.GetUpAxis(this.transform.position) );
						this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, (gravRotationSpeed) * Time.deltaTime);
					//}
					//else{
					//	Invoke("resetGate", .3f);
					//}
				}
				else{
					Quaternion toRotation2 = Quaternion.LookRotation(ProjectDirectionOnPlane(aimingCamera.transform.forward, CustomGravity.GetUpAxis(this.transform.position)), CustomGravity.GetUpAxis(this.transform.position));
					this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation2, (camRotationSpeed) * Time.deltaTime);
				}
			}
			else{
				if(controllingCam.fieldOfView < aimingCamera.GetComponent<OrbitCamera>().baseFOV){
					controllingCam.fieldOfView += Time.deltaTime * cameraBlendRate;
				}
				if(controllingCam.fieldOfView > aimingCamera.GetComponent<OrbitCamera>().baseFOV){
					controllingCam.fieldOfView -= Time.deltaTime * cameraBlendRate;
				}
				if(aimingCamera.GetComponent<OrbitCamera>().focus != basePoint){
					aimingCamera.GetComponent<OrbitCamera>().focus = basePoint;
				}
				
				UpdateSpins();
			}
		}


    }
    void UpdateSpins()
    {
		Vector3 player2Pointer = sphere.ProjectDirectionOnPlane(point.transform.position - transform.parent.gameObject.transform.position, CustomGravity.GetUpAxis(transform.position));
		Debug.DrawRay(this.transform.position, player2Pointer, Color.gray, 3f);
        Vector3 gravity = CustomGravity.GetUpAxis(this.transform.position);
		if(this.transform.up != CustomGravity.GetUpAxis(this.transform.position)){
			//Debug.Log("Gravity Mismatch!");
		//	if(Gate == false){
				Quaternion toRotation = Quaternion.LookRotation( transform.forward , CustomGravity.GetUpAxis(this.transform.position) );
				this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, (gravRotationSpeed) * Time.deltaTime);
		//	}
		//	else{
		//		Invoke("resetGate", .3f);
		//	}
		}
		else{
			Gate = true;
		}
		if (sphere.velocity.magnitude > .2f && (sphere.playerInput != Vector3.zero)){
			if(!sphere.OnGround){
				Quaternion toRotation = Quaternion.LookRotation(ProjectDirectionOnPlane(player2Pointer, gravity), gravity);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, airRotationSpeed * Time.deltaTime);
			}
			else{
				Quaternion toRotation = Quaternion.LookRotation(ProjectDirectionOnPlane(player2Pointer, gravity), gravity);
				transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, rotationSpeed * Time.deltaTime);
			}
			//Debug.Log("Moving");

		}
	}
    Vector3 ProjectDirectionOnPlane (Vector3 direction, Vector3 normal) {
		return (direction - normal * Vector3.Dot(direction, normal)).normalized;
	}
}
