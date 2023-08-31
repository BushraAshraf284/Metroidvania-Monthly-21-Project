using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetable : MonoBehaviour
{
    [SerializeField]
	public Transform targetFocusPoint;
	[SerializeField]
	public bool HasEvent;
	public bool hasAnim;
	public bool hasResetTime;
	public float resetTime = 5f;
	[SerializeField]
	bool animatorInParent;
	public void ResetAnim(){
		if(animatorInParent){
			if(this.transform.parent.gameObject.GetComponent<Animator>()!= null){
				this.transform.parent.gameObject.GetComponent<Animator>().SetBool("Activated", false);
			}
		}
		else{
			if(this.gameObject.GetComponent<Animator>()!= null){
				this.gameObject.GetComponent<Animator>().SetBool("Activated", false);
			}
		}
	}
	public void Triggered(){
		if(HasEvent){
			if(this.gameObject.GetComponent<Console>()!= null){
				Debug.Log("Targetable thing you hit has has a valid console script!");
				this.gameObject.GetComponent<Console>().Interact();
			}
		}
		if(hasAnim){
			if(animatorInParent){
				if(this.transform.parent.gameObject.GetComponent<Animator>()!= null){
					this.transform.parent.gameObject.GetComponent<Animator>().SetBool("Activated", true);
					if(hasResetTime){
						Invoke("ResetAnim", resetTime);
					}
				}
			}
			else{
				if(this.gameObject.GetComponent<Animator>()!= null){
					this.gameObject.GetComponent<Animator>().SetBool("Activated", true);
					if(hasResetTime){
						Invoke("ResetAnim", resetTime);
					}
				}
			}
		}
	}

}
