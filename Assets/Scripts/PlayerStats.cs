//adapted and modified from video "How to make a HEALTH BAR in Unity!" by Brackeys
//https://www.youtube.com/watch?v=BLfNP4Sc_iA
//Author: Sandeep and Travis
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using FMOD.Studio;

//this script will just keep track of the player's various stats and allow other scripts to access and edit them
public class PlayerStats : MonoBehaviour
{
	[SerializeField]
	Transform caveSpawnPos;
	[SerializeField]
	Transform shipSpawnPos;
	public bool comingFromCave;
	public bool comingFromShip;
	
	[SerializeField]
	OrbitCamera cam;
	[SerializeField]
	GameObject X1Base;
	[SerializeField]
	GameObject X1Chunks;
	[SerializeField]
	GameObject deathScreen;
    [SerializeField]
    [Tooltip("Max HP for the player without any health upgrades")]
	float MaxHpPhase1 = 100f;
    [SerializeField]
    [Tooltip("Max HP for the player with one health upgrade")]
	float MaxHpPhase2 = 120f;
    [SerializeField]
    [Tooltip("Max HP for the player with two health upgrades")]
	float MaxHpPhase3 = 150f;
    public float MaxHP;
	public float hp = 0;
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
	public float charge = 0;
    [SerializeField]
	BatteryBar batteryBar;
    [SerializeField]
	BatteryBar batteryBar2;
    [SerializeField]
	BatteryBar batteryBar3;
	float batteryChargeTimer = 0f;
	public bool isBatteryCharging;
    private EventInstance playerBatteryCharge; // Audio
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
	public float shieldCharge = 0;
    public bool hasShieldUpgrade;
    [SerializeField]
    ShieldCell shieldCell;

    public bool hasIFrames;
    [SerializeField]
    [Tooltip("How long the player is invincible after taking damage")]
    public float iFrameCooldown = 1f;
	float iFrameCount;
	bool iFrameCooldownBlock;
	
	void LateAwake(){
		if(comingFromShip){
			if(shipSpawnPos != null){
				this.transform.position = shipSpawnPos.position;
				comingFromShip = false;
				SaveData.Instance.comingFromShip = false;
				
			}
		}
		else if (comingFromCave){
			if(caveSpawnPos != null){
				this.transform.position = caveSpawnPos.position;
				comingFromCave = false;
				SaveData.Instance.comingFromCave = false;
			}
		}
	}

    private void Awake()
	{      
		comingFromCave = SaveData.Instance.comingFromCave;
		comingFromShip = SaveData.Instance.comingFromShip;
        hp = SaveData.Instance.playerHP;
        hasShield = SaveData.Instance.hasSheild;
        shieldCharge = SaveData.Instance.shieldCharge;
        hasShieldUpgrade = SaveData.Instance.HasShieldUpgrade;
        batteryphase = (BAPhase)SaveData.Instance.BatteryPhase;
	    healthphase =(HPPhase) SaveData.Instance.HPPhase;
	    UpdateMaxes();
	    if(batteryphase == BAPhase.Phase3){
		    UpdateBatteryMaxes();
		    batteryBar.gameObject.SetActive(false);
		    batteryBar2.gameObject.SetActive(false);
		    batteryBar3.gameObject.SetActive(true);

		    if(batteryBar3.batterySlider.GetComponent<Slider>() != null){
			    if(batteryBar3.fill.GetComponent<RectTransform>() != null){
				    batteryBar3.batterySlider.GetComponent<Slider>().maxValue = MaxChargePhase3;
				    batteryBar3.batterySlider.GetComponent<Slider>().fillRect = batteryBar3.fill.GetComponent<RectTransform>();
			    }
		    }
	    }
		Invoke("LateAwake", .5f);

    }


    public void RestoreHP(float healAmount){
        AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHeal, this.transform.position);

