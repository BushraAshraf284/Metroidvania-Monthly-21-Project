using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using static Upgrade;

public class UpgradeTracker : MonoBehaviour
{
	public WeaponManager wepMan;
    [SerializeField]
	public GameObject shockProng, shockSpike, missiles, homingMissiles, jetBoost, vertBoost, sword, swordBlade, shield, hp1, hp11, hp2, bat1, bat2;
    public bool hasShockProng, hasShockSpike, hasMissiles, hasHomingMissiles, hasJetBoost, hasVertBoost, hasSword;
    public int heartPieceCount;
    public int shieldPieceCount;
    public int batteryPieceCount;
	PlayerStats stats;
	enum LeftWeaponEquipped{Sword, ShockProng, none};
	LeftWeaponEquipped leftWeapon;
	enum RightWeaponEquipped{Missiles, HomingMissiles, ShockSpike, none};
	RightWeaponEquipped rightWeapon;

    [SerializeField]
    GetItemScreen itemScreen;
    [SerializeField]
    UpgradeMenu upgradeMenu;

	string heartDesc = "You have increased your maximum health points!";
	string batteryDesc = "You have increased your maximum battery charge!";
	string shieldDesc = "You can now fully block one attack";
	
	void LateAwake(){
		foreach (string s in wepMan.unlockedLeftWeapon){
			Debug.Log(s);
			if(s == "Sword"){
				//Debug.Log("AAAAAAAAAAAAAAAAAAAUnlocking Sword!");
				upgradeMenu.ShowUpgrade("Sword");
			}
			if(s == "ShockProng"){
				//Debug.Log("AAAAAAAAAAAAAAAAAAAUnlocking Shock prong!");
				upgradeMenu.ShowUpgrade("Shock Prong");
			}
		}
		foreach (string s in wepMan.unlockedRightWeapon){
			Debug.Log(s);
			if(s == "Missiles"){
				//Debug.Log("AAAAAAAAAAAAAAAAAAAUnlocking Missiles!");
				upgradeMenu.ShowUpgrade("Missiles");
			}
			if(s== "HomingMissiles"){
				//Debug.Log("AAAAAAAAAAAAAAAAAAAUnlocking Homing Missiles!");
				upgradeMenu.ShowUpgrade("Homing Missiles");
			}
			if(s == "ShockSpike"){
				//Debug.Log("AAAAAAAAAAAAAAAAAAAUnlocking Shock Spike!");
				upgradeMenu.ShowUpgrade("Shock Spike");
			}
		}
	}

