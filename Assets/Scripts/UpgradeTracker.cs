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

	string heartDesc = "You have increased your maximum health points!";
	string batteryDesc = "You have increased your maximum battery charge!";
	string shieldDesc = "You have unlocked the shield!";

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
	    if(!SaveData.Instance.unlockedLeftWeapon.Contains("None")){
		    SaveData.Instance.unlockedLeftWeapon.Add("None");
	    }
	    if(!SaveData.Instance.unlockedRightWeapon.Contains("None")){
		    SaveData.Instance.unlockedRightWeapon.Add("None");
	    }
	    
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
                shieldPieceCount = 0;
                shield.SetActive(true);
            }
            else
            {
                Debug.Log("Got a shield Piece!");
                shieldPieceCount += 1;
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
                heartPieceCount = 0;
                hp1.SetActive(true);
                hp11.SetActive(true);
                hp2.SetActive(true);
            }
            else
            {
                Debug.Log("Got A Heart Piece");
                heartPieceCount += 1;
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
                batteryPieceCount = 0;
                bat1.SetActive(true);
                bat2.SetActive(true);
            }
            else
            {
                Debug.Log("Got A Battery Piece");
                batteryPieceCount += 1;
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
        sword.SetActive(true);
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
	    wepMan.unlockedLeftWeapon.Add("Sword");
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
	    wepMan.unlockedLeftWeapon.Add("ShockProng");
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
	    wepMan.unlockedRightWeapon.Add("ShockSpike");
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
	    wepMan.unlockedRightWeapon.Add("Missiles");
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
	    wepMan.unlockedRightWeapon.Add("HomingMissiles");
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
        hasJetBoost = true;
        SaveData.Instance.HasJetBoost = hasJetBoost;
    }
    public void UnlockVertBoost()
    {
        Debug.Log("Equipping Vertical Boost!");
        vertBoost.SetActive(true);
        hasVertBoost = true;
        SaveData.Instance.HasVertBoost = hasVertBoost;
    }


    // Update is called once per frame
    void Update()
    {

    }
}
