using UnityEngine;
using System.IO;
using System.Text;

public class SaveLoad {

	public static void SaveProgress(){
		string saveDataHashed = JsonUtility.ToJson (SaveData.Instance, true);
		File.WriteAllText (GetSavePath (), saveDataHashed);
	}

	public static SaveData SaveObjectCreator(){
		SaveData CheckSave = new SaveData (SaveData.Instance.RemoveAds, SaveData.Instance.Level, SaveData.Instance.Coins);
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
