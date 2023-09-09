using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public DoorManager doorManager;
    public UpgradeManager upgradeManager;

    public static SaveManager Instance { get; private set; }

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        //SaveLoad.LoadProgress();
    }



    public void SaveDoorData()
    {
        if (SaveData.Instance.DoorsOpened.Count == 0)
        {
            SaveData.Instance.DoorsOpened = new List<bool>();
            for (int i = 0; i < doorManager.Doors.Count; i++)
            {
                SaveData.Instance.DoorsOpened.Add(false);
            }
        }
        
        for (int i = 0; i< doorManager.Doors.Count; i++)
        {
            SaveData.Instance.DoorsOpened[i] = doorManager.Doors[i].GetComponent<platformAnimController>().isOpened;
        }
        SaveLoad.SaveProgress();
    }

    public void LoadDoors()
    {
        if (SaveData.Instance.DoorsOpened == null)
        {
            SaveData.Instance.DoorsOpened = new List<bool>();
        }
        for (int i = 0; i < doorManager.Doors.Count; i++)
        {
           
            if(i < SaveData.Instance.DoorsOpened.Count)
                doorManager.Doors[i].GetComponent<platformAnimController>().isOpened = SaveData.Instance.DoorsOpened[i];
            doorManager.Doors[i].GetComponent<platformAnimController>().Init();
        }
       
    }

    public void SaveUpgrades()
    {
        if (SaveData.Instance.UpgradesPickedUp.Count == 0)
        {
            SaveData.Instance.UpgradesPickedUp = new List<bool>();
            for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
            {
                SaveData.Instance.UpgradesPickedUp.Add(false);
            }
        }
        for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
        {
           

            SaveData.Instance.UpgradesPickedUp[i] = upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp;
            SaveLoad.SaveProgress();
        }
    }

    public void LoadUpgrades()
    { 
        if (SaveData.Instance.UpgradesPickedUp == null)
        {
            SaveData.Instance.UpgradesPickedUp = new List<bool>();
        }
       
        for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
        {
            if( i < SaveData.Instance.UpgradesPickedUp.Count)
            upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp = SaveData.Instance.UpgradesPickedUp[i];
            upgradeManager.Upgrades[i].GetComponent<Upgrade>().Init();
        }
    }

    
}
