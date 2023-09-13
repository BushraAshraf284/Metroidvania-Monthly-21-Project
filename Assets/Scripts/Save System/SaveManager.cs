using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveManager : MonoBehaviour
{
    public sceneType sceneType;
    public DoorManager doorManager;
    public UpgradeManager upgradeManager;
    public NPCManager NPCManager;
    public Transform Player;
    public GameObject LoadingScreen;
    private string sceneName;
    private List<NPCInteractables> interactables;
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
        LoadingScreen.SetActive(true); 
    }
    private void Start()
    {
       
           

        if (NPCManager)
        {
            interactables = new List<NPCInteractables>();
           for (int i = 0; i < NPCManager.NPCs.Count; i++)
            {
                interactables.Add(NPCManager.NPCs[i].GetComponent<NPCInteractables>());
            }
            LoadNPCData();
           
        }
        StartCoroutine(LoadPlayerWithDelay());
    }
    IEnumerator LoadPlayerWithDelay()
    {
        yield return new WaitForSeconds(1f);
        if (SaveData.Instance.isSaved)
        {
           
            Debug.Log("Tried moving the player");
            sceneType lastScene = (sceneType) SaveData.Instance.LastSaveScene;
            if(lastScene == sceneType)
            {
                Player.position = SaveData.Instance.playerSavePos;
            }
         
           
        }
        LoadingScreen.SetActive(false);
    }

    public void SaveNPCsData()
    {
        if (SaveData.Instance.NPCsData == null)
        {
            SaveData.Instance.NPCsData = new NPCsData();
        }
        if (SaveData.Instance.NPCsData.NPCs.Count == 0)
        {
            for (int i = 0; i < interactables.Count; i++)
            {
                SaveData.Instance.NPCsData.NPCs.Add(new NPC(false, false));
            }
        }
        for (int i = 0; i < interactables.Count; i++)
        {
            int index = i;
            SaveData.Instance.NPCsData.NPCs[index].DoneIntro = interactables[index].doneIntro;
            SaveData.Instance.NPCsData.NPCs[index].Repaired = interactables[index].repaired;
        }
    }

    public void LoadNPCData()
    {
       if(NPCManager)
        {
            if (SaveData.Instance.NPCsData == null)
            {
                Debug.Log("Save data is null");
                SaveData.Instance.NPCsData = new NPCsData();
            }
            for (int i = 0; i < interactables.Count; i++)
            {
                Debug.Log(SaveData.Instance.NPCsData.NPCs.Count);

                if (i < SaveData.Instance.NPCsData.NPCs.Count)
                {
                    int index = i;
                    interactables[index].doneIntro = SaveData.Instance.NPCsData.NPCs[index].DoneIntro;
                    interactables[index].repaired = SaveData.Instance.NPCsData.NPCs[index].Repaired;
                }
                    
            }
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
            if (SaveData.Instance.HubData == null)
            {
                SaveData.Instance.HubData = new SceneData();
            }

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
            if (SaveData.Instance.CaveData == null)
            {
                SaveData.Instance.CaveData = new SceneData();
            }

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
            if (SaveData.Instance.ShipData == null)
            {
                SaveData.Instance.ShipData = new SceneData();
            }
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

    public void SavePointInfo()
    {
        SaveData.Instance.LastSaveScene =(int) sceneType;
        SaveData.Instance.playerSavePos = Player.position;
    }

    public void LoadGameAfterDeath()
    {
        Debug.Log("Load i being called");
        SaveLoad.LoadProgress();
        SaveData.Instance.isSaved = true;
        SaveLoad.SaveProgress();
        sceneType scene = (sceneType) SaveData.Instance.LastSaveScene;
        switch (scene)
        {
            case sceneType.Ship:
                sceneName = "Ship Level Design";                
                break;

            case sceneType.Cave:
                sceneName = "Cave Level Design";
                break;

            case sceneType.Hub:
                sceneName = "Hub Level Design";
                break;
        }
        SceneManager.LoadScene(sceneName);
       
    }

    
}
