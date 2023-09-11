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

    GetItemScreen itemScreen;

	string shockProngDesc = "Press and hold the attack buton to charge nearby batteries";
	string shockSpikeDesc = "Send a charge from a distance! Aim and fire to charge batteries far away";
	string missileDesc = "Aim and fire to shoot a missile! Steer the missile to precisely hit your target";
    string homeMissileDesc = "Aim to lock onto enemies, and fire using the attack button.";
	string jetBoostDesc = "Allows you to boost yourself forward by pressing the dash button. Jump while dashing for an extra boost!";
    string vertBoostDesc = "Allows you to jump higher by pressing and holding the jump button.";
    string swordDesc = "Press the attack button to swing your sword. Time your swings to do a triple attack!";
    string repairDesc = "Use this item to repair broken robots found throughout the hub.";

    private void Start()
    {
        itemScreen = GameObject.Find("UI Canvas").GetComponentInChildren<GetItemScreen>();
    }

    public void Init()
    {
        if (isPickedUp)
        {
            if (reappear)
            {
                if (GetComponent<BoxCollider>() != null)
                {
                    if (GetComponent<MeshRenderer>() != null)
                    {
                        GetComponent<BoxCollider>().enabled = false;
                        GetComponent<MeshRenderer>().enabled = false;
                        Invoke("ReappearTrigger", reappearTimer);
                    }
                }
            }
            else
            {
                Destroy(this.gameObject);
            }

        }
    }
    void ReappearTrigger(){
        if(GetComponent<BoxCollider>() != null){
	        if(GetComponent<MeshRenderer>()!= null){
		        isPickedUp = false;
                SaveManager.Instance.SaveUpgrades();
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            if(whatUpgrade == upgrade.ShockProng){
                itemScreen.ShowItem("Shock Prong", 6, shockProngDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockShockProng();
                }
            }
            else if(whatUpgrade == upgrade.ShockSpike){
                itemScreen.ShowItem("Shock Spike", 7, shockSpikeDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockShockSpike();
                }
            }
            else if(whatUpgrade == upgrade.Missile){
                itemScreen.ShowItem("Missile", 4, missileDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockMissiles();
                }
                
            }
            else if(whatUpgrade == upgrade.HomingMissiles){
                itemScreen.ShowItem("Homing Missiles", 3, homeMissileDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockHomingMissiles();
                }
            }
            else if(whatUpgrade == upgrade.JetBooster){
                itemScreen.ShowItem("Jet Booster", 1, jetBoostDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockJetBoost();
                }
            }
            else if(whatUpgrade == upgrade.VerticalBooster){
                itemScreen.ShowItem("Vertical Booster", 9, vertBoostDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockVertBoost();
                }
            }
            else if(whatUpgrade == upgrade.Sword){
                itemScreen.ShowItem("Sword", 8, swordDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().UnlockSword();
                }
            }
            else if(whatUpgrade == upgrade.HeartPiece){
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetHeartUpgrade();
                    // Play audio when collected
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.upgradeCollectedSound, this.transform.position);
                }
            }
            else if(whatUpgrade == upgrade.ShieldPiece){
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetShieldUpgrade();
                }
            }
            else if(whatUpgrade == upgrade.BatteryPiece){
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
                    other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetBatteryUpgrade();
                }
            }
            else if(whatUpgrade == upgrade.RepairKit){
                itemScreen.ShowItem("RepairKit", 10, repairDesc);
                if ( other.transform.root.gameObject.GetComponent<UpgradeTracker>() != null ){
		            other.transform.root.gameObject.GetComponent<UpgradeTracker>().GetRepairKit();
	            }
            }
	        isPickedUp = true;
           SaveManager.Instance.SaveUpgrades();
            if (dissapear){
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
