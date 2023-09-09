using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public enum sceneType { Cave, Hub, Ship };
    public sceneType type;
    [SerializeField]
    public List<GameObject> Upgrades = new List<GameObject>();

}
