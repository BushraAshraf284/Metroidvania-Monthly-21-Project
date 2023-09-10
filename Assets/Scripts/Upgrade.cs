using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
	public bool isPickedUp;
	public enum upgrade{ShockProng, ShockSpike, Missile, HomingMissiles, JetBooster, VerticalBooster, Sword, BatteryPiece, HeartPiece, ShieldPiece, RepairKit};
    public upgrade whatUpgrade;
    [SerializeField]
    [Tooltip("Will this pickup dissapear when picked up?")]
    bool dissapear;
    [SerializeField]
    [Tooltip("Will this pickup reappear after picked up?")]
    bool reappear;
    [SerializeField]
    [Tooltip("How long until this pickup reappears?")]
	float reappearTimer = 5f;
	protected void Start()
	{
		if(isPickedUp){
			if(reappear){
				if(GetComponent<BoxCollider>() != null){
					if(GetComponent<MeshRenderer>()!= null){
						GetComponent<BoxCollider>().enabled = false;
						GetComponent<MeshRenderer>().enabled = false;
						Invoke("ReappearTrigger", reappearTimer);
					}
				}
			}
			else{
				Destroy(this.gameObject);
			}
                
		}
	}
    void ReappearTrigger(){
        if(GetComponent<BoxCollider>() != null){
	        if(GetComponent<MeshRenderer>()!= null){
		        isPickedUp = false;
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            if(whatUpgrade == upgrade.ShockProng){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockShockProng();
                }
            }
            else if(whatUpgrade == upgrade.ShockSpike){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockShockSpike();
                }
            }
            else if(whatUpgrade == upgrade.Missile){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockMissiles();
                }
                
            }
            else if(whatUpgrade == upgrade.HomingMissiles){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockHomingMissiles();
                }
            }
            else if(whatUpgrade == upgrade.JetBooster){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockJetBoost();
                }
            }
            else if(whatUpgrade == upgrade.VerticalBooster){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockVertBoost();
                }
            }
            else if(whatUpgrade == upgrade.Sword){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockSword();
                }
            }
            else if(whatUpgrade == upgrade.HeartPiece){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetHeartUpgrade();
                    // Play audio when collected
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.upgradeCollectedSound, this.transform.position);
                }
            }
            else if(whatUpgrade == upgrade.ShieldPiece){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetShieldUpgrade();
                }
            }
            else if(whatUpgrade == upgrade.BatteryPiece){
                if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetBatteryUpgrade();
                }
            }
            else if(whatUpgrade == upgrade.RepairKit){
	            if( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
		            other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetRepairKit();
	            }
            }
	        isPickedUp = true;
            if(dissapear){
                if(reappear){
                    if(GetComponent<BoxCollider>() != null){
                        if(GetComponent<MeshRenderer>()!= null){
                            GetComponent<BoxCollider>().enabled = false;
                            GetComponent<MeshRenderer>().enabled = false;
                            Invoke("ReappearTrigger", reappearTimer);
                        }
                    }
                }
                else{
                    Destroy(this.gameObject);
                }
                
            }
            
        }
    }

}
