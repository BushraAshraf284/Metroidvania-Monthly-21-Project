using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI upgradeText;
    public Image upgradeIcon;
    public GameObject[] upgradeIcons;

    [SerializeField]
    UpgradeTracker upgradeTracker;

    private void Start()
    {

    }

    public void ShowUpgrade(string upgradeName)
    {
        if (upgradeName == "Sword")
        {
            upgradeIcons[0].SetActive(true);
        }

        else if (upgradeName == "Shock Prong")
        {
            upgradeIcons[1].SetActive(true);
        }

        else if (upgradeName == "Shock Spike")
        {
            upgradeIcons[2].SetActive(true);
        }

        else if (upgradeName == "Missiles")
        {
            upgradeIcons[3].SetActive(true);
        }

        else if (upgradeName == "Homing Missiles")
        {
            upgradeIcons[4].SetActive(true);
        }

        else if (upgradeName == "Jet Booster")
        {
            upgradeIcons[5].SetActive(true);
        }

        else if (upgradeName == "Vertical Booster")
        {
            upgradeIcons[6].SetActive(true);
        }
        else if (upgradeName == "IC Chip") { 
            
        }
        else
        {
            upgradeIcon.enabled = false;
        }
        pauseMenu.SetActive(true);
    }

    public void ShowHeartUpgrade()
    {
        upgradeIcons[7].SetActive(true);
        upgradeIcons[7].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "1/3";
    }

    public void ShowHeartUpgradeTwo()
    {
        upgradeIcons[7].SetActive(true);
        upgradeIcons[7].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "2/3";
    }

    public void UpdateHeartUpgrade()
    {
        upgradeIcons[7].SetActive(true);
        upgradeIcons[7].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "3/3";
    }

    public void ShowShieldUpgrade()
    {
        upgradeIcons[8].SetActive(true);
        upgradeIcons[8].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "1/3";
    }

    public void ShowShieldUpgradeTwo()
    {
        upgradeIcons[8].SetActive(true);
        upgradeIcons[8].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "2/3";
    }

    public void UpdateShieldUpgrade()
    {
        upgradeIcons[8].SetActive(true);
        upgradeIcons[8].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "3/3";
    }

    public void ShowBatteryUpgrade()
    {
        upgradeIcons[9].SetActive(true);
        upgradeIcons[9].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "1/3";
    }

    public void ShowBatteryUpgradeTwo()
    {
        upgradeIcons[9].SetActive(true);
        upgradeIcons[9].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "2/3";
    }

    public void UpdateBatteryUpgrade()
    {
        upgradeIcons[9].SetActive(true);
        upgradeIcons[9].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = "3/3";
    }
    public void ShowICChip() {
        upgradeIcons[10].SetActive(true);
        upgradeIcons[10].transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text =""+ upgradeTracker.iCChipCount;
    }
}
