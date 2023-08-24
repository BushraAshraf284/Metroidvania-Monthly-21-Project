using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//keeps track of which direction the player is inputting in  
public class RotationPointer : MonoBehaviour
{
    [SerializeField]
    float maxSpeed = 10;
    [SerializeField]
    GameObject player = default;
    Movement sphere; 
    [SerializeField]
    Transform playerinputSpace = default;
    // Start is called before the first frame update
    void Start()
    {
        sphere = player.GetComponent<Movement>();
    }

    // Update is called once per frame
	void Update () {
		Vector3 playerInput;
		playerInput.y = (Input.GetKey(sphere.controls.keys["walkUp"]) ? 1 : 0) - (Input.GetKey(sphere.controls.keys["walkDown"]) ? 1 : 0);; //Input.GetAxis("Horizontal");
		playerInput.x = (Input.GetKey(sphere.controls.keys["walkRight"]) ? 1 : 0) - (Input.GetKey(sphere.controls.keys["walkLeft"]) ? 1 : 0); // Input.GetAxis("Vertical");
        if(Input.GetKey(sphere.controls.keys["walkUp"]) && Input.GetKey(sphere.controls.keys["walkDown"])){
            playerInput.y = 0f;
        }
            if(Input.GetKey(sphere.controls.keys["walkLeft"]) && Input.GetKey(sphere.controls.keys["walkRight"])){
            playerInput.x = 0f;
        }
        transform.localPosition = sphere.ProjectDirectionOnPlane(playerinputSpace.TransformDirection(playerInput.x, 0f, playerInput.y) * maxSpeed, CustomGravity.GetUpAxis(transform.position) );
        
		//transform.localPosition = new Vector3(playerInput.x, 0.5f, playerInput.y);
	}
}
