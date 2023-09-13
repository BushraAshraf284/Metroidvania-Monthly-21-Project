using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UpgradeMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public TextMeshProUGUI upgradeText;
    public Image upgradeIcon;
    public Sprite[] upgradeIcons;

    [SerializeField]
    UpgradeTracker upgradeTracker;

    private void Start()
    {
        HideUpgradeUI();
    }

    public void ShowUpgrade(string upgradeName, int iconIndex)
    {
        if(iconIndex >= 0 && iconIndex < upgradeIcons.Length)
        {
            upgradeIcon.sprite = upgradeIcons[iconIndex];
            upgradeText.text = upgradeName;
            upgradeIcon.enabled = true;
        }
        else
        {
            upgradeIcon.enabled = false;
        }
        pauseMenu.SetActive(true);
    }

    public void HideUpgradeUI()
    {
        pauseMenu.SetActive(false);
    }

}
