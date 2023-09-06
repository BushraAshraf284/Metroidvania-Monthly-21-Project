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
	public enum LeftWeaponEquipped{Sword, ShockProng, none};
	public LeftWeaponEquipped leftWeapon;
	public enum RightWeaponEquipped{Missiles, HomingMissiles, ShockSpike, none};
	public RightWeaponEquipped rightWeapon;

    private void Awake()
    {
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
    }
    void Start()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Managers")){
			if(g.GetComponent<WeaponManager>() != null){
				wepMan = g.GetComponent<WeaponManager>();
			}
		}
	    stats = GetComponent<PlayerStats>();
	    if(leftWeapon == LeftWeaponEquipped.Sword){
	    	if(hasSword){
		    	EquipSword();
	    	}
	    	else{
	    		DisableAllRightHandWeapons();
	    	}
	    }
	    else if(leftWeapon == LeftWeaponEquipped.ShockProng){
	    	if(hasShockProng){
		    	EquipShockProng();
	    	}
	    	else{
	    		DisableAllRightHandWeapons();
	    	}
	    }
	    else if(leftWeapon == LeftWeaponEquipped.none){
	    	DisableAllLeftHandWeapons();
	    }
	    if(rightWeapon == RightWeaponEquipped.Missiles){
		    if(hasMissiles){
		    	EquipMissiles();
	    	}
		    else{
	    		DisableAllRightHandWeapons();
	    	}
	    }
	    else if(rightWeapon == RightWeaponEquipped.HomingMissiles){
		    if(hasHomingMissiles){
		    	EquipHomingMissiles();
	    	}
		    else{
	    		DisableAllRightHandWeapons();
	    	}
	    }
	    else if(rightWeapon == RightWeaponEquipped.ShockSpike){
		    if(hasShockSpike){
		    	EquipShockSpike();
	    	}
	    	else{
	    		DisableAllRightHandWeapons();
	    	}
	    }
	    else if(rightWeapon == RightWeaponEquipped.none){
	    	DisableAllRightHandWeapons();
	    }
    }
    // Start is called before the first frame update
    public void GetShieldUpgrade()
    {
        if (shieldPieceCount < 3)
        {
            if (shieldPieceCount + 1 >= 3)
            {
                Debug.Log("Got All Three Pieces! Giving shield Upgrade");
                stats.GetShieldUpgrade();
                shieldPieceCount = 0;
                shield.SetActive(true);
            }
            else
            {
                Debug.Log("Got a shield Piece!");
                shieldPieceCount += 1;
            }
            SaveData.Instance.ShieldPieceCount = shieldPieceCount;
            SaveLoad.SaveProgress();
        }
    }
    public void GetHeartUpgrade()
    {
        if (heartPieceCount < 3)
        {
            if (heartPieceCount + 1 >= 3)
            {
                Debug.Log("Got All Three Pieces! Giving Health Upgrade");
                stats.GetHPUpgrade();
                stats.hp = stats.MaxHP;
                heartPieceCount = 0;
               

                if (!hp1.activeInHierarchy)
                {
                    hp1.SetActive(true);
                    hp11.SetActive(true);
                }
                else if (!hp2.activeInHierarchy)
                {
                    hp2.SetActive(true);
                }
                else
                {
                    Debug.Log("Already Have all Upgraddes!");
                }
            }
            else
            {
                Debug.Log("Got A Heart Piece");
                heartPieceCount += 1;
            }
            SaveData.Instance.HeartPieceCount = heartPieceCount;
            SaveLoad.SaveProgress();
        }
    }
    public void GetBatteryUpgrade()
    {
        if (batteryPieceCount < 3)
        {
            if (batteryPieceCount + 1 >= 3)
            {
                Debug.Log("Got All Three Pieces! Giving Shield Upgrade");
                stats.GetBatteryUpgrade();
                batteryPieceCount = 0;
                if (!bat1.activeInHierarchy)
                {
                    bat1.SetActive(true);
                }
                else if (!bat2.activeInHierarchy)
                {
                    bat2.SetActive(true);
                }
                else
                {
                    Debug.Log("Already Have all Upgraddes!");
                }
            }
            else
            {
                Debug.Log("Got A Battery Piece");
                batteryPieceCount += 1;
            }
            SaveData.Instance.BatteryPieceCount = batteryPieceCount;
            SaveLoad.SaveProgress();
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
        SaveLoad.SaveProgress();
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
        SaveLoad.SaveProgress();

        wepMan.missileLauncherIcon.SetActive(false);
	    wepMan.homingMissileIcon.SetActive(false);
	    wepMan.shockSpikeIcon.SetActive(false);
	    wepMan.noneRIcon.SetActive(false);
	   
    }
    public void UnlockSword()
    {
        Debug.Log("Equipping Sword!");
        DisableAllLeftHandWeapons();
        sword.SetActive(true);
        swordBlade.SetActive(true);
	    hasSword = true;
        SaveData.Instance.HasSword = hasSword;
        SaveLoad.SaveProgress();
        wepMan.EquipSword();
	    wepMan.totalLeftWeapons++;
	    if(wepMan.currentLeftWeaponIndex+1 > wepMan.totalLeftWeapons){
	    	wepMan.currentLeftWeaponIndex = 0;
	    }
	    else{
	    	wepMan.currentLeftWeaponIndex++;
	    }
	    wepMan.unlockedLeftWeapon.Add("Sword");
	    
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
        DisableAllLeftHandWeapons();
        shockProng.SetActive(true);
	    hasShockProng = true;

        SaveData.Instance.HasShockProng = hasShockProng;
        SaveLoad.SaveProgress();

        wepMan.EquipShockProng();
	    wepMan.totalLeftWeapons++;
	    if(wepMan.currentLeftWeaponIndex+1 > wepMan.totalLeftWeapons){
	    	wepMan.currentLeftWeaponIndex = 0;
	    }
	    else{
	    	wepMan.currentLeftWeaponIndex++;
	    }
	    wepMan.unlockedLeftWeapon.Add("ShockProng");
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
        shockSpike.SetActive(true);
	    hasShockSpike = true;
        SaveData.Instance.HasShockSpike = hasShockSpike;
        SaveLoad.SaveProgress();
        wepMan.totalRightWeapons++;
	    wepMan.EquipShockSpike();
	    if(wepMan.currentRightWeaponIndex+1 > wepMan.totalRightWeapons){
	    	wepMan.currentRightWeaponIndex = 0;
	    }
	    else{
	    	wepMan.currentRightWeaponIndex++;
	    }
	    wepMan.unlockedRightWeapon.Add("ShockSpike");
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
        missiles.SetActive(true);
	    hasMissiles = true;

        SaveData.Instance.HasMissiles = hasMissiles;
        SaveLoad.SaveProgress();

        wepMan.totalRightWeapons++;
	    wepMan.EquipMissiles();
	    if(wepMan.currentRightWeaponIndex+1 > wepMan.totalRightWeapons){
	    	wepMan.currentRightWeaponIndex = 0;
	    }
	    else{
	    	wepMan.currentRightWeaponIndex++;
	    }
	    wepMan.unlockedRightWeapon.Add("Missiles");
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
        DisableAllRightHandWeapons();
        homingMissiles.SetActive(true);
	    hasHomingMissiles = true;

        SaveData.Instance.HasHomingMissiles= homingMissiles;
        SaveLoad.SaveProgress(); 

        wepMan.totalRightWeapons++;
	    wepMan.EquipHomingMissiles();
	    if(wepMan.currentRightWeaponIndex+1 > wepMan.totalRightWeapons){
	    	wepMan.currentRightWeaponIndex = 0;
	    }
	    else{
	    	wepMan.currentRightWeaponIndex++;
	    }
	    wepMan.unlockedRightWeapon.Add("HomingMissiles");
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
        hasJetBoost = true;
        SaveData.Instance.HasJetBoost = hasJetBoost;
        SaveLoad.SaveProgress();
    }
    public void UnlockVertBoost()
    {
        Debug.Log("Equipping Vertical Boost!");
        vertBoost.SetActive(true);
        hasVertBoost = true;
        SaveData.Instance.HasVertBoost = hasVertBoost;
        SaveLoad.SaveProgress();
    }


    // Update is called once per frame
    void Update()
    {

    }
}
