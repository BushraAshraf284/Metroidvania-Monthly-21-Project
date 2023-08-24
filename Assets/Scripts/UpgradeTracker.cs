using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

public class UpgradeTracker : MonoBehaviour
{
    [SerializeField]
    GameObject shockProng, shockSpike, missiles, homingMissiles, jetBoost, vertBoost, sword;
    public bool hasShockProng, hasShockSpike, hasMissiles, hasHomingMissiles, hasJetBoost, hasVertBoost, hasSword;
    public int heartPieceCount;
    public int shieldPieceCount;
    public int batteryPieceCount;
    PlayerStats stats;
    void Start()
    {
        stats = GetComponent<PlayerStats>();
    }
    // Start is called before the first frame update
    public void GetShieldUpgrade(){
        if(shieldPieceCount < 3){
            if(shieldPieceCount + 1 >= 3){
                Debug.Log("Got All Three Pieces! Giving shield Upgrade");
                stats.GetShieldUpgrade();
                shieldPieceCount = 0;
            }
            else{
                Debug.Log("Got a shield Piece!");
                shieldPieceCount += 1;
            }
        }
    }
    public void GetHeartUpgrade(){
        if(heartPieceCount < 3){
            if(heartPieceCount + 1 >= 3){
                Debug.Log("Got All Three Pieces! Giving Health Upgrade");
                stats.GetHPUpgrade();
                heartPieceCount = 0;
            }
            else{
                Debug.Log("Got A Heart Piece");
                heartPieceCount += 1;
            }
        }
    }
    public void GetBatteryUpgrade(){
        if(batteryPieceCount < 3){
            if(batteryPieceCount + 1 >= 3){
                Debug.Log("Got All Three Pieces! Giving Shield Upgrade");
                stats.GetBatteryUpgrade();
                batteryPieceCount = 0;
            }
            else{
                Debug.Log("Got A Battery Piece");
                batteryPieceCount += 1;
            }
        }
    }
    void DisableAllLeftHandWeapons(){
        sword.SetActive(false);
        shockProng.SetActive(false);
        hasSword = false;
        hasShockProng = false;
    }
    void DisableAllRightHandWeapons(){
        shockSpike.SetActive(false);
        missiles.SetActive(false);
        homingMissiles.SetActive(false);
        hasHomingMissiles = false;
        hasShockSpike = false;
        hasMissiles = false;
    }
    public void UnlockSword(){
        Debug.Log("Equipping Sword!");
        DisableAllLeftHandWeapons();
        sword.SetActive(true);
        hasSword = true;
    }
    public void UnlockShockProng(){
        Debug.Log("Equipping Shock Prong!");
        DisableAllLeftHandWeapons();
        shockProng.SetActive(true);
        hasShockProng = true;
    }
    public void UnlockShockSpike(){
        Debug.Log("Equipping Shock Spike!");
        DisableAllRightHandWeapons();
        shockSpike.SetActive(true);
        hasShockSpike = true;
    }
    public void UnlockMissiles(){
        Debug.Log("Equipping Missiles!");
        DisableAllRightHandWeapons();
        missiles.SetActive(true);
        hasMissiles = true;
    }
    public void UnlockHomingMissiles(){
        Debug.Log("Equipping Homing Missiles!");
        DisableAllRightHandWeapons();
        homingMissiles.SetActive(true);
        hasHomingMissiles = true;
    }
    public void UnlockJetBoost(){
        Debug.Log("Equipping Jet Boost!");
        jetBoost.SetActive(true);
        hasJetBoost = true;
    }
    public void UnlockVertBoost(){
        Debug.Log("Equipping Vertical Boost!");
        vertBoost.SetActive(true);
        hasVertBoost = true;
    }
    //none for vertboost and jet boost cause they are just always on 


    // Update is called once per frame
    void Update()
    {
        
    }
}
