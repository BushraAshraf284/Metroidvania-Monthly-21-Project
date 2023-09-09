using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    [SerializeField]
    public List<GameObject> Doors = new List<GameObject>();

    private void Start()
    {
        SaveManager.Instance.LoadDoors();
    }

}
