using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public enum upgrade{ShockProng, ShockSpike, Missile, HomingMissiles, JetBooster, VerticalBooster};
    public upgrade whatUpgrade;
    /// <summary>
    /// OnCollisionEnter is called when this collider/rigidbody has begun
    /// touching another rigidbody/collider.
    /// </summary>
    /// <param name="other">The Collision data associated with this collision.</param>
    void OnTriggerEnter(Collider other)
    {
        Debug.Log("made it past point #1");
        if(other.gameObject.tag == "Player"){
            Debug.Log("made it past point #2" + other);
            if(whatUpgrade == upgrade.ShockProng){
                Debug.Log("made it past point #3");
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
            Destroy(this.gameObject);
        }
    }

}
