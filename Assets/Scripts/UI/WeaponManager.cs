using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class WeaponManager : MonoBehaviour
{
    public Weapon[] weapons;
    public int currentWeaponIndex = 0;

    [SerializeField]
    GameObject shockProng, shockSpike, missiles, homingMissiles, sword, swordBlade;
    public bool hasShockProng, hasShockSpike, hasMissiles, hasHomingMissiles, hasSword;
    public enum LeftWeaponEquipped { Sword, ShockProng, none };
    public LeftWeaponEquipped leftWeapon;
    public enum RightWeaponEquipped { Missiles, HomingMissiles, ShockSpike, none };
    public RightWeaponEquipped rightWeapon;

    void Start()
    {
        if (leftWeapon == LeftWeaponEquipped.Sword)
        {
            if (hasSword)
            {
                EquipSword();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (leftWeapon == LeftWeaponEquipped.ShockProng)
        {
            if (hasShockProng)
            {
                EquipShockProng();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (leftWeapon == LeftWeaponEquipped.none)
        {
            DisableAllLeftHandWeapons();
        }
        if (rightWeapon == RightWeaponEquipped.Missiles)
        {
            if (hasMissiles)
            {
                EquipMissiles();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (rightWeapon == RightWeaponEquipped.HomingMissiles)
        {
            if (hasHomingMissiles)
            {
                EquipHomingMissiles();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (rightWeapon == RightWeaponEquipped.ShockSpike)
        {
            if (hasShockSpike)
            {
                EquipShockSpike();
            }
            else
            {
                DisableAllRightHandWeapons();
            }
        }
        else if (rightWeapon == RightWeaponEquipped.none)
        {
            DisableAllRightHandWeapons();
        }
    }

    void DisableAllLeftHandWeapons()
    {
        swordBlade.SetActive(false);
        sword.SetActive(false);
        shockProng.SetActive(false);
        hasSword = false;
        hasShockProng = false;
    }
    void DisableAllRightHandWeapons()
    {
        shockSpike.SetActive(false);
        missiles.SetActive(false);
        homingMissiles.SetActive(false);
        hasHomingMissiles = false;
        hasShockSpike = false;
        hasMissiles = false;
    }

    public void EquipSword()
    {
        Debug.Log("Equipping Sword!");
        DisableAllLeftHandWeapons();
        sword.SetActive(true);
        swordBlade.SetActive(true);
    }

    public void EquipShockProng()
    {
        Debug.Log("Equipping Shock Prong!");
        DisableAllLeftHandWeapons();
        shockProng.SetActive(true);
    }

    public void EquipShockSpike()
    {
        Debug.Log("Equipping Shock Spike!");
        DisableAllRightHandWeapons();
        shockSpike.SetActive(true);
    }

    public void EquipMissiles()
        {
            Debug.Log("Equipping Missiles!");
            DisableAllRightHandWeapons();
            missiles.SetActive(true);
        }

    public void EquipHomingMissiles()
        {
            Debug.Log("Equipping Homing Missiles!");
            DisableAllRightHandWeapons();
            homingMissiles.SetActive(true);
        }

    public void SelectNextWeapon()
    {
        currentWeaponIndex++;
        if (currentWeaponIndex >= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

    }

    public void SelectPreviousWeapon()
    {
        currentWeaponIndex--;
        if (currentWeaponIndex <= weapons.Length)
        {
            currentWeaponIndex = 0;
        }

    }
}

