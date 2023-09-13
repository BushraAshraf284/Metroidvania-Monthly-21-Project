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
	//public Weapon[] weapons;
	//public List<string> unlockedLeftWeapons = new List<string>();
	public List<string> unlockedLeftWeapon = new List<string>();
	//public List<string> unlockedRightWeapons = new List<string>();
	public List<string> unlockedRightWeapon = new List<string>();
	public int currentLeftWeaponIndex = 0; 
	public int currentRightWeaponIndex = 0; 
    GameObject shockProng, shockSpike, missiles, homingMissiles, sword, swordBlade;
    public enum LeftWeaponEquipped { Sword, ShockProng, none };
    public LeftWeaponEquipped leftWeapon;
    public enum RightWeaponEquipped { Missiles, HomingMissiles, ShockSpike, none };
    public RightWeaponEquipped rightWeapon;
    private void Awake()
	{
		currentLeftWeaponIndex = SaveData.Instance.currentLeftWeaponIndex;
		currentRightWeaponIndex = SaveData.Instance.currentRightWeaponIndex;
		totalLeftWeapons = SaveData.Instance.totalLeftWeapons;
		totalRightWeapons = SaveData.Instance.totalRightWeapons;

		unlockedLeftWeapon = SaveData.Instance.unlockedLeftWeapon;
		unlockedRightWeapon = SaveData.Instance.unlockedRightWeapon;
		rightWeapon = (RightWeaponEquipped)SaveData.Instance.RightWeaponEquipped;
		leftWeapon = (LeftWeaponEquipped)SaveData.Instance.LeftWeaponEquipped;
	    foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")){
		    if(g.GetComponent<UpgradeTracker>() != null){
			    upgrades = g.GetComponent<UpgradeTracker>();
		    }
		    if(g.GetComponent<abilities>() != null){
			    abil = g.GetComponent<abilities>();
		    }
	    }
	    shockProng = upgrades.shockProng;
	    shockSpike = upgrades.shockSpike;
	    missiles = upgrades.missiles;
	    homingMissiles = upgrades.homingMissiles;
	    sword = upgrades.sword;
	    swordBlade = upgrades.swordBlade;
	    
	    if (leftWeapon == LeftWeaponEquipped.Sword)
	    {
		    if (upgrades.hasSword)
		    {
		    	upgrades.UnlockSword();
			    //EquipSword();
		    }
		    else
		    {
			    DisableAllLeftHandWeapons();
		    }
	    }
	    else if (leftWeapon == LeftWeaponEquipped.ShockProng)
	    {
		    if (upgrades.hasShockProng)
		    {
			    //EquipShockProng();
			    upgrades.UnlockShockProng();
		    }
		    else
		    {
			    DisableAllLeftHandWeapons();
		    }
	    }
	    else if (leftWeapon == LeftWeaponEquipped.none)
	    {
		    DisableAllLeftHandWeapons();
	    }
	    if (rightWeapon == RightWeaponEquipped.Missiles)
	    {
		    if (upgrades.hasMissiles)
		    {
			    //EquipMissiles();
			    upgrades.UnlockMissiles();
		    }
		    else
		    {
			    DisableAllRightHandWeapons();
		    }
	    }
	    else if (rightWeapon == RightWeaponEquipped.HomingMissiles)
	    {
		    if (upgrades.hasHomingMissiles)
		    {
		    	upgrades.UnlockHomingMissiles();
			    //EquipHomingMissiles();
		    }
		    else
		    {
			    DisableAllRightHandWeapons();
		    }
	    }
	    else if (rightWeapon == RightWeaponEquipped.ShockSpike)
	    {
		    if (upgrades.hasShockSpike)
		    {
		    	upgrades.UnlockShockSpike();
			    //EquipShockSpike();
		    }
		    else
		    {
			    DisableAllRightHandWeapons();
		    }
	    }
	    else if (rightWeapon == RightWeaponEquipped.none)
	    {
		    DisableAllRightHandWeapons();
	    }
		foreach(string s in unlockedLeftWeapon){
			if(s == "Sword"){
				EquipSword();
			}
			if(s == "ShockProng"){
				EquipShockProng();
			}
		}
		foreach(string s in unlockedRightWeapon){
			if(s == "Missile"){
				EquipMissiles();
			}
			if(s == "HomingMissiles"){
				EquipHomingMissiles();
			}
			if(s == "ShockSpike"){
				EquipHomingMissiles();
			}
		}
    }

    void Start()
	{
		SaveData.Instance.unlockedRightWeapon = unlockedRightWeapon;
		SaveData.Instance.unlockedLeftWeapon = unlockedLeftWeapon;
		
		foreach(GameObject g in GameObject.FindGameObjectsWithTag("Player")){
			if(g.GetComponent<UpgradeTracker>() != null){
				upgrades = g.GetComponent<UpgradeTracker>();
			}
			if(g.GetComponent<abilities>() != null){
				abil = g.GetComponent<abilities>();
			}
		}
		shockProng = upgrades.shockProng;
		shockSpike = upgrades.shockSpike;
		missiles = upgrades.missiles;
		homingMissiles = upgrades.homingMissiles;
		sword = upgrades.sword;
		swordBlade = upgrades.swordBlade;
		
        if (leftWeapon == LeftWeaponEquipped.Sword)
        {
	        if (unlockedLeftWeapon.Contains("Sword"))
            {
                EquipSword();
            }
            else
            {
	            DisableAllLeftHandWeapons();
            }
        }
        else if (leftWeapon == LeftWeaponEquipped.ShockProng)
        {
	        if (unlockedLeftWeapon.Contains("ShockProng"))
            {
                EquipShockProng();
            }
            else
            {
                DisableAllLeftHandWeapons();
            }
        }
        else if (leftWeapon == LeftWeaponEquipped.none)
        {
            DisableAllLeftHandWeapons();
        }
        if (rightWeapon == RightWeaponEquipped.Missiles)
        {
	        if (unlockedRightWeapon.Contains("Missiles"))
            {
                EquipMissiles();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (rightWeapon == RightWeaponEquipped.HomingMissiles)
        {
	        if (unlockedRightWeapon.Contains("HomingMissiles"))
            {
                EquipHomingMissiles();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (rightWeapon == RightWeaponEquipped.ShockSpike)
        {
	        if (unlockedRightWeapon.Contains("ShockSpike"))
            {
                EquipShockSpike();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (rightWeapon == RightWeaponEquipped.none)
        {
            DisableAllRightHandWeapons();
        }
    }

    void DisableAllLeftHandWeapons()
    {
        swordBlade.SetActive(false);
        sword.SetActive(false);
        shockProng.SetActive(false);
        upgrades.hasSword = false;
	    upgrades.hasShockProng = false;
	    swordIcon.SetActive(false);
	    shockProngIcon.SetActive(false);
	    noneLIcon.SetActive(false);
	    leftWeapon = LeftWeaponEquipped.none;
		SaveData.Instance.LeftWeaponEquipped = (int)leftWeapon;
	    upgrades.DisableAllLeftHandWeapons();
    }
    void DisableAllRightHandWeapons()
	{
		//Debug.Log("Disabling all right hand weapons");
        shockSpike.SetActive(false);
        missiles.SetActive(false);
        homingMissiles.SetActive(false);
        upgrades.hasHomingMissiles = false;
        upgrades.hasShockSpike = false;
	    upgrades.hasMissiles = false;
	    shockSpikeIcon.SetActive(false);
	    missileLauncherIcon.SetActive(false);
	    homingMissileIcon.SetActive(false);
	    noneRIcon.SetActive(false);
	    rightWeapon = RightWeaponEquipped.none;
        SaveData.Instance.RightWeaponEquipped = (int)rightWeapon;
	    upgrades.DisableAllRightHandWeapons();
		abil.reloadingSpikeIcon.SetActive(false);
		abil.reloadingHomingMissileIcon.SetActive(false);
		abil.reloadingMissileIcon.SetActive(false);
    }

    public void EquipSword()
    {
        Debug.Log("Equipping Sword!");
        DisableAllLeftHandWeapons();
        sword.SetActive(true);
	    swordBlade.SetActive(true);
	    swordIcon.SetActive(true);
	    leftWeapon = LeftWeaponEquipped.Sword;
	    upgrades.hasSword = true;
        SaveData.Instance.HasSword = upgrades.hasSword;
        SaveData.Instance.LeftWeaponEquipped = (int)leftWeapon;
    }

	public void EquipShockProng()
   
    {
        Debug.Log("Equipping Shock Prong!");
        DisableAllLeftHandWeapons();
	    shockProng.SetActive(true);
	    shockProngIcon.SetActive(true);
	    leftWeapon = LeftWeaponEquipped.ShockProng;
	    upgrades.hasShockProng = true;
		SaveData.Instance.HasShockProng = upgrades.hasShockProng;
		SaveData.Instance.LeftWeaponEquipped = (int)leftWeapon;
    }
	public void EquipNoneLeft()
	{
		Debug.Log("Equipping None!");
		DisableAllLeftHandWeapons();
		noneLIcon.SetActive(true);
		leftWeapon = LeftWeaponEquipped.none;
        SaveData.Instance.LeftWeaponEquipped = (int)leftWeapon;

    }

    public void EquipShockSpike()
    {
        Debug.Log("Equipping Shock Spike!");
        DisableAllRightHandWeapons();
	    shockSpike.SetActive(true);
	    shockSpikeIcon.SetActive(true);
	    rightWeapon = RightWeaponEquipped.ShockSpike;
	    upgrades.hasShockSpike = true;       
    
		if(abil.spikeReloading){
			abil.reloadingSpikeIcon.SetActive(true);
			abil.reloadingHomingMissileIcon.SetActive(false);
			abil.reloadingMissileIcon.SetActive(false);
		}
		 SaveData.Instance.HasShockSpike = upgrades.hasShockSpike;
        SaveData.Instance.RightWeaponEquipped = (int)rightWeapon;
    }

    public void EquipMissiles()
        {
            Debug.Log("Equipping Missiles!");
            DisableAllRightHandWeapons();
	        missiles.SetActive(true);
	        missileLauncherIcon.SetActive(true);
	        rightWeapon = RightWeaponEquipped.Missiles;
	        upgrades.hasMissiles = true;
			if(abil.missileReloading){
				abil.reloadingSpikeIcon.SetActive(false);
				abil.reloadingHomingMissileIcon.SetActive(false);
				abil.reloadingMissileIcon.SetActive(true);
			}
			 SaveData.Instance.HasMissiles = upgrades.hasMissiles;
			SaveData.Instance.RightWeaponEquipped = (int)rightWeapon;
        }

    public void EquipHomingMissiles()
        {
            Debug.Log("Equipping Homing Missiles!");
            DisableAllRightHandWeapons();
	        homingMissiles.SetActive(true);
	        homingMissileIcon.SetActive(true);
	        rightWeapon = RightWeaponEquipped.HomingMissiles;
	        upgrades.hasHomingMissiles = true;
            SaveData.Instance.HasHomingMissiles = upgrades.hasHomingMissiles;
            SaveData.Instance.RightWeaponEquipped = (int)rightWeapon;
    
			if(abil.homingMissileReloading){
				abil.reloadingSpikeIcon.SetActive(false);
				abil.reloadingHomingMissileIcon.SetActive(true);
				abil.reloadingMissileIcon.SetActive(false);
			}
        }
		
	public void EquipNoneRight()
	{
		Debug.Log("Equipping None!");
		DisableAllRightHandWeapons();
		noneRIcon.SetActive(true);
		rightWeapon = RightWeaponEquipped.none;
        SaveData.Instance.RightWeaponEquipped = (int)rightWeapon;
		abil.reloadingSpikeIcon.SetActive(false);
		abil.reloadingHomingMissileIcon.SetActive(false);
		abil.reloadingMissileIcon.SetActive(false);
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

