﻿using UnityEngine;
using System.IO;
using System.Text;

public class SaveLoad {

	public static void SaveProgress(){
		string saveDataHashed = JsonUtility.ToJson (SaveData.Instance, true);
		File.WriteAllText (GetSavePath (), saveDataHashed);
	}

	public static SaveData SaveObjectCreator(){
		SaveData CheckSave = new SaveData(SaveData.Instance.playerHP, SaveData.Instance.hasSheild, SaveData.Instance.shieldCharge, SaveData.Instance.HasShockProng, SaveData.Instance.HasMissiles, SaveData.Instance.HasSword,
			SaveData.Instance.HasHomingMissiles, SaveData.Instance.HasJetBoost, SaveData.Instance.HasVertBoost, SaveData.Instance.HasShockSpike, SaveData.Instance.LeftWeapon, SaveData.Instance.RightWeapon,
			SaveData.Instance.HasShieldUpgrade, SaveData.Instance.HPPhase, SaveData.Instance.BatteryPhase, SaveData.Instance.HeartPieceCount, SaveData.Instance.ShieldPieceCount, SaveData.Instance.BatteryPieceCount,
			SaveData.Instance.HubData, SaveData.Instance.CaveData, SaveData.Instance.ShipData, SaveData.Instance.NPCsData, SaveData.Instance.playerSavePos, SaveData.Instance.LastSaveScene, SaveData.Instance.isSaved,
            SaveData.Instance.EnteredWorld1, SaveData.Instance.EnteredWorld2, SaveData.Instance.controlData);
		return CheckSave;
	}

	public static string SaveObjectJSON(){
		string saveDataString = JsonUtility.ToJson (SaveObjectCreator(), true);
		return saveDataString;
	}

	public static void LoadProgress(){
		if (File.Exists (GetSavePath ())) {
			string fileContent = File.ReadAllText (GetSavePath());
			JsonUtility.FromJsonOverwrite (fileContent, SaveData.Instance);
			Debug.Log ("Game Load Successful --> "+GetSavePath ());
		} else {
			Debug.Log ("New Game Creation Successful --> "+GetSavePath ());
			SaveProgress ();
		}
	}

	public static void DeleteProgress(){
		if (File.Exists (GetSavePath ())) {
			File.Delete (GetSavePath());
		}
	}

	private static string GetSavePath(){
		return Path.Combine(Application.persistentDataPath,"SavedGame.json");
	}
}
