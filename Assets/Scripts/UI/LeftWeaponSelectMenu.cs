using UnityEngine;
using UnityEngine.UI;

public class LeftWeaponSelectionMenu : MonoBehaviour
{
    public Image[] weaponIcons;
    public Image equippedLeftWeaponImage;
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
        Weapon equippedLeftWeapon = weaponManager.weapons[weaponManager.currentWeaponIndex];

        if (equippedLeftWeapon != null)
        {
            equippedLeftWeaponImage.sprite = equippedLeftWeapon.icon;
        }
        else
        {
            equippedLeftWeaponImage.sprite = null;
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

    public void SelectPreviousWeapon()
    {
        weaponManager.SelectPreviousWeapon();
        UpdateEquippedWeaponUI();
    }
}
