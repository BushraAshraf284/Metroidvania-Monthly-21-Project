using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Upgrades = new List<GameObject>();

    private void Awake()
    {
        SaveManager.Instance.LoadUpgrades();
    }

}