    private void Awake()
	{
		//Debug.LogError("UpgradeTrackerAwake");
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

        if (!SaveData.Instance.unlockedLeftWeapon.Contains("None")){
		    SaveData.Instance.unlockedLeftWeapon.Add("None");
	    }
	    if(!SaveData.Instance.unlockedRightWeapon.Contains("None")){
		    SaveData.Instance.unlockedRightWeapon.Add("None");
	    }
	    
	    if(heartPieceCount == 1){
	    	upgradeMenu.ShowHeartUpgrade();
	    }
	    if(heartPieceCount == 2){
	    	upgradeMenu.ShowHeartUpgradeTwo();
	    }
	    if(heartPieceCount == 3){
	    	upgradeMenu.UpdateHeartUpgrade();
		    hp1.SetActive(true);
	    	hp11.SetActive(true);
	    	hp2.SetActive(true);
	    }
	    if(shieldPieceCount == 1){
	    	upgradeMenu.ShowShieldUpgrade();
	    }
	    if(shieldPieceCount == 2){
	    	upgradeMenu.ShowShieldUpgradeTwo();
	    }
	    if(shieldPieceCount == 3){
	    	upgradeMenu.UpdateShieldUpgrade();
	    	shield.SetActive(true);
	    }
	    if(batteryPieceCount == 1){
	    	upgradeMenu.ShowBatteryUpgrade();
	    }
	    if(batteryPieceCount == 2){
	    	upgradeMenu.ShowBatteryUpgradeTwo();
	    }
	    if(batteryPieceCount == 3){
	    	upgradeMenu.UpdateBatteryUpgrade();
		    bat1.SetActive(true);
		    bat2.SetActive(true);
	    } 
	    if(hasJetBoost){
	    	upgradeMenu.ShowUpgrade("Jet Booster");
	    	jetBoost.SetActive(true);
	    }
	    if(hasVertBoost){
	    	upgradeMenu.ShowUpgrade("Vertical Booster");
	    	vertBoost.SetActive(true);
	    }
		Invoke("LateAwake", .1f);
		
    }
	public int repairKitCount;
	[SerializeField]
	public bool enteredWorld1, enteredWorld2, enteredBossArea;
    void Start()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Managers")){
			if(g.GetComponent<WeaponManager>() != null){
				wepMan = g.GetComponent<WeaponManager>();
			}
		}
	    stats = GetComponent<PlayerStats>();
	    
    }
	// Start is called before the first frame update
	public void GetRepairKit(){
		repairKitCount++;
		SaveData.Instance.RepairKitCount++;
	}
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
                shield.SetActive(true);
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
                hp1.SetActive(true);
                hp11.SetActive(true);
                hp2.SetActive(true);
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
                bat1.SetActive(true);
                bat2.SetActive(true);
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
	public void DisableAllLeftHandWeapons()
    {
        swordBlade.SetActive(false);
        sword.SetActive(false);
        shockProng.SetActive(false);
        hasSword = false;
	    hasShockProng = false;
        SaveData.Instance.HasSword = hasSword;
        SaveData.Instance.HasShockProng = hasShockProng;
        wepMan.swordIcon.SetActive(false);
	    wepMan.shockProngIcon.SetActive(false);
	    wepMan.noneLIcon.SetActive(false);
    }
	public void DisableAllRightHandWeapons()
    {
        shockSpike.SetActive(false);
        missiles.SetActive(false);
        homingMissiles.SetActive(false);
        hasHomingMissiles = false;
        hasShockSpike = false;
	    hasMissiles = false;

        SaveData.Instance.HasShockSpike = hasShockSpike;
        SaveData.Instance.HasHomingMissiles = hasHomingMissiles;
        SaveData.Instance.HasMissiles = hasMissiles;
     

        wepMan.missileLauncherIcon.SetActive(false);
	    wepMan.homingMissileIcon.SetActive(false);
	    wepMan.shockSpikeIcon.SetActive(false);
	    wepMan.noneRIcon.SetActive(false);
	   
    }
    public void UnlockSword()
    {
        Debug.Log("Equipping Sword!");
        DisableAllLeftHandWeapons();
        upgradeMenu.ShowUpgrade("Sword");
        sword.SetActive(true); ;
        swordBlade.SetActive(true);
	    hasSword = true;
        SaveData.Instance.HasSword = hasSword;
        wepMan.EquipSword();
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
	    
	    
	    if(!wepMan.unlockedLeftWeapon.Contains("Sword")){
	    	wepMan.unlockedLeftWeapon.Add("Sword");
	    }
	    SaveData.Instance.unlockedRightWeapon = wepMan.unlockedRightWeapon;
	    SaveData.Instance.unlockedLeftWeapon = wepMan.unlockedLeftWeapon;
	    
    }
    public void EquipSword()
    {
        Debug.Log("Equipping Sword!");
        DisableAllLeftHandWeapons();
        sword.SetActive(true);
        swordBlade.SetActive(true);
    }
    public void UnlockShockProng()
    {
        Debug.Log("Equipping Shock Prong!");
        upgradeMenu.ShowUpgrade("Shock Prong");
        DisableAllLeftHandWeapons();
        shockProng.SetActive(true);
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
    public void EquipShockProng()
    {
        Debug.Log("Equipping Shock Prong!");
        DisableAllLeftHandWeapons();
        shockProng.SetActive(true);
    }
    public void UnlockShockSpike()
    {
        Debug.Log("Equipping Shock Spike!");
        DisableAllRightHandWeapons();
        upgradeMenu.ShowUpgrade("Shock Spike");
        shockSpike.SetActive(true);
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
    public void EquipShockSpike()
    {
        Debug.Log("Equipping Shock Spike!");
        DisableAllRightHandWeapons();
        shockSpike.SetActive(true);
    }
    public void UnlockMissiles()
    {
        Debug.Log("Equipping Missiles!");
        DisableAllRightHandWeapons();
        upgradeMenu.ShowUpgrade("Missiles");
        missiles.SetActive(true);
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
    public void EquipMissiles()
    {
        Debug.Log("Equipping Missiles!");
        DisableAllRightHandWeapons();
        missiles.SetActive(true);
    }
    public void UnlockHomingMissiles()
    {
        Debug.Log("Unlocking Homing Missiles!");
        upgradeMenu.ShowUpgrade("Homing Missiles");
        DisableAllRightHandWeapons();
        homingMissiles.SetActive(true);
	    hasHomingMissiles = true;

        SaveData.Instance.HasHomingMissiles= homingMissiles;

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
    public void EquipHomingMissiles()
    {
        Debug.Log("Equipping Homing Missiles!");
        DisableAllRightHandWeapons();
	    homingMissiles.SetActive(true);
        
    }
    public void UnlockJetBoost()
    {
        Debug.Log("Equipping Jet Boost!");
        jetBoost.SetActive(true);
        upgradeMenu.ShowUpgrade("Jet Booster");
        hasJetBoost = true;
        SaveData.Instance.HasJetBoost = hasJetBoost;
    }
    public void UnlockVertBoost()
    {
        Debug.Log("Equipping Vertical Boost!");
        vertBoost.SetActive(true);
        upgradeMenu.ShowUpgrade("Vertical Booster");
        hasVertBoost = true;
        SaveData.Instance.HasVertBoost = hasVertBoost;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
