//adapted and modified from video "How to make a HEALTH BAR in Unity!" by Brackeys
//https://www.youtube.com/watch?v=BLfNP4Sc_iA
//Author: Sandeep
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BatteryBar : MonoBehaviour
{
	public Slider batterySlider;

  //method used in PlayerStats class to set the max value of the slider
	public void SetMaxCharge(float charge)
  {
    //sets max value of health equal to health
	  batterySlider.maxValue = charge;
    //sets current value of health equal to health
    batterySlider.value = charge;

  }
  //method used in the PlayerStats class to set the current value of health to the slider
	public void SetCharge(float charge)
  {
    batterySlider.value = charge;
  }
}
