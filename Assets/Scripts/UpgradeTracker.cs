using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static Upgrade;

//This script handles unlocking new abilities and upgrades, as well as enabling their world models and UI elements

public class UpgradeTracker : MonoBehaviour
{
	//reference to weapon manager
	public WeaponManager wepMan;
	//References to all the world models (WM)
    [SerializeField]
	public GameObject shockProngWM, shockSpikeWM, missilesWM, homingMissilesWM, jetBoostWM, vertBoostWM, swordWM, swordBladeWM, shieldWM, hp1WM, hp11WM, hp2WM, bat1WM, bat2WM;
	//Bools that keep track of what weapon you currently have equipped
	public bool hasShockProng, hasShockSpike, hasMissiles, hasHomingMissiles, hasJetBoost, hasVertBoost, hasSword;
	//tracks amount of each type of upgrade piece
    public int heartPieceCount;
    public int shieldPieceCount;
	public int batteryPieceCount;
	public int repairKitCount;
	public int iCChipCount;
	//reference to playerstats
	PlayerStats stats;
	//Enums that track which weapon is currently equipped in which hand. 
	enum LeftWeaponEquipped{Sword, ShockProng, none};
	LeftWeaponEquipped leftWeapon;
	enum RightWeaponEquipped{Missiles, HomingMissiles, ShockSpike, none};
	RightWeaponEquipped rightWeapon;
	//reference to Ui element that pops up to tell you what you unlocked
    [SerializeField]
	GetItemScreen itemScreen;
	//reference to UI element tracking which upgrades you have unlocked
    [SerializeField]
    UpgradeMenu upgradeMenu;
	//Descriptions for each of the 3 piece upgrades
	string heartDesc = "You have increased your maximum health points!";
	string batteryDesc = "You have increased your maximum battery charge!";
	string shieldDesc = "You can now fully block one attack";
	//bools that track which worlds you have entered so that the NPC's can respond accordingly
	[SerializeField]
	public bool enteredWorld1, enteredWorld2, enteredBossArea;
	
    private void Awake()
	{
		//Load all data from the .json file
        heartPieceCount = SaveData.Instance.HeartPieceCount;
        shieldPieceCount = SaveData.Instance.ShieldPieceCount;
        batteryPieceCount = SaveData.Instance.BatteryPieceCount;
        hasShockProng = SaveData.Instance.HasShockProng;
        hasMissiles = SaveData.Instance.HasMissiles;
        hasSword = SaveData.Instance.HasSword;
        hasHomingMissiles = SaveData.Instance.HasHomingMissiles;
        hasJetBoost = SaveData.Instance.HasJetBoost;
        hasVertBoost = SaveData.Instance.HasVertBoost;
	    hasShockSpike = SaveData.Instance.HasShockSpike;
        enteredWorld1 = SaveData.Instance.EnteredWorld1;
		enteredWorld2 = SaveData.Instance.EnteredWorld2;
		repairKitCount = SaveData.Instance.RepairKitCount;
		iCChipCount = SaveData.Instance.ICChipCount;
		
		//Make sure the "None" weapon exists for each hand if it doesnt
        if (!SaveData.Instance.unlockedLeftWeapon.Contains("None")){
		    SaveData.Instance.unlockedLeftWeapon.Add("None");
	    }
	    if(!SaveData.Instance.unlockedRightWeapon.Contains("None")){
		    SaveData.Instance.unlockedRightWeapon.Add("None");
	    }
		//update the upgrade UI if you have loaded data saying you have these upgrades unlocked already
		//the upgrade Ui is the menu that shows up on your pause menu showing what upgrades you currently have unlocked. 
	    if(heartPieceCount == 1){
	    	upgradeMenu.ShowHeartUpgrade();
	    }
	    if(heartPieceCount == 2){
	    	upgradeMenu.ShowHeartUpgradeTwo();
	    }
	    if(heartPieceCount == 3){
	    	upgradeMenu.UpdateHeartUpgrade();
	    	//enable world models if you have collected all 3 pieces already 
		    hp1WM.SetActive(true);
	    	hp11WM.SetActive(true);
	    	hp2WM.SetActive(true);
	    }
	    if(shieldPieceCount == 1){
	    	upgradeMenu.ShowShieldUpgrade();
	    }
	    if(shieldPieceCount == 2){
	    	upgradeMenu.ShowShieldUpgradeTwo();
	    }
	    if(shieldPieceCount == 3){
	    	upgradeMenu.UpdateShieldUpgrade();
	    	//enable world models if you have collected all 3 pieces already 
	    	shieldWM.SetActive(true);
	    }
	    if(batteryPieceCount == 1){
	    	upgradeMenu.ShowBatteryUpgrade();
	    }
	    if(batteryPieceCount == 2){
	    	upgradeMenu.ShowBatteryUpgradeTwo();
	    }
	    if(batteryPieceCount == 3){
	    	upgradeMenu.UpdateBatteryUpgrade();
	    	//enable world models if you have collected all 3 pieces already 
		    bat1WM.SetActive(true);
		    bat2WM.SetActive(true);
	    } 
	    if(hasJetBoost){
	    	upgradeMenu.ShowUpgrade("Jet Booster");
	    	jetBoostWM.SetActive(true);
	    }
	    if(hasVertBoost){
	    	upgradeMenu.ShowUpgrade("Vertical Booster");
	    	vertBoostWM.SetActive(true);
	    }
		if (iCChipCount > 0) {
			upgradeMenu.ShowICChip();
		}
		
		Invoke("LateAwake", .1f);
		
	}
    
