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
	[SerializeField]
	bool hasManager;
	[SerializeField]
	GameObject targetManager;
	bool down;
	public void ResetAnim(){
		down = false;
		gameObject.tag = "Targetable";
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
		if(hasManager){
			if(targetManager.GetComponent<TargetManager>()!=null){
				targetManager.GetComponent<TargetManager>().ResetTarget();
			}
		}
	}
	public void Triggered(){
		if(!down){
			down = true;
			gameObject.tag = "Untagged";
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
			if(hasManager){
				if(targetManager.GetComponent<TargetManager>()!=null){
					targetManager.GetComponent<TargetManager>().HitTarget();
				}
			}
		}
	}
}