        if (hp + healAmount >= MaxHP){
            hp = MaxHP;
        }
        else{
            hp += healAmount;
        }
        SaveData.Instance.playerHP = hp;
    }
    
	public void DrainBattery(float drain){
		if (charge - drain < 0){
			//Debug.Log("Went from "+hp+" to 0");
            batteryChargeTimer = 0f;
			charge = 0;
            
        }
		else {
			//Debug.Log("Drained "+drain+" from total amount "+ charge+ " for a total " + (charge-drain));
			if(charge != charge-drain){
                batteryChargeTimer = 0f;
				charge = charge-drain;
			}
		}
        
        // AudioManager.instance.PlayOneShot(FMODEvents.instance.playerBatteryDrain, this.transform.position);
    }
    public void ResetHasIFrames(){
        hasIFrames = false;
    }
    public void ChargeBattery(){
        if (isBatteryCharging){
            PLAYBACK_STATE playbackState;
            playerBatteryCharge.getPlaybackState(out playbackState);
            if (playbackState.Equals(PLAYBACK_STATE.STOPPED))
            {
                playerBatteryCharge.start();
            }
            
        } else{
            playerBatteryCharge.stop(STOP_MODE.ALLOWFADEOUT);
        }

        if (charge < MaxCharge){
            if(batteryChargeTimer < batteryChargeCap){
	            batteryChargeTimer += Time.deltaTime;
	            isBatteryCharging = false;
            }
            else{
            	isBatteryCharging = true;
                charge += Time.deltaTime * batteryChargeRate;
            }
        }
        else if (charge >= MaxCharge){
	        charge = MaxCharge;
	        isBatteryCharging = false;
        }
    }

    public void TakeDamage(float damage){
        if(!hasIFrames){
            if(!hasShield){
                if (hp - damage < 0){
                    //Debug.Log("Went from "+hp+" to 0");
                    //Debug.Log("Zero HP!");
	                deathScreen.SetActive(true);
	                X1Base.SetActive(false);
	                Instantiate(X1Chunks, this.transform.position, Quaternion.identity);
	                GetComponent<Movement>().blockMovement();
	                cam.enabled = false;
	                Cursor.visible = true; //makes cursor visible
	                Cursor.lockState = CursorLockMode.None;//makes cursor moveable
                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerDeath, this.transform.position);
                    hp = 0;
                    SaveData.Instance.playerHP = hp;
                }
                else {

                    AudioManager.instance.PlayOneShot(FMODEvents.instance.playerHurt, this.transform.position);

                    if (hp != Mathf.Round(hp-damage)){
                        //Debug.Log("Took " + damage + " Damage");
                        hasIFrames = true;
                        iFrameCount = 0f;
                        hp = Mathf.Round(hp-damage);
                        SaveData.Instance.playerHP = hp;
                    }
                }
            }
            else{
                //Debug.Log("Shield Blocked Damage!");
                hasIFrames = true;
                iFrameCount = 0f;
                hasShield = false;
                shieldCharge = 0f;

                SaveData.Instance.hasSheild = hasShield; 
                SaveData.Instance.shieldCharge = shieldCharge;
            }   
        }
        else{
            Debug.Log("Took Damage during I Frames!");
        }

       

    }
    public void GetShieldUpgrade(){
        if(!hasShieldUpgrade){
            shieldCell.transform.parent.gameObject.SetActive(true);
            hasShieldUpgrade = true;
            hasShield = true;
	        shieldCharge = maxShieldCharge;
	        SaveData.Instance.shieldCharge = shieldCharge;
            SaveData.Instance.hasSheild = hasShield;
            SaveData.Instance.HasShieldUpgrade = hasShieldUpgrade;

            Debug.Log("Got Shield Upgrade!");
        }
        else{
            Debug.Log("You already have this shield upgrade!");
        }
    }
	public void CheckShieldUpgrade(){
		if(hasShieldUpgrade){
			shieldCell.transform.parent.gameObject.SetActive(true);
		}
	}
    public void GetBatteryUpgrade(){
        if(batteryphase == BAPhase.Phase1){
            batteryphase = BAPhase.Phase3;
            UpdateBatteryMaxes();
            batteryBar.gameObject.SetActive(false);
            batteryBar3.gameObject.SetActive(true);

            if(batteryBar3.batterySlider.GetComponent<Slider>() != null){
                if(batteryBar3.fill.GetComponent<RectTransform>() != null){
                    
	                batteryBar3.batterySlider.GetComponent<Slider>().maxValue = MaxChargePhase3;
                    batteryBar3.batterySlider.GetComponent<Slider>().fillRect = batteryBar3.fill.GetComponent<RectTransform>();
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

        SaveData.Instance.BatteryPhase = (int)batteryphase;
    }
	public void CheckBatteryUpgrade(){
		if(batteryphase == BAPhase.Phase1){
			UpdateBatteryMaxes();
			batteryBar.gameObject.SetActive(true);
			batteryBar2.gameObject.SetActive(false);
			batteryBar3.gameObject.SetActive(false);
			if(batteryBar.batterySlider.GetComponent<Slider>() != null){
				if(batteryBar.fill.GetComponent<RectTransform>() != null){
					batteryBar.batterySlider.GetComponent<Slider>().maxValue = MaxChargePhase1;
					batteryBar.batterySlider.GetComponent<Slider>().fillRect = batteryBar.fill.GetComponent<RectTransform>();
				}
			}
		}
		else if(batteryphase == BAPhase.Phase2){
			UpdateBatteryMaxes();
			batteryBar.gameObject.SetActive(false);
			batteryBar2.gameObject.SetActive(true);
			batteryBar3.gameObject.SetActive(false);

			if(batteryBar2.batterySlider.GetComponent<Slider>() != null){
				if(batteryBar2.fill.GetComponent<RectTransform>() != null){
                    
					batteryBar2.batterySlider.GetComponent<Slider>().maxValue = MaxChargePhase2;
					batteryBar2.batterySlider.GetComponent<Slider>().fillRect = batteryBar2.fill.GetComponent<RectTransform>();
				}
			}
		}
		else if(batteryphase == BAPhase.Phase3){
			UpdateBatteryMaxes();
			batteryBar.gameObject.SetActive(false);
			batteryBar2.gameObject.SetActive(false);
			batteryBar3.gameObject.SetActive(true);

			if(batteryBar3.batterySlider.GetComponent<Slider>() != null){
				if(batteryBar3.fill.GetComponent<RectTransform>() != null){
					batteryBar3.batterySlider.GetComponent<Slider>().maxValue = MaxChargePhase3;
					batteryBar3.batterySlider.GetComponent<Slider>().fillRect = batteryBar3.fill.GetComponent<RectTransform>();
				}
			}
		}
	}
    public void GetHPUpgrade(){
        if(healthphase == HPPhase.Phase1){
            healthBar.gameObject.SetActive(false);
            healthBar3.gameObject.SetActive(true);
            healthphase = HPPhase.Phase3;
            UpdateHPMaxes();

            if(healthBar3.healthSlider.GetComponent<Slider>() != null){
                if(healthBar3.fill.GetComponent<RectTransform>() != null){
                    healthBar3.healthSlider.GetComponent<Slider>().maxValue = MaxHpPhase2;
                    healthBar3.healthSlider.GetComponent<Slider>().fillRect = healthBar3.fill.GetComponent<RectTransform>();
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
        SaveData.Instance.HPPhase = (int)healthphase;
    }
	public void CheckHPUpgrade(){
		if(healthphase == HPPhase.Phase1){
			
			if(healthBar3.gameObject.activeInHierarchy){
				healthBar3.gameObject.SetActive(false);
			}
			if(!healthBar.gameObject.activeInHierarchy){
				healthBar.gameObject.SetActive(true);
			}
			if(healthBar2.gameObject.activeInHierarchy){
				healthBar2.gameObject.SetActive(false);
			}
			if(healthBar.healthSlider.GetComponent<Slider>() != null){
				if(healthBar.fill.GetComponent<RectTransform>() != null){
					healthBar.healthSlider.GetComponent<Slider>().maxValue = MaxHpPhase1;
					healthBar.healthSlider.GetComponent<Slider>().fillRect = healthBar.fill.GetComponent<RectTransform>();
				}
			}
		}
		else if(healthphase == HPPhase.Phase2){
			if(healthBar3.gameObject.activeInHierarchy){
				healthBar3.gameObject.SetActive(false);
			}
			if(healthBar.gameObject.activeInHierarchy){
				healthBar.gameObject.SetActive(false);
			}
			if(!healthBar2.gameObject.activeInHierarchy){
				healthBar2.gameObject.SetActive(false);
			}
			if(healthBar2.healthSlider.GetComponent<Slider>() != null){
				if(healthBar2.fill.GetComponent<RectTransform>() != null){
					healthBar2.healthSlider.GetComponent<Slider>().maxValue = MaxHpPhase2;
					healthBar2.healthSlider.GetComponent<Slider>().fillRect = healthBar2.fill.GetComponent<RectTransform>();
				}
			}
		}
		else if(healthphase == HPPhase.Phase3){
			if(!healthBar3.gameObject.activeInHierarchy){
				healthBar3.gameObject.SetActive(true);
			}
			if(healthBar.gameObject.activeInHierarchy){
				healthBar.gameObject.SetActive(false);
			}
			if(healthBar2.gameObject.activeInHierarchy){
				healthBar2.gameObject.SetActive(false);
			}
			if(healthBar3.healthSlider.GetComponent<Slider>() != null){
				if(healthBar3.fill.GetComponent<RectTransform>() != null){
					healthBar3.healthSlider.GetComponent<Slider>().maxValue = MaxHpPhase3;
					healthBar3.healthSlider.GetComponent<Slider>().fillRect = healthBar3.fill.GetComponent<RectTransform>();
				}
			}
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

        SaveData.Instance.hasSheild = hasShield;
        SaveData.Instance.shieldCharge = shieldCharge;
    }
    void UpdateHPMaxes(){
        if(healthphase == HPPhase.Phase1){
            MaxHP = MaxHpPhase1;
	        healthBar.SetMaxHealth(MaxHpPhase1);
	        if(hp!= 0){
	        	//if doesnt equal zero, then its being carried over from previous scene
	        	CheckHPUpgrade();
	        }
	        else{
	        	//if it does equal zero, its coming from a fresh started scene
	        	hp = MaxHpPhase1;
	        }
            
        }
        else if(healthphase == HPPhase.Phase2){
            Debug.Log("Got Health Upgrade #1!, Set max HP to " + MaxHpPhase2 + " From " + MaxCharge);
            MaxHP = MaxHpPhase2;
	        healthBar.SetMaxHealth(MaxHpPhase2);
	        if(hp!= 0){
	        	CheckHPUpgrade();
	        }
	        else{
	        	hp = MaxHpPhase2;
	        }
        }
        else if(healthphase == HPPhase.Phase3){
            Debug.Log("Got Health Upgrade #2!, Set max HP to " + MaxHpPhase3 + " From " + MaxCharge);
            MaxHP = MaxHpPhase3;
            healthBar.SetMaxHealth(MaxHpPhase3);
	        if(hp!= 0){
	        	CheckHPUpgrade();
	        }
	        else{
	        	hp = MaxHpPhase3;
	        }
        }
        SaveData.Instance.playerHP = hp;
    }
    void UpdateShieldMaxes(){
	    if(hasShieldUpgrade){
		    shieldCell.transform.parent.gameObject.SetActive(true);
	        shieldCell.SetMaxCharge(maxShieldCharge);

        }
    }
    void UpdateBatteryMaxes(){
	    if(batteryphase == BAPhase.Phase1){
		    batteryBar.gameObject.SetActive(true);
		    batteryBar2.gameObject.SetActive(false);
		    batteryBar3.gameObject.SetActive(false);
            MaxCharge = MaxChargePhase1;
            batteryBar.SetMaxCharge(MaxChargePhase1);
	        charge = MaxCharge;
        }
	    else if(batteryphase == BAPhase.Phase2){
		    batteryBar.gameObject.SetActive(false);
		    batteryBar2.gameObject.SetActive(true);
		    batteryBar3.gameObject.SetActive(false);
            Debug.Log("Got Battery Upgrade #1!, Set max charge to " + MaxChargePhase2 + " From " + MaxCharge);
            MaxCharge = MaxChargePhase2;
            batteryBar2.SetMaxCharge(MaxChargePhase2);
            charge = MaxCharge;
        }
	    else if(batteryphase == BAPhase.Phase3){
		    batteryBar.gameObject.SetActive(false);
		    batteryBar2.gameObject.SetActive(false);
		    batteryBar3.gameObject.SetActive(true);
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
        playerBatteryCharge = AudioManager.instance.CreateEventInstance(FMODEvents.instance.playerBatteryCharge);
        //Test line to see if we can set a default start point
        //when game is started, it sets the slider max value to hp value
		//healthBar.SetMaxHealth(hp);
		if(hasShield){
			
		}
    }

    void Update()
	{
		if(!iFrameCooldownBlock){
	        if(hasIFrames){
	            if(iFrameCount < iFrameCooldown ){
	                iFrameCount += Time.deltaTime;
	            }
	            else{
	                hasIFrames = false;
	            }
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
	public void ForceIFramesStart(){
		Debug.Log("Iframes On");
		hasIFrames = true;
		iFrameCount = 0f;
		iFrameCooldownBlock = true;
		Invoke("ForceIFramesStop", 1f);
	}
	public void ForceIFramesStop(){
		hasIFrames = false;
		Debug.Log("Iframes Off");
		iFrameCount = 0f;
		iFrameCooldownBlock = false;
	}

}
