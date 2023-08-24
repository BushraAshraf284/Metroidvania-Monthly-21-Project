//adapted and modified from video "How to make a HEALTH BAR in Unity!" by Brackeys
//https://www.youtube.com/watch?v=BLfNP4Sc_iA
//Author: Sandeep
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShieldCell : MonoBehaviour
{
	public Slider shieldSlider;

  //method used in PlayerStats class to set the max value of the slider
  [SerializeField]
  public GameObject fill;
	public void SetMaxCharge(float charge)
  {
    //sets max value of shield equal to shield
	  shieldSlider.maxValue = charge;
    //sets current value of shield equal to shield
    shieldSlider.value = charge;

  }
  //method used in the PlayerStats class to set the current value of shield to the slider
	public void SetCharge(float charge)
  {
    shieldSlider.value = charge;
  }
}
