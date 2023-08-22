using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;
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
	[SerializeField]
	public bool isAiming;
	[SerializeField]
	//orbitcamera object, basically the camera polearm
	GameObject aimingCamera;
	[SerializeField]
	//empty that defines the camera position while aiming
	Transform aimPoint;
	[SerializeField]
	//empty that defines the camera position while not aiming
	Transform basePoint;
	//The actual camera object on the polearm
	Camera controllingCam;
	//reference to the controls component, needed to bind configs
	Controls controls;
	// controls blending rate of FOV 
	public float t = 0.5f;
    // gameobject that is ray cast from the camera forward to give the aiming IK an object to aim towards
	[SerializeField]
	GameObject aimCast;
	[SerializeField]
	LayerMask mask;
	RaycastHit raycastHit;
	[SerializeField]
	Rig rig;

    void Start()
    {
		controls = GameObject.Find("Data").GetComponentInChildren<Controls>();
		if(aimingCamera.GetComponent<OrbitCamera>() != null){
			controllingCam = aimingCamera.GetComponent<OrbitCamera>().controllingCam.GetComponent<Camera>();
		}
		transform.rotation = Quaternion.LookRotation( transform.forward , CustomGravity.GetUpAxis(transform.position));
        sphere = player.GetComponent<Movement>();
    }

    void Update() {
		if(Input.GetKey(controls.keys["aim"]) && !FindObjectOfType<PauseMenu>().isPaused && !sphere.moveBlocked){
			rig.weight = 1f;
			isAiming = true;
			aimCast.SetActive(true);
			if(Physics.Raycast(controllingCam.transform.position, ProjectDirectionOnPlane(controllingCam.transform.forward, this.transform.right), out raycastHit, 999f, mask)){
				//Debug.DrawLine(this.transform.position, raycastHit.point, Color.red, 5f);
				if (aimCast.transform.position != raycastHit.point){
					aimCast.transform.position = Vector3.Lerp(aimCast.transform.position, raycastHit.point, Time.deltaTime*25f);
				}
				//aimCast.transform.position = raycastHit.point;
			}

		}
		else if(!Input.GetKey(controls.keys["aim"]) && !FindObjectOfType<PauseMenu>().isPaused && !sphere.moveBlocked){
			rig.weight = 0f;
			isAiming = false;
			aimCast.SetActive(false);
		}
		if(aimingCamera.GetComponent<OrbitCamera>() != null){
			if(isAiming){

				controllingCam.fieldOfView = Mathf.Lerp(controllingCam.fieldOfView, aimingCamera.GetComponent<OrbitCamera>().aimFOV, t);

				if(Mathf.Approximately(Mathf.Round(controllingCam.fieldOfView), Mathf.Round(aimingCamera.GetComponent<OrbitCamera>().aimFOV))){
					if(controllingCam.fieldOfView != aimingCamera.GetComponent<OrbitCamera>().aimFOV){
						controllingCam.fieldOfView = aimingCamera.GetComponent<OrbitCamera>().aimFOV;
					}
				}
				
				
				
				if(aimingCamera.GetComponent<OrbitCamera>().focus != aimPoint){
					aimingCamera.GetComponent<OrbitCamera>().focus = aimPoint;
				}

				if(this.transform.up != CustomGravity.GetUpAxis(this.transform.position)){
						Quaternion toRotation = Quaternion.LookRotation( transform.forward , CustomGravity.GetUpAxis(this.transform.position) );
						this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, (gravRotationSpeed) * Time.deltaTime);
				}
				else{
					Quaternion toRotation2 = Quaternion.LookRotation(ProjectDirectionOnPlane(aimingCamera.transform.forward, CustomGravity.GetUpAxis(this.transform.position)), CustomGravity.GetUpAxis(this.transform.position));
					this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation2, (camRotationSpeed) * Time.deltaTime);
				}
			}
			else{
				controllingCam.fieldOfView = Mathf.Lerp(controllingCam.fieldOfView, aimingCamera.GetComponent<OrbitCamera>().baseFOV, t);

				if(Mathf.Approximately(Mathf.Round(controllingCam.fieldOfView), Mathf.Round(aimingCamera.GetComponent<OrbitCamera>().baseFOV))){
					if(controllingCam.fieldOfView != aimingCamera.GetComponent<OrbitCamera>().baseFOV){
						controllingCam.fieldOfView = aimingCamera.GetComponent<OrbitCamera>().baseFOV;
					}
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
				Quaternion toRotation = Quaternion.LookRotation( transform.forward , CustomGravity.GetUpAxis(this.transform.position) );
				this.transform.rotation = Quaternion.RotateTowards (transform.rotation, toRotation, (gravRotationSpeed) * Time.deltaTime);
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
