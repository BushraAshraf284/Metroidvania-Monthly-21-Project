using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public sceneType sceneType;
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

    }



    public void SaveDoorData()
    {
        if(sceneType == sceneType.Hub)
        {
            if (SaveData.Instance.HubData.DoorsOpened.Count == 0)
            {
                SaveData.Instance.HubData.DoorsOpened = new List<bool>();
                for (int i = 0; i < doorManager.Doors.Count; i++)
                {
                    SaveData.Instance.HubData.DoorsOpened.Add(false);
                }
            }

            for (int i = 0; i < doorManager.Doors.Count; i++)
            {
                SaveData.Instance.HubData.DoorsOpened[i] = doorManager.Doors[i].GetComponent<platformAnimController>().isOpened;
            }
        }
        else if (sceneType == sceneType.Cave)
        {
            if (SaveData.Instance.CaveData.DoorsOpened.Count == 0)
            {
                SaveData.Instance.CaveData.DoorsOpened = new List<bool>();
                for (int i = 0; i < doorManager.Doors.Count; i++)
                {
                    SaveData.Instance.CaveData.DoorsOpened.Add(false);
                }
            }

            for (int i = 0; i < doorManager.Doors.Count; i++)
            {
                SaveData.Instance.CaveData.DoorsOpened[i] = doorManager.Doors[i].GetComponent<platformAnimController>().isOpened;
            }
        }
        else if(sceneType == sceneType.Ship)
        {
            if (SaveData.Instance.ShipData.DoorsOpened.Count == 0)
            {
                SaveData.Instance.ShipData.DoorsOpened = new List<bool>();
                for (int i = 0; i < doorManager.Doors.Count; i++)
                {
                    SaveData.Instance.ShipData.DoorsOpened.Add(false);
                }
            }

            for (int i = 0; i < doorManager.Doors.Count; i++)
            {
                SaveData.Instance.ShipData.DoorsOpened[i] = doorManager.Doors[i].GetComponent<platformAnimController>().isOpened;
            }
        }

    }

    public void LoadDoors(sceneType sceneType)
    {
        if (sceneType == sceneType.Hub)
        {
            if (SaveData.Instance.HubData.DoorsOpened == null)
            {
                SaveData.Instance.HubData.DoorsOpened = new List<bool>();
            }
            for (int i = 0; i < doorManager.Doors.Count; i++)
            {

                if (i < SaveData.Instance.HubData.DoorsOpened.Count)
                    doorManager.Doors[i].GetComponent<platformAnimController>().isOpened = SaveData.Instance.HubData.DoorsOpened[i];
                doorManager.Doors[i].GetComponent<platformAnimController>().Init();
            }
        }
        else if (sceneType == sceneType.Ship)
        {
            if (SaveData.Instance.ShipData.DoorsOpened == null)
            {
                SaveData.Instance.ShipData.DoorsOpened = new List<bool>();
            }
            for (int i = 0; i < doorManager.Doors.Count; i++)
            {

                if (i < SaveData.Instance.ShipData.DoorsOpened.Count)
                    doorManager.Doors[i].GetComponent<platformAnimController>().isOpened = SaveData.Instance.ShipData.DoorsOpened[i];
                doorManager.Doors[i].GetComponent<platformAnimController>().Init();
            }
        }
        else if(sceneType == sceneType.Cave)
        {
            if (SaveData.Instance.CaveData.DoorsOpened == null)
            {
                SaveData.Instance.CaveData.DoorsOpened = new List<bool>();
            }
            for (int i = 0; i < doorManager.Doors.Count; i++)
            {

                if (i < SaveData.Instance.CaveData.DoorsOpened.Count)
                    doorManager.Doors[i].GetComponent<platformAnimController>().isOpened = SaveData.Instance.CaveData.DoorsOpened[i];
                doorManager.Doors[i].GetComponent<platformAnimController>().Init();
            }
        }
           
       
    }

    public void SaveUpgrades()
    {
        if (sceneType == sceneType.Hub)
        {
            if (SaveData.Instance.HubData.UpgradesPickedUp.Count == 0)
            {
                SaveData.Instance.HubData.UpgradesPickedUp = new List<bool>();
                for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
                {
                    SaveData.Instance.HubData.UpgradesPickedUp.Add(false);
                }
            }
            for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
            {
               if(upgradeManager.Upgrades[i] !=null)
                {   
                    if(i >= SaveData.Instance.HubData.UpgradesPickedUp.Count)
                    {
                        SaveData.Instance.HubData.UpgradesPickedUp.Add(upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp);
                    }
                    else
                    {
                        SaveData.Instance.HubData.UpgradesPickedUp[i] = upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp;
                        Debug.Log("i:" + i + "upgrade value" + SaveData.Instance.HubData.UpgradesPickedUp[i]);
                    }
                   
                }
               
            }
        }
        else if (sceneType == sceneType.Cave)
        {
            if (SaveData.Instance.CaveData.UpgradesPickedUp.Count == 0)
            {
                SaveData.Instance.CaveData.UpgradesPickedUp = new List<bool>();
                for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
                {
                    SaveData.Instance.CaveData.UpgradesPickedUp.Add(false);
                }
            }
            for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
            {
                SaveData.Instance.CaveData.UpgradesPickedUp[i] = upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp;
                
            }
        }
        else if (sceneType == sceneType.Ship)
        {
            if (SaveData.Instance.ShipData.UpgradesPickedUp.Count == 0)
            {
                SaveData.Instance.ShipData.UpgradesPickedUp = new List<bool>();
                for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
                {
                    SaveData.Instance.ShipData.UpgradesPickedUp.Add(false);
                }
            }
            for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
            {
                SaveData.Instance.ShipData.UpgradesPickedUp[i] = upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp;
                
            }
        }
    }

    public void LoadUpgrades(sceneType sceneType)
    { 

        if(sceneType == sceneType.Hub)
        {
            if (SaveData.Instance.HubData.UpgradesPickedUp == null)
            {
                SaveData.Instance.HubData.UpgradesPickedUp = new List<bool>();
            }

            for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
            {
                if (i < SaveData.Instance.HubData.UpgradesPickedUp.Count)
                    upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp = SaveData.Instance.HubData.UpgradesPickedUp[i];
                upgradeManager.Upgrades[i].GetComponent<Upgrade>().Init();
            }
        }
        else if(sceneType == sceneType.Cave)
        {
            if (SaveData.Instance.CaveData.UpgradesPickedUp == null)
            {
                SaveData.Instance.CaveData.UpgradesPickedUp = new List<bool>();
            }

            for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
            {
                if (i < SaveData.Instance.CaveData.UpgradesPickedUp.Count)
                    upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp = SaveData.Instance.CaveData.UpgradesPickedUp[i];
                upgradeManager.Upgrades[i].GetComponent<Upgrade>().Init();
            }

        }
        else if(sceneType == sceneType.Ship)
        {
            if (SaveData.Instance.ShipData.UpgradesPickedUp == null)
            {
                SaveData.Instance.ShipData.UpgradesPickedUp = new List<bool>();
            }

            for (int i = 0; i < upgradeManager.Upgrades.Count; i++)
            {
                if (i < SaveData.Instance.ShipData.UpgradesPickedUp.Count)
                    upgradeManager.Upgrades[i].GetComponent<Upgrade>().isPickedUp = SaveData.Instance.ShipData.UpgradesPickedUp[i];
                upgradeManager.Upgrades[i].GetComponent<Upgrade>().Init();
            }
        }
       
    }

    
}