	//This is fires slightly after the Awake Method
	void LateAwake(){
		//ensure the upgrade UI accurately tracks which weapons you have unlocked
		//we do not need to enable the world models here since the equip methods already to this. 
		foreach (string s in wepMan.unlockedLeftWeapon){
			if(s == "Sword"){
				//Debug.Log("Unlocking Sword!");
				upgradeMenu.ShowUpgrade("Sword");
			}
			if(s == "ShockProng"){
				//Debug.Log("Unlocking Shock prong!");
				upgradeMenu.ShowUpgrade("Shock Prong");
			}
		}
		foreach (string s in wepMan.unlockedRightWeapon){
			if(s == "Missiles"){
				//Debug.Log("Unlocking Missiles!");
				upgradeMenu.ShowUpgrade("Missiles");
			}
			if(s== "HomingMissiles"){
				//Debug.Log("Unlocking Homing Missiles!");
				upgradeMenu.ShowUpgrade("Homing Missiles");
			}
			if(s == "ShockSpike"){
				//Debug.Log("Unlocking Shock Spike!");
				upgradeMenu.ShowUpgrade("Shock Spike");
			}
		}
	}
	//Runs after awake, simply gets a few references
    void Start()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Managers")){
			if(g.GetComponent<WeaponManager>() != null){
				wepMan = g.GetComponent<WeaponManager>();
			}
		}
	    stats = GetComponent<PlayerStats>();
	    
	}
	//called when you pick up a repair kit, we do not need any deeped logic here since infinite repair kits can be held and nothing changes
	public void GetRepairKit(){
		repairKitCount++;
		SaveData.Instance.RepairKitCount++;
	}
	public void GetICChip()
	{
		iCChipCount++;
		SaveData.Instance.ICChipCount++;
		upgradeMenu.ShowICChip();
	}
	//called when you pick up a shield upgrade, checks if you are over the threshold of 3 and equips the new upgrade if so. 
	public void GetShieldUpgrade()
    {
        if (shieldPieceCount < 3)
        {
            if (shieldPieceCount + 1 >= 3)
            {
                Debug.Log("Got All Three Pieces! Giving shield Upgrade");
                itemScreen.ShowItem("ShieldPiece", 5, shieldDesc);
                stats.GetShieldUpgrade();
	            shieldPieceCount = 3;
                shieldWM.SetActive(true);
                upgradeMenu.UpdateShieldUpgrade();
            }
            else
            {
                Debug.Log("Got a shield Piece!");
                shieldPieceCount += 1;
                if (shieldPieceCount == 1)
                {
                    upgradeMenu.ShowShieldUpgrade();
                }

                if (shieldPieceCount == 2)
                {
                    upgradeMenu.ShowShieldUpgradeTwo();
                }
            }
            SaveData.Instance.ShieldPieceCount = shieldPieceCount;
        }
    }
	//called when you pick up a heart upgrade, checks if you are over the threshold of 3 and equips the new upgrade if so. 
    public void GetHeartUpgrade()
    {
        if (heartPieceCount < 3)
        {
            if (heartPieceCount + 1 >= 3)
            {
                Debug.Log("Got All Three Pieces! Giving Health Upgrade");
                itemScreen.ShowItem("HeartPiece", 2, heartDesc);
                stats.GetHPUpgrade();
                stats.hp = stats.MaxHP;
	            heartPieceCount = 3;
                hp1WM.SetActive(true);
                hp11WM.SetActive(true);
                hp2WM.SetActive(true);
                upgradeMenu.UpdateHeartUpgrade();
                SaveData.Instance.playerHP = stats.hp;
            }
            else
            {
                Debug.Log("Got A Heart Piece");
                heartPieceCount += 1;
                if (heartPieceCount == 1)
                {
                    upgradeMenu.ShowHeartUpgrade();
                }

                if (heartPieceCount == 2)
                {
                    upgradeMenu.ShowHeartUpgradeTwo();
                }
            }
            SaveData.Instance.HeartPieceCount = heartPieceCount;
        }
    }
	//called when you pick up a battery upgrade, checks if you are over the threshold of 3 and equips the new upgrade if so. 
    public void GetBatteryUpgrade()
    {
        if (batteryPieceCount < 3)
        {
            if (batteryPieceCount + 1 >= 3)
            {
                Debug.Log("Got All Three Pieces! Giving Battery Upgrade");
                itemScreen.ShowItem("BatteryPiece", 0, batteryDesc);
                stats.GetBatteryUpgrade();
	            batteryPieceCount = 3;
                bat1WM.SetActive(true);
                bat2WM.SetActive(true);
                upgradeMenu.UpdateBatteryUpgrade();
            }
            else
            {
                Debug.Log("Got A Battery Piece");
                batteryPieceCount += 1;
                if (batteryPieceCount == 1)
                {
                    upgradeMenu.ShowBatteryUpgrade();
                }

                if (batteryPieceCount == 2)
                {
                    upgradeMenu.ShowBatteryUpgradeTwo();
                }
            }
            SaveData.Instance.BatteryPieceCount = batteryPieceCount;
        }
    }
	//call the Unlock methods when you initially pick up an item for the first time
	//This is roughly the same for each unlock method, so i will only comment this one
    public void UnlockSword()
	{
		//Debug.Log("Unlocking Sword!");
		// Unequip All Weapons in left hand
		wepMan.DisableAllLeftHandWeapons();
	    // Pause Menu HUD
		upgradeMenu.ShowUpgrade("Sword");
		// bool keeps track of what weapons you have equipped 
		hasSword = true;
		// update .json save file
		SaveData.Instance.HasSword = hasSword;
		// update world model 
		wepMan.EquipSword();
		// update count of currently unlocked weapons
		wepMan.totalLeftWeapons++;
		// Update .json save file
		SaveData.Instance.totalLeftWeapons = wepMan.totalLeftWeapons;
		// try to iterate the current left weapon index (still kinda unclear on this)
		// check if you rolled over the index when you unlocked this weapon, if so reset
	    if(wepMan.currentLeftWeaponIndex+1 > wepMan.totalLeftWeapons){
	    	wepMan.currentLeftWeaponIndex = 0;
	    	SaveData.Instance.currentLeftWeaponIndex = wepMan.currentLeftWeaponIndex;
	    }
		// you didnt roll over, just iterate the value
	    else{
	    	wepMan.currentLeftWeaponIndex++;
	    	SaveData.Instance.currentLeftWeaponIndex = wepMan.currentLeftWeaponIndex;
	    }
		// if you somehow already have sword unlocked, skip this
	    if(!wepMan.unlockedLeftWeapon.Contains("Sword")){
	    	wepMan.unlockedLeftWeapon.Add("Sword");
	    }
		// update .json
	    SaveData.Instance.unlockedRightWeapon = wepMan.unlockedRightWeapon;
	    SaveData.Instance.unlockedLeftWeapon = wepMan.unlockedLeftWeapon; 
	}
    public void UnlockShockProng()
    {
	    //Debug.Log("Unlocking Shock Prong!");
        upgradeMenu.ShowUpgrade("Shock Prong");
	    wepMan.DisableAllLeftHandWeapons();
	    hasShockProng = true;
        SaveData.Instance.HasShockProng = hasShockProng;
        wepMan.EquipShockProng();
	    wepMan.totalLeftWeapons++;
	    SaveData.Instance.totalLeftWeapons = wepMan.totalLeftWeapons;
	    if(wepMan.currentLeftWeaponIndex+1 > wepMan.totalLeftWeapons){
	    	wepMan.currentLeftWeaponIndex = 0;
	    	SaveData.Instance.currentLeftWeaponIndex = wepMan.currentLeftWeaponIndex;
	    }
	    else{
	    	wepMan.currentLeftWeaponIndex++;
	    	SaveData.Instance.currentLeftWeaponIndex = wepMan.currentLeftWeaponIndex;
	    }
	    if(!wepMan.unlockedLeftWeapon.Contains("ShockProng")){
	    	wepMan.unlockedLeftWeapon.Add("ShockProng");
	    }
	    SaveData.Instance.unlockedRightWeapon = wepMan.unlockedRightWeapon;
	    SaveData.Instance.unlockedLeftWeapon = wepMan.unlockedLeftWeapon;
    }
    public void UnlockShockSpike()
    {
	    //Debug.Log("Unlocking Shock Spike!");
	    wepMan.DisableAllRightHandWeapons();
        upgradeMenu.ShowUpgrade("Shock Spike");
	    hasShockSpike = true;
        SaveData.Instance.HasShockSpike = hasShockSpike;
	    wepMan.totalRightWeapons++;
	    SaveData.Instance.totalRightWeapons = wepMan.totalRightWeapons;
	    wepMan.EquipShockSpike();
	    if(wepMan.currentRightWeaponIndex+1 > wepMan.totalRightWeapons){
	    	wepMan.currentRightWeaponIndex = 0;
	    	SaveData.Instance.currentRightWeaponIndex = wepMan.currentRightWeaponIndex;
	    }
	    else{
	    	wepMan.currentRightWeaponIndex++;
	    	SaveData.Instance.currentRightWeaponIndex = wepMan.currentRightWeaponIndex;
	    }
	    if(!wepMan.unlockedRightWeapon.Contains("ShockSpike")){
	    	wepMan.unlockedRightWeapon.Add("ShockSpike");
	    }
	    SaveData.Instance.unlockedRightWeapon = wepMan.unlockedRightWeapon;
	    SaveData.Instance.unlockedLeftWeapon = wepMan.unlockedLeftWeapon;
    }
    public void UnlockMissiles()
    {
	    //Debug.Log("Unlocking Missiles!");
	    wepMan.DisableAllRightHandWeapons();
        upgradeMenu.ShowUpgrade("Missiles");
	    hasMissiles = true;
        SaveData.Instance.HasMissiles = hasMissiles;
	    wepMan.totalRightWeapons++;
	    SaveData.Instance.totalRightWeapons = wepMan.totalRightWeapons;
	    wepMan.EquipMissiles();
	    if(wepMan.currentRightWeaponIndex+1 > wepMan.totalRightWeapons){
	    	wepMan.currentRightWeaponIndex = 0;
	    	SaveData.Instance.currentRightWeaponIndex = wepMan.currentRightWeaponIndex;
	    }
	    else{
	    	wepMan.currentRightWeaponIndex++;
	    	SaveData.Instance.currentRightWeaponIndex = wepMan.currentRightWeaponIndex;
	    }
	    if(!wepMan.unlockedRightWeapon.Contains("Missiles")){
	    	wepMan.unlockedRightWeapon.Add("Missiles");
	    }
	    SaveData.Instance.unlockedRightWeapon = wepMan.unlockedRightWeapon;
	    SaveData.Instance.unlockedLeftWeapon = wepMan.unlockedLeftWeapon;
    }
    public void UnlockHomingMissiles()
    {
	    //Debug.Log("Unlocking Homing Missiles!");
        upgradeMenu.ShowUpgrade("Homing Missiles");
	    wepMan.DisableAllRightHandWeapons();
	    hasHomingMissiles = true;
	    SaveData.Instance.HasHomingMissiles= hasHomingMissiles;
	    wepMan.totalRightWeapons++;
	    SaveData.Instance.totalRightWeapons = wepMan.totalRightWeapons;
	    wepMan.EquipHomingMissiles();
	    if(wepMan.currentRightWeaponIndex+1 > wepMan.totalRightWeapons){
	    	wepMan.currentRightWeaponIndex = 0;
	    	SaveData.Instance.currentRightWeaponIndex = wepMan.currentRightWeaponIndex;
	    }
	    else{
	    	wepMan.currentRightWeaponIndex++;
	    	SaveData.Instance.currentRightWeaponIndex = wepMan.currentRightWeaponIndex;
	    }
	    if(!wepMan.unlockedRightWeapon.Contains("HomingMissiles")){
	    	wepMan.unlockedRightWeapon.Add("HomingMissiles");
	    }
	    SaveData.Instance.unlockedRightWeapon = wepMan.unlockedRightWeapon;
	    SaveData.Instance.unlockedLeftWeapon = wepMan.unlockedLeftWeapon;
    }
    public void UnlockJetBoost()
    {
	    // Debug.Log("Equipping Jet Boost!");
        jetBoostWM.SetActive(true);
        upgradeMenu.ShowUpgrade("Jet Booster");
        hasJetBoost = true;
        SaveData.Instance.HasJetBoost = hasJetBoost;
    }
    public void UnlockVertBoost()
    {
	    //Debug.Log("Equipping Vertical Boost!");
        vertBoostWM.SetActive(true);
        upgradeMenu.ShowUpgrade("Vertical Booster");
        hasVertBoost = true;
        SaveData.Instance.HasVertBoost = hasVertBoost;
    }

}
