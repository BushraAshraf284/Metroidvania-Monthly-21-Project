using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
//placed on the abttery object, similar to console except dependent on a charge
public class Battery : MonoBehaviour
{   [SerializeField]
    bool extraActivation;
    [SerializeField]
    public List<GameObject> platforms = new List<GameObject>();
    [SerializeField]
    float maxCharge = 100f;
    [SerializeField]
    float charge;
    public float batteryDrainTimer;
    [SerializeField]
    [Tooltip("time it takes before a battery starts draining")]
    float batteryDrainCap;
    [SerializeField]
    float batteryDrainRate;
    [SerializeField]
    float chargeRate;
    public bool charging;
    bool activationGate = false;
    bool activationGate2 = false;
    //added recently
    bool beenCharged;
    SkinnedMeshRenderer skin;
    void Start(){
        skin = GetComponent<SkinnedMeshRenderer>();
    }
    void Update()
    {
        DrainBattery();
        if(charge > 0){
            if(!activationGate){
                activationGate2 = false;
                activationGate = true;
                foreach (GameObject p in platforms){
                        if(p.GetComponent<platformAnimController>() != null){
                            if(extraActivation){
                                Debug.Log("Moving a platform");
                                p.GetComponent<platformAnimController>().ExtraActivated();
                                beenCharged = true;
                            }
                            else{
                                Debug.Log("Moving a platform", this.gameObject);
                                p.GetComponent<platformAnimController>().Activated();
                                beenCharged = true;
                            }
                    }
                }
            }
        }
        else{
            if(beenCharged){
                if(!activationGate2){
                    activationGate = false;
                    activationGate2 = true;
                    foreach (GameObject p in platforms){
                        if(p.GetComponent<platformAnimController>() != null){
                            if(extraActivation){
                                Debug.Log("Reverting a platform", this.gameObject);
                                p.GetComponent<platformAnimController>().ResetExtraActivated();
                            }
                            else{
                                Debug.Log("Reverting a platform", this.gameObject);
                                p.GetComponent<platformAnimController>().ResetActivated();
                            }
                        }
                    }
                }
            }
        }
        skin.SetBlendShapeWeight(0, 100-(charge/maxCharge)*100);
    }
    public void ChargeBattery(){
		if (charge + (Time.deltaTime*chargeRate) > maxCharge){
            batteryDrainTimer = 0f;
			charge = maxCharge;
            charging = true;
            //running a million timesx
            Debug.Log("Battery at capacity!");
		}
		else {
			if(charge < maxCharge){
                Debug.Log("Charging a Battery!");
                batteryDrainTimer = 0f;
				charge += (Time.deltaTime*chargeRate);
                charging = true;
			}
		}
	}
    public void DrainBattery(){
        if(charge > 0){
            if(batteryDrainTimer < batteryDrainCap){
                batteryDrainTimer += Time.deltaTime;
            }
            else{
                Debug.Log("Timer up, draining battery back to zero");
                charge = charge - (Time.deltaTime * batteryDrainRate);
            }
        }
        else if (charge <= 0){
            charge = 0;
            batteryDrainTimer = 0f;
        }
    }




    public void Interact(){
        Debug.Log("made it into console");
        if(platforms.Count > 0){
            foreach (GameObject p in platforms){
                if(p.GetComponent<platformAnimController>() != null){
                    if(extraActivation){
                        Debug.Log("Moving a platform");
                        p.GetComponent<platformAnimController>().ExtraActivated();
                    }
                    else{
                        Debug.Log("Moving a platform");
                        p.GetComponent<platformAnimController>().Activated();
                    }
                }
            }
        }
    }
}
