//adapted and modified from video "How to make a HEALTH BAR in Unity!" by Brackeys
//https://www.youtube.com/watch?v=BLfNP4Sc_iA
//Author: Sandeep and Travis
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//this script will just keep track of the player's various stats and allow other scripts to access and edit them
public class PlayerStats : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Max HP for the player without any health upgrades")]
	float MaxHpPhase1 = 100f;
    [SerializeField]
    [Tooltip("Max HP for the player with one health upgrade")]
	float MaxHpPhase2 = 120f;
    [SerializeField]
    [Tooltip("Max HP for the player with two health upgrades")]
	float MaxHpPhase3 = 150f;
    float MaxHP;
	public float hp = 100;
    [SerializeField]
	HealthBar healthBar;
    [SerializeField]
	HealthBar healthBar2;
    [SerializeField]
	HealthBar healthBar3;
	public enum HPPhase{Phase1, Phase2, Phase3};
	public HPPhase healthphase;

    [SerializeField]
    [Tooltip("Max HP for the player without any battery upgrades")]
	public float MaxChargePhase1 = 100f;
    [SerializeField]
    [Tooltip("Max HP for the player with one battery upgrade")]
	public float MaxChargePhase2 = 120f;
    [SerializeField]
    [Tooltip("Max HP for the player with two battery upgrades")]
	public float MaxChargePhase3 = 150f;
    float MaxCharge;
	public float charge = 100;
    [SerializeField]
	BatteryBar batteryBar;
    [SerializeField]
	BatteryBar batteryBar2;
    [SerializeField]
	BatteryBar batteryBar3;
    float batteryChargeTimer = 0f;
    [SerializeField]
    [Tooltip("How long it takes for the battery's charge to start to refill again")]
    float batteryChargeCap = 3f;
    [SerializeField]
    [Tooltip("How fast does the battery recharge")]
    float batteryChargeRate = 25f;
	public enum BAPhase{Phase1, Phase2, Phase3};
	public BAPhase batteryphase;

	public bool hasShield;
    public float maxShieldCharge = 100f;
    public float shieldCharge = 100f;
    public bool hasShieldUpgrade;
    [SerializeField]
    ShieldCell shieldCell;

    public bool hasIFrames;
    [SerializeField]
    [Tooltip("How long the player is invincible after taking damage")]
    public float iFrameCooldown = 1f;
    float iFrameCount;


    public void RestoreHP(float healAmount){
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHeal, this.transform.position);

        if (hp + healAmount >= MaxHP){
            hp = MaxHP;
        }
        else{
            hp += healAmount;
        }
    }
    
	public void DrainBattery(float drain){
		if (charge - drain < 0){
			//Debug.Log("Went from "+hp+" to 0");
            batteryChargeTimer = 0f;
			charge = 0;

		}
		else {
			//Debug.Log("Went from "+hp+" to "+ Mathf.Round(hp-damage));
			if(charge != charge-drain){
                batteryChargeTimer = 0f;
				charge = charge-drain;
			}
		}
	}
    public void ResetHasIFrames(){
        hasIFrames = false;
    }
    public void ChargeBattery(){
        if(charge < MaxCharge){
            if(batteryChargeTimer < batteryChargeCap){
                batteryChargeTimer += Time.deltaTime;
            }
            else{
                charge += Time.deltaTime * batteryChargeRate;
            }
        }
        else if (charge >= MaxCharge){
            charge = MaxCharge;
        }
    }

    public void TakeDamage(float damage){
        if(!hasIFrames){
            if(!hasShield){
                if (hp - damage < 0){
                    //Debug.Log("Went from "+hp+" to 0");
                    //Debug.Log("Zero HP!");

                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDeath, this.transform.position);

                    hp = 0;
                }
                else {

                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHurt, this.transform.position);

                    if (hp != Mathf.Round(hp-damage)){
                        //Debug.Log("Took " + damage + " Damage");
                        hasIFrames = true;
                        iFrameCount = 0f;
                        hp = Mathf.Round(hp-damage);
                    }
                }
            }
            else{
                //Debug.Log("Shield Blocked Damage!");
                hasIFrames = true;
                iFrameCount = 0f;
                hasShield = false;
                shieldCharge = 0f;
            }   
        }
        else{
            //Debug.Log("Took Damage during I Frames!");
        }
        
    }
    public void GetShieldUpgrade(){
        if(!hasShieldUpgrade){
            shieldCell.transform.parent.gameObject.SetActive(true);
            hasShieldUpgrade = true;
            hasShield = true;
            Debug.Log("Got Shield Upgrade!");
        }
        else{
            Debug.Log("You already have this shield upgrade!");
        }
    }
    public void GetBatteryUpgrade(){
        if(batteryphase == BAPhase.Phase1){
            batteryphase = BAPhase.Phase2;
            UpdateBatteryMaxes();
            batteryBar.gameObject.SetActive(false);
            batteryBar2.gameObject.SetActive(true);

            if(batteryBar2.batterySlider.GetComponent<Slider>() != null){
                if(batteryBar2.fill.GetComponent<RectTransform>() != null){
                    
                    batteryBar2.batterySlider.GetComponent<Slider>().maxValue = MaxChargePhase2;
                    batteryBar2.batterySlider.GetComponent<Slider>().fillRect = batteryBar2.fill.GetComponent<RectTransform>();
                }
            }
        }
        else if(batteryphase == BAPhase.Phase2){
            batteryphase = BAPhase.Phase3;
            UpdateBatteryMaxes();
            batteryBar2.gameObject.SetActive(false);
            batteryBar3.gameObject.SetActive(true);

            if(batteryBar3.batterySlider.GetComponent<Slider>() != null){
                if(batteryBar3.fill.GetComponent<RectTransform>() != null){
                    batteryBar3.batterySlider.GetComponent<Slider>().maxValue = MaxChargePhase3;
                    batteryBar3.batterySlider.GetComponent<Slider>().fillRect = batteryBar3.fill.GetComponent<RectTransform>();
                }
            }
        }
        else if(batteryphase == BAPhase.Phase3){
            Debug.Log("You already have all battery upgrades!");
        }
    }
    public void GetHPUpgrade(){
        if(healthphase == HPPhase.Phase1){
            healthBar.gameObject.SetActive(false);
            healthBar2.gameObject.SetActive(true);
            healthphase = HPPhase.Phase2;
            UpdateHPMaxes();

            if(healthBar2.healthSlider.GetComponent<Slider>() != null){
                if(healthBar2.fill.GetComponent<RectTransform>() != null){
                    healthBar2.healthSlider.GetComponent<Slider>().maxValue = MaxHpPhase2;
                    healthBar2.healthSlider.GetComponent<Slider>().fillRect = healthBar2.fill.GetComponent<RectTransform>();
                }
            }
        }
        else if(healthphase == HPPhase.Phase2){
            healthBar2.gameObject.SetActive(false);
            healthBar3.gameObject.SetActive(true);
            healthphase = HPPhase.Phase3;
            UpdateHPMaxes();
            if(healthBar3.healthSlider.GetComponent<Slider>() != null){
                if(healthBar3.fill.GetComponent<RectTransform>() != null){
                    healthBar3.healthSlider.GetComponent<Slider>().maxValue = MaxHpPhase3;
                    healthBar3.healthSlider.GetComponent<Slider>().fillRect = healthBar3.fill.GetComponent<RectTransform>();
                }
            }
        }
        else if(healthphase == HPPhase.Phase3){
            Debug.Log("You already have all Health upgrades!");
        }
    }
    public void ChargeShield(float shieldChargeAmount){
        if (shieldCharge + shieldChargeAmount >= maxShieldCharge){
            shieldCharge = maxShieldCharge; 
            hasShield = true;
        }
        else{
            shieldCharge += shieldChargeAmount;
        }
    }
    void UpdateHPMaxes(){
        if(healthphase == HPPhase.Phase1){
            MaxHP = MaxHpPhase1;
            healthBar.SetMaxHealth(MaxHpPhase1);
            hp = MaxHP;
        }
        else if(healthphase == HPPhase.Phase2){
            Debug.Log("Got Health Upgrade #1!, Set max HP to " + MaxHpPhase2 + " From " + MaxCharge);
            MaxHP = MaxHpPhase2;
            healthBar.SetMaxHealth(MaxHpPhase2);
            hp = MaxHP;
        }
        else if(healthphase == HPPhase.Phase3){
            Debug.Log("Got Health Upgrade #2!, Set max HP to " + MaxHpPhase3 + " From " + MaxCharge);
            MaxHP = MaxHpPhase3;
            healthBar.SetMaxHealth(MaxHpPhase3);
            hp = MaxHP;
        }
    }
    void UpdateShieldMaxes(){
        if(hasShieldUpgrade){
            shieldCell.SetMaxCharge(shieldCharge);
        }
    }
    void UpdateBatteryMaxes(){
        if(batteryphase == BAPhase.Phase1){
            MaxCharge = MaxChargePhase1;
            batteryBar.SetMaxCharge(MaxChargePhase1);
            charge = MaxCharge;
        }
        else if(batteryphase == BAPhase.Phase2){
            Debug.Log("Got Battery Upgrade #1!, Set max charge to " + MaxChargePhase2 + " From " + MaxCharge);
            MaxCharge = MaxChargePhase2;
            batteryBar2.SetMaxCharge(MaxChargePhase2);
            charge = MaxCharge;
        }
        else if(batteryphase == BAPhase.Phase3){
            Debug.Log("Got Battery Upgrade #2!, Set max charge to " + MaxChargePhase3 + " From " + MaxCharge);
            MaxCharge = MaxChargePhase3;
            batteryBar3.SetMaxCharge(MaxChargePhase3);
            charge = MaxCharge;
        }
    }
    void UpdateMaxes(){
        UpdateHPMaxes();
        UpdateBatteryMaxes();
        UpdateShieldMaxes();

    }
    void Start()
	{
        UpdateMaxes();
        //Test line to see if we can set a default start point
        //when game is started, it sets the slider max value to hp value
        //healthBar.SetMaxHealth(hp);
    }

    void Update()
    {
        if(hasIFrames){
            if(iFrameCount < iFrameCooldown ){
                iFrameCount += Time.deltaTime;
            }
            else{
                hasIFrames = false;
            }
        }
        //updates the slider value to match the current hp value
        if(healthphase == HPPhase.Phase1){
            healthBar.SetHealth(hp);
        }
        if(healthphase == HPPhase.Phase2){
            healthBar2.SetHealth(hp);
        }
        if(healthphase == HPPhase.Phase3){
            healthBar3.SetHealth(hp);
        }
        if(batteryphase == BAPhase.Phase1){
            batteryBar.SetCharge(charge);
        }
        if(batteryphase == BAPhase.Phase2){
            batteryBar2.SetCharge(charge);
        }
        if(batteryphase == BAPhase.Phase3){
            batteryBar3.SetCharge(charge);
        }
        if(hasShieldUpgrade){
            shieldCell.SetCharge(shieldCharge);
        }
        ChargeBattery();
    }

}
