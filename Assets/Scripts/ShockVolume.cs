using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Apply a shock to anything in its collider, charge batteries
public class ShockVolume : MonoBehaviour
{
    PlayerStats stats;
    Battery battery;
    [SerializeField]
    float playerBatteryDrainAmount = .1f;
    [SerializeField]
    bool isSpike;
    /// <summary>
    /// OnTriggerEnter is called when the Collider other enters the trigger.
    /// </summary>
    /// <param name="other">The other Collider involved in this collision.</param>
    void OnTriggerStay(Collider other)
    {
        if(other.gameObject.GetComponent<Battery>()!= null){
            battery = other.gameObject.GetComponent<Battery>();
            battery.ChargeBattery();
        }
    }
    void Start()
    {
        stats = GameObject.Find("3rd Person Character").GetComponent<PlayerStats>();
    }
	void FixedUpdate(){
        if(!isSpike){
            stats.DrainBattery(playerBatteryDrainAmount);
        }
    }
}
