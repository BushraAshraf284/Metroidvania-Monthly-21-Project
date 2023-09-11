using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GetItemScreen : MonoBehaviour
{
    public GameObject popupPanel;
    public TextMeshProUGUI popupText;
    public TextMeshProUGUI upgradeDesc;
    public Image popupImage;
    public Sprite[] upgradeImages;
    [SerializeField]
    Movement movement;
    [SerializeField]
    OrbitCamera cameraMovement;

    public void ShowItem(string itemName, int itemIndex, string itemDesc)
    {
        popupText.text = "You have acquired the " + itemName + "!";
        upgradeDesc.text = itemDesc;
        if(itemIndex >= 0 && itemIndex < upgradeImages.Length)
        {
            popupImage.sprite = upgradeImages[itemIndex];
            popupImage.enabled = true;
        }
        else
        {
            popupImage.enabled = false;
        }
        popupPanel.SetActive(true);
        movement.blockMovement();
        cameraMovement.enabled = false;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
    }

    public void HidePopup()
    {
        popupPanel.SetActive(false);
        movement.unblockMovement();
        cameraMovement.enabled = true;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        Time.timeScale = 1;
    }
}
