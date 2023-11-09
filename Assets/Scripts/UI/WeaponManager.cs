using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System.Collections;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour
{
	abilities abil;
	[SerializeField]
	public GameObject swordIcon, shockProngIcon, noneLIcon, shockSpikeIcon, missileLauncherIcon, homingMissileIcon, noneRIcon;
	UpgradeTracker upgrades;
	public int totalLeftWeapons = 0, totalRightWeapons = 0;
	public List<string> unlockedLeftWeapon = new List<string>();
	public List<string> unlockedRightWeapon = new List<string>();
	public int currentLeftWeaponIndex = 0; 
	public int currentRightWeaponIndex = 0; 
	GameObject shockProngWM, shockSpikeWM, missilesWM, homingMissilesWM, swordWM, swordBladeWM;
	public enum LeftWeaponEquipped { none, Sword, ShockProng };
    public LeftWeaponEquipped leftWeapon;
	public enum RightWeaponEquipped { none, Missiles, HomingMissiles, ShockSpike };
	public RightWeaponEquipped rightWeapon;
	void LateAwake(){
		
	}
    private void Awake()
	{
		rightWeapon = (RightWeaponEquipped)SaveData.Instance.RightWeapon;
		leftWeapon = (LeftWeaponEquipped)SaveData.Instance.LeftWeapon;
		//Debug.Log("RIGHT WEAPON IS " + rightWeapon);
		//Debug.Log("LEFT WEAPON IS " + leftWeapon);
		//Debug.Log("FIRING AWAKE in WEPMAN!!!");
		currentLeftWeaponIndex = SaveData.Instance.currentLeftWeaponIndex;
		currentRightWeaponIndex = SaveData.Instance.currentRightWeaponIndex;
		totalLeftWeapons = SaveData.Instance.totalLeftWeapons;
		totalRightWeapons = SaveData.Instance.totalRightWeapons;
		unlockedLeftWeapon = SaveData.Instance.unlockedLeftWeapon;
		unlockedRightWeapon = SaveData.Instance.unlockedRightWeapon;
	    foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")){
		    if(g.GetComponent<UpgradeTracker>() != null){
			    upgrades = g.GetComponent<UpgradeTracker>();
		    }
		    if(g.GetComponent<abilities>() != null){
			    abil = g.GetComponent<abilities>();
		    }
	    }
		shockProngWM = upgrades.shockProngWM;
	    shockSpikeWM = upgrades.shockSpikeWM;
	    missilesWM = upgrades.missilesWM;
	    homingMissilesWM = upgrades.homingMissilesWM;
	    swordWM = upgrades.swordWM;
		swordBladeWM = upgrades.swordBladeWM;
	    if (leftWeapon == LeftWeaponEquipped.Sword)
	    {
	    	//Debug.Log("have sword equipped in save data, equipping");
	    	EquipSword();
	    }
	    else if (leftWeapon == LeftWeaponEquipped.ShockProng)
	    {
		    //Debug.Log("have shock prong equipped in save data, equipping");
		    EquipShockProng();
	    }
	    else if (leftWeapon == LeftWeaponEquipped.none)
	    {
		    DisableAllLeftHandWeapons();
		    if(totalLeftWeapons > 0){
		    	noneLIcon.SetActive(true);
		    }
		    else{
		    	noneLIcon.SetActive(false);
		    }
	    }
	    if (rightWeapon == RightWeaponEquipped.Missiles)
	    {
		    //Debug.Log("have missiles equipped in save data, equipping");
		    EquipMissiles();
	    }
	    else if (rightWeapon == RightWeaponEquipped.HomingMissiles)
	    {
	    	//Debug.Log("have homing missiles equipped in save data, equipping");
	    	EquipHomingMissiles();
	    }
	    else if (rightWeapon == RightWeaponEquipped.ShockSpike)
	    {
	    	//Debug.Log("have shock spike equipped in save data, equipping");
	    	EquipShockSpike();
	    }
	    else if (rightWeapon == RightWeaponEquipped.none)
	    {
		    DisableAllRightHandWeapons();
		    if(totalRightWeapons > 0){
		    	noneRIcon.SetActive(true);
		    }
		    else{
		    	noneRIcon.SetActive(false);
		    }
		    
	    }
	    
    }

	public void DisableAllLeftHandWeapons()
    {
        swordBladeWM.SetActive(false);
        swordWM.SetActive(false);
        shockProngWM.SetActive(false);
        upgrades.hasSword = false;
	    upgrades.hasShockProng = false;
	    swordIcon.SetActive(false);
	    shockProngIcon.SetActive(false);
	    noneLIcon.SetActive(false);
	    leftWeapon = LeftWeaponEquipped.none;
		SaveData.Instance.LeftWeapon = (int)leftWeapon;
	    SaveData.Instance.currentLeftWeaponIndex = currentLeftWeaponIndex;
    }
	public void DisableAllRightHandWeapons()
	{
		//Debug.Log("Disabling all right hand weapons");
        shockSpikeWM.SetActive(false);
        missilesWM.SetActive(false);
        homingMissilesWM.SetActive(false);
        upgrades.hasHomingMissiles = false;
        upgrades.hasShockSpike = false;
	    upgrades.hasMissiles = false;
	    shockSpikeIcon.SetActive(false);
	    missileLauncherIcon.SetActive(false);
	    homingMissileIcon.SetActive(false);
	    noneRIcon.SetActive(false);
	    rightWeapon = RightWeaponEquipped.none;
        SaveData.Instance.RightWeapon = (int)rightWeapon;
		abil.reloadingSpikeIcon.SetActive(false);
		abil.reloadingHomingMissileIcon.SetActive(false);
		abil.reloadingMissileIcon.SetActive(false);
    }

    public void EquipSword()
    {
	    //Debug.Log("Equipping Sword!");
        DisableAllLeftHandWeapons();
        swordWM.SetActive(true);
	    swordBladeWM.SetActive(true);
	    swordIcon.SetActive(true);
	    leftWeapon = LeftWeaponEquipped.Sword;
	    upgrades.hasSword = true;
	    SaveData.Instance.HasSword = upgrades.hasSword;
	    Debug.Log("old data: " + SaveData.Instance.LeftWeapon);
	    SaveData.Instance.LeftWeapon = (int)leftWeapon;
	    Debug.Log("new data: " + SaveData.Instance.LeftWeapon);
	    SaveData.Instance.currentLeftWeaponIndex = currentLeftWeaponIndex;
    }

	public void EquipShockProng()
   
    {
	    //Debug.Log("Equipping Shock Prong!");
        DisableAllLeftHandWeapons();
	    shockProngWM.SetActive(true);
	    shockProngIcon.SetActive(true);
	    leftWeapon = LeftWeaponEquipped.ShockProng;
	    upgrades.hasShockProng = true;
	    SaveData.Instance.HasShockProng = upgrades.hasShockProng;
	    Debug.Log("old data: " + SaveData.Instance.LeftWeapon);
	    SaveData.Instance.LeftWeapon = (int)leftWeapon;
	    Debug.Log("new data: " + SaveData.Instance.LeftWeapon);
	    SaveData.Instance.currentLeftWeaponIndex = currentLeftWeaponIndex;
    }
	public void EquipNoneLeft()
	{
		//Debug.Log("Equipping None!");
		DisableAllLeftHandWeapons();
		noneLIcon.SetActive(true);
		leftWeapon = LeftWeaponEquipped.none;
		Debug.Log("old data: " + SaveData.Instance.LeftWeapon);
		SaveData.Instance.LeftWeapon = (int)leftWeapon;
		Debug.Log("new data: " + SaveData.Instance.LeftWeapon);
		SaveData.Instance.currentLeftWeaponIndex = currentLeftWeaponIndex;

    }

    public void EquipShockSpike()
    {
	    //Debug.Log("Equipping Shock Spike!");
        DisableAllRightHandWeapons();
	    shockSpikeWM.SetActive(true);
	    shockSpikeIcon.SetActive(true);
	    rightWeapon = RightWeaponEquipped.ShockSpike;
	    upgrades.hasShockSpike = true;       
		if(abil.spikeReloading){
			abil.reloadingSpikeIcon.SetActive(true);
			abil.reloadingHomingMissileIcon.SetActive(false);
			abil.reloadingMissileIcon.SetActive(false);
		}
	    SaveData.Instance.HasShockSpike = upgrades.hasShockSpike;
	    Debug.Log("old data: " + SaveData.Instance.RightWeapon);
	    SaveData.Instance.RightWeapon = (int)rightWeapon;
	    Debug.Log("new data: " + SaveData.Instance.RightWeapon);
	    SaveData.Instance.currentRightWeaponIndex = currentRightWeaponIndex;
    }

    public void EquipMissiles()
        {
	        //Debug.Log("Equipping Missiles!");
            DisableAllRightHandWeapons();
	        missilesWM.SetActive(true);
	        missileLauncherIcon.SetActive(true);
	        rightWeapon = RightWeaponEquipped.Missiles;
	        upgrades.hasMissiles = true;
			if(abil.missileReloading){
				abil.reloadingSpikeIcon.SetActive(false);
				abil.reloadingHomingMissileIcon.SetActive(false);
				abil.reloadingMissileIcon.SetActive(true);
			}
	        SaveData.Instance.HasMissiles = upgrades.hasMissiles;
	        Debug.Log("old data: " + SaveData.Instance.RightWeapon);
	        SaveData.Instance.RightWeapon = (int)rightWeapon;
	        Debug.Log("new data: " + SaveData.Instance.RightWeapon);
	        SaveData.Instance.currentRightWeaponIndex = currentRightWeaponIndex;
        }

    public void EquipHomingMissiles()
        {
	        //Debug.Log("Equipping Homing Missiles!");
            DisableAllRightHandWeapons();
	        homingMissilesWM.SetActive(true);
	        homingMissileIcon.SetActive(true);
	        rightWeapon = RightWeaponEquipped.HomingMissiles;
	        upgrades.hasHomingMissiles = true;
	        SaveData.Instance.HasHomingMissiles = upgrades.hasHomingMissiles;
	        Debug.Log("old data: " + SaveData.Instance.RightWeapon);
	        SaveData.Instance.RightWeapon = (int)rightWeapon;
	        Debug.Log("new data: " + SaveData.Instance.RightWeapon);
	        SaveData.Instance.currentRightWeaponIndex = currentRightWeaponIndex;
			if(abil.homingMissileReloading){
				abil.reloadingSpikeIcon.SetActive(false);
				abil.reloadingHomingMissileIcon.SetActive(true);
				abil.reloadingMissileIcon.SetActive(false);
			}
        }
		
	public void EquipNoneRight()
	{
		//Debug.Log("Equipping None!");
		DisableAllRightHandWeapons();
		noneRIcon.SetActive(true);
		rightWeapon = RightWeaponEquipped.none;
		Debug.Log("old data: " + SaveData.Instance.RightWeapon);
		SaveData.Instance.RightWeapon = (int)rightWeapon;
		Debug.Log("new data: " + SaveData.Instance.RightWeapon);
		abil.reloadingSpikeIcon.SetActive(false);
		abil.reloadingHomingMissileIcon.SetActive(false);
		abil.reloadingMissileIcon.SetActive(false);
		SaveData.Instance.currentRightWeaponIndex = currentRightWeaponIndex;
	}

	public void SelectNextWeaponR()
    {
        currentRightWeaponIndex++;
	    if (currentRightWeaponIndex > totalRightWeapons)
        {
            currentRightWeaponIndex = 0;
        }
	    if(unlockedRightWeapon[currentRightWeaponIndex] == "Missiles"){
	    	DisableAllRightHandWeapons();
	    	EquipMissiles();
	    }
	    if(unlockedRightWeapon[currentRightWeaponIndex] == "HomingMissiles"){
		    DisableAllRightHandWeapons();
	    	EquipHomingMissiles();
	    }
	    if(unlockedRightWeapon[currentRightWeaponIndex] == "ShockSpike"){
		    DisableAllRightHandWeapons();
	    	EquipShockSpike();
	    }
	    if(unlockedRightWeapon[currentRightWeaponIndex] == "None"){
		    DisableAllRightHandWeapons();
	    	EquipNoneRight();
	    }

    }

	public void SelectNextWeaponL()
	{
		currentLeftWeaponIndex++;
		if (currentLeftWeaponIndex > totalLeftWeapons)
		{
			currentLeftWeaponIndex = 0;
		}
		if(unlockedLeftWeapon[currentLeftWeaponIndex] == "Sword"){
			DisableAllLeftHandWeapons();
			EquipSword();
		}
		if(unlockedLeftWeapon[currentLeftWeaponIndex] == "ShockProng"){
			DisableAllLeftHandWeapons();
			EquipShockProng();
		}
		if(unlockedLeftWeapon[currentLeftWeaponIndex] == "None"){
			DisableAllLeftHandWeapons();
			EquipNoneLeft();
		}

	}

	
}

