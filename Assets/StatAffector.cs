using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatAffector : MonoBehaviour
{
    public enum EffectType{HpDrain, BatteryDrain, HpHeal, shieldCharge};
    public EffectType affect;
    [SerializeField]
    float hpEffectAmount;
    [SerializeField]
    float batteryEffectAmount;
    [SerializeField]
    float shieldAffectAmount;
    [SerializeField]
    [Tooltip("Will this pickup dissapear when picked up?")]
    bool dissapear;
    [SerializeField]
    [Tooltip("Will this pickup reappear after picked up?")]
    bool reappear;
    [SerializeField]
    [Tooltip("How long until this pickup reappears?")]
    float reappearTimer = 5f;
    void ReappearTrigger(){
        if(GetComponent<BoxCollider>() != null){
            if(GetComponent<MeshRenderer>()!= null){
                GetComponent<BoxCollider>().enabled = true;
                GetComponent<MeshRenderer>().enabled = true;
            }
        }
    }
        void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player"){
            if(affect == EffectType.HpDrain){
                if(other.transform.root.gameObject.GetComponent<PlayerStats>() != null){
                    //Debug.Log("Took " + hpEffectAmount + " Damage!");
                    other.transform.root.gameObject.GetComponent<PlayerStats>().TakeDamage(hpEffectAmount);
                }
            }
            if(affect == EffectType.BatteryDrain){
                if(other.transform.root.gameObject.GetComponent<PlayerStats>() != null){
                    Debug.Log("Drained Battery by " + batteryEffectAmount);
                    other.transform.root.gameObject.GetComponent<PlayerStats>().DrainBattery(batteryEffectAmount);
                }
            }  
            if(affect == EffectType.HpHeal){
                if(other.transform.root.gameObject.GetComponent<PlayerStats>() != null){
                    Debug.Log("Healed" + hpEffectAmount + " Damage");
                    other.transform.root.gameObject.GetComponent<PlayerStats>().RestoreHP(hpEffectAmount);
                }
            }            
            if(affect == EffectType.shieldCharge){
                if(other.transform.root.gameObject.GetComponent<PlayerStats>() != null){
                    if(other.transform.root.gameObject.GetComponent<PlayerStats>().hasShieldUpgrade){
                        Debug.Log("Charged Shield by "+ shieldAffectAmount);
                        other.transform.root.gameObject.GetComponent<PlayerStats>().ChargeShield(shieldAffectAmount);
                    }
                }
            }
            if(dissapear){
                if(reappear){
                    if(GetComponent<BoxCollider>() != null){
                        if(GetComponent<MeshRenderer>()!= null){
                            GetComponent<BoxCollider>().enabled = false;
                            GetComponent<MeshRenderer>().enabled = false;
                            Invoke("ReappearTrigger", reappearTimer);
                        }
                    }
                }
                else{
                    Destroy(this.gameObject);
                }
                
            }
        }
    }
}
