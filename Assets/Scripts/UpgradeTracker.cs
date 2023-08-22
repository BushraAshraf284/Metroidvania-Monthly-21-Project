using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTracker : MonoBehaviour
{
    [SerializeField]
    GameObject shockProng, shockSpike, missiles, homingMissiles, jetBoost, vertBoost, sword;
    // Start is called before the first frame update
    public void UnlockSword(){
        shockProng.SetActive(true);
    }
    public void UnlockShockProng(){
        shockProng.SetActive(true);
    }
    public void UnlockShockSpike(){
        shockSpike.SetActive(true);
    }
    public void UnlockMissiles(){
        missiles.SetActive(true);
    }
    public void UnlockHomingMissiles(){
        homingMissiles.SetActive(true);
    }
    public void UnlockJetBoost(){
        jetBoost.SetActive(true);
    }
    public void UnlockVertBoost(){
        vertBoost.SetActive(true);
    }
    public void EquipShockProng(){

    }
    public void EquipShockSpike(){

    }
    public void EquipMissiles(){

    }
    public void EquipHomingMissiles(){

    }
    //none for vertboost and jet boost cause they are just always on 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
