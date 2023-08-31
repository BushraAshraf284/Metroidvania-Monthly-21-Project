using UnityEngine;
using UnityEngine.UI;

public class RightWeaponSelectionMenu : MonoBehaviour
{
    public Image[] weaponIcons;
    public Image equippedRightWeaponImage;
    public Image noneImage;

    private WeaponManager weaponManager;

    private void Start()
    {
        weaponManager = GetComponent<WeaponManager>();
        UpdateEquippedWeaponUI();
    }

    public void UpdateEquippedWeaponUI()
    {
        WeaponManager weaponManager = FindObjectOfType<WeaponManager>();
        Weapon equippedRightWeapon = weaponManager.weapons[weaponManager.currentWeaponIndex];

        if (equippedRightWeapon != null)
        {
            equippedRightWeaponImage.sprite = equippedRightWeapon.icon;
        }
        else
        {
            equippedRightWeaponImage.sprite = null;
        }

        for (int i = 0; i < weaponIcons.Length; i++)
        {
            if (i < weaponManager.weapons.Length)
            {
                weaponIcons[i].sprite = weaponManager.weapons[i].icon;
            }
            else
            {
                weaponIcons[i].sprite = noneImage.sprite;
            }
        }
    }

    public void SelectNextWeapon()
    {
        weaponManager.SelectNextWeapon();
        UpdateEquippedWeaponUI();
    }
}
