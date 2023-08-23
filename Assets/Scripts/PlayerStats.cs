﻿//adapted and modified from video "How to make a HEALTH BAR in Unity!" by Brackeys
//https://www.youtube.com/watch?v=BLfNP4Sc_iA
//Author: Sandeep and Travis
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script will just keep track of the player's various stats and allow other scripts to access and edit them
public class PlayerStats : MonoBehaviour
{
	public float MaxHp = 100f;
	public float hp = 100;
	public float MaxCharge = 100f;
	public float charge = 100;
	
	public HealthBar healthBar;
	public HealthBar healthBar2;
	public HealthBar healthBar3;
	public BatteryBar batteryBar;
	public BatteryBar batteryBar2;
	public BatteryBar batteryBar3;
	
	public enum HPPhase{Phase1, Phase2, Phase3};
	public HPPhase healthphase;
	public enum BAPhase{Phase1, Phase2, Phase3};
	public BAPhase batteryphase;

    public void restoreHP(float healAmount){
        if (hp + healAmount > 100){
            hp = 100;
        }
        else{
            hp += healAmount;
        }
    }
    
	public void DrainBattery(float drain){
		if (charge - drain < 0){
			//Debug.Log("Went from "+hp+" to 0");
			charge = 0;

		}
		else {

			//Debug.Log("Went from "+hp+" to "+ Mathf.Round(hp-damage));
			if(charge != Mathf.Round(charge-drain)){
				charge = Mathf.Round(charge-drain);
			}
		}
	}

    public void takeDamage(float damage){
        if (hp - damage < 0){
            //Debug.Log("Went from "+hp+" to 0");
            hp = 0;

        }
        else {

            //Debug.Log("Went from "+hp+" to "+ Mathf.Round(hp-damage));
            if(hp != Mathf.Round(hp-damage)){
                hp = Mathf.Round(hp-damage);
            }
        }
    }

    void Start()
	{
		//WAS WORKING ON IMPLEMENTING DIFFERENT PHASES
		if(healthphase)
        //Test line to see if we can set a default start point
        //when game is started, it sets the slider max value to hp value
        healthBar.SetMaxHealth(hp);
    }

    void Update()
    {
        //updates the slider value to match the current hp value
        healthBar.SetHealth(hp);
    }

}
