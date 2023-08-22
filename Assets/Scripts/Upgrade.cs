using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public enum upgrade{ShockProng, ShockSpike, Missile, HomingMissiles, JetBooster, VerticalBooster, Sword};
    public upgrade whatUpgrade;
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
            Destroy(this.gameObject);
        }
    }

}
