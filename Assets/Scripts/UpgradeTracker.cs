using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTracker : MonoBehaviour
{
    [SerializeField]
    GameObject shockProng, shockSpike, missiles, homingMissiles, jetBoost, vertBoost, sword;
    public bool hasShockProng, hasShockSpike, hasMissiles, hasHomingMissiles, hasJetBoost, hasVertBoost, hasSword;
    
    // Start is called before the first frame update
    public void UnlockSword(){
        sword.SetActive(true);
        hasSword = true;
    }
    public void UnlockShockProng(){
        shockProng.SetActive(true);
        hasShockProng = true;
    }
    public void UnlockShockSpike(){
        shockSpike.SetActive(true);
        hasShockSpike = true;
    }
    public void UnlockMissiles(){
        missiles.SetActive(true);
        hasMissiles = true;
    }
    public void UnlockHomingMissiles(){
        homingMissiles.SetActive(true);
        hasHomingMissiles = true;
    }
    public void UnlockJetBoost(){
        jetBoost.SetActive(true);
        hasJetBoost = true;
    }
    public void UnlockVertBoost(){
        vertBoost.SetActive(true);
        hasVertBoost = true;
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
