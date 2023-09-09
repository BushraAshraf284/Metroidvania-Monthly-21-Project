using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SaveData{

	public static SaveData Instance;

	// Player Health
	public float playerHP = 0;
	
	// Player Shield
	public bool hasSheild = false;
	public float shieldCharge = 0;

	// Player Weapons
	public bool HasShockProng;
	public bool HasMissiles;
	public bool HasSword;
	public bool HasHomingMissiles;
	public bool HasJetBoost;
	public bool HasVertBoost;
	public bool HasShockSpike;
    public int LeftWeaponEquipped;
	public int RightWeaponEquipped;

	//Player Upgrades
	public bool HasShieldUpgrade;
	public int HPPhase;
	public int BatteryPhase;
	public int HeartPieceCount;
	public int ShieldPieceCount;
	public int BatteryPieceCount;

	//World Upgrade
	public List<bool> UpgradesPickedUp;

    //Door Upgrade
    public List<bool> DoorsOpened;



    //Constructor to save actual GameData
    public SaveData(){}

	//Constructor to check any tampering with the SaveData
    public SaveData(float hp, bool hasShield, float shieldCharge, bool hasShockProng, bool hasMissiles, bool hasSword, bool hasHomingMissiles, bool hasJetBoost, bool hasVertBoost, bool hasShockSpike,
        int leftweapon, int rightWeapon, bool hasShieldUpgrade, int hpPhase, int batteryPhase, int heartPieceCount, int shieldPieceCount, int batteryPieceCount, List<bool> isPickedUp, List<bool> doorsOpened)
    {
        playerHP = hp;
        hasSheild = hasShield;
        this.shieldCharge = shieldCharge;
        HasShockProng = hasShockProng;
        HasMissiles = hasMissiles;
        HasSword = hasSword;
        HasHomingMissiles = hasHomingMissiles;
        HasJetBoost = hasJetBoost;
        HasVertBoost = hasVertBoost;
        HasShockSpike = hasShockSpike;
        LeftWeaponEquipped = leftweapon;
        RightWeaponEquipped = rightWeapon;
        HasShieldUpgrade = hasShieldUpgrade;
        HPPhase = hpPhase;
        BatteryPhase = batteryPhase;
        HeartPieceCount = heartPieceCount;
        ShieldPieceCount = shieldPieceCount;
        BatteryPieceCount = batteryPieceCount;
        UpgradesPickedUp = isPickedUp;
        DoorsOpened = doorsOpened;
    }



}