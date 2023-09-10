using UnityEngine;
using TMPro;

public class GetItemScreen : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;

    public void ShowItemAcquiredPopup(string itemName)
    {
        popupText.text = "You acquired: " + itemName + "!";
        popupPanel.SetActive(true);
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
    }
}
