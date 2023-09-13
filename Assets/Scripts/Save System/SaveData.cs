﻿using UnityEngine;
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
	public List<string> unlockedLeftWeapon = new List<string>();
	public List<string> unlockedRightWeapon = new List<string>();
	public int currentLeftWeaponIndex, totalLeftWeapons;
	public int currentRightWeaponIndex, totalRightWeapons;
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

    // Each Scene Data (Doors and Upgrades)
    public SceneData HubData;
    public SceneData CaveData;
    public SceneData ShipData;

    // NPC Data
    public NPCsData NPCsData;

    //Save Point Info
    public Vector3 playerSavePos;
    public int LastSaveScene = 1;
    public bool isSaved;

    //Scene progress Info
    public bool EnteredWorld1 = false;
    public bool EnteredWorld2 = false;
    //Constructor to save actual GameData
    public SaveData(){

    HubData = new SceneData();
        CaveData = new SceneData();
        ShipData = new SceneData();
    }
    
	//bools for map loads
	public bool comingFromCave;
	public bool comingFromShip;

	//Constructor to check any tampering with the SaveData
    public SaveData(float hp, bool hasShield, float shieldCharge, bool hasShockProng, bool hasMissiles, bool hasSword, bool hasHomingMissiles, bool hasJetBoost, bool hasVertBoost, bool hasShockSpike,
        int leftweapon, int rightWeapon, bool hasShieldUpgrade, int hpPhase, int batteryPhase, int heartPieceCount, int shieldPieceCount, int batteryPieceCount, SceneData hubData, SceneData caveData, SceneData shipData, NPCsData nPCsData,
        Vector3 playerSavePos, int lastSaveScene, bool isDead, bool enteredWorld1, bool enteredWorld2)
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
        HubData = hubData;
        CaveData = caveData;
        ShipData = shipData;
        NPCsData = nPCsData;
        this.playerSavePos = playerSavePos;
        LastSaveScene = lastSaveScene;
        this.isSaved = isDead;
        EnteredWorld1 = enteredWorld1;
        EnteredWorld2 = enteredWorld2;
    }



}

[System.Serializable]
public class SceneData
{
   public List<bool> DoorsOpened;
   public List<bool> UpgradesPickedUp;

    public SceneData() { 
        DoorsOpened = new List<bool>();
        UpgradesPickedUp = new List<bool>();
    
    }
}

[System.Serializable]
public class NPCsData
{
    public List <NPC> NPCs;

    public NPCsData()
    {
        NPCs = new List<NPC>();
    }
}

[System.Serializable]
public class NPC
{
    public bool DoneIntro;
    public bool Repaired;

    public NPC (bool doneIntro, bool repaired)
    { this.DoneIntro = doneIntro; this.Repaired = repaired;}

   
}

public enum sceneType { Cave, Hub, Ship };
