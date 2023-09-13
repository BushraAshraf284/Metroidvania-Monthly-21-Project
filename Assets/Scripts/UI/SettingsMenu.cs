//Author: Brian Meginness
//Debugging: Brian Meginness
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class SettingsMenu : MonoBehaviour
{
    //Menu components
    Slider volSlide;
    Slider baseFOVSlide;
    Slider aimFOVSlide;
    Slider mouseSens;
    Dropdown resDrop;
    Toggle fullToggle;
    Dropdown qualDrop;

    [SerializeField]
    OrbitCamera orbitCamera;

    Resolution[] resolutions;

    // Start is called before the first frame update
    void Start()
    {
        //Get components
        volSlide = GameObject.Find("VolumeSlide").GetComponent<Slider>();
        volSlide.value = 0.5f;
        resDrop = GameObject.Find("Resolution").GetComponent<Dropdown>();
        resolutions = Screen.resolutions.Where(resolution => resolution.refreshRate == Screen.currentResolution.refreshRate).ToArray();
        fullToggle = GameObject.Find("Fullscreen").GetComponent<Toggle>();
        fullToggle.isOn = Screen.fullScreen;
        baseFOVSlide = GameObject.Find("BaseFOVSlider").GetComponent<Slider>();
        baseFOVSlide.value = 0.5f;
        aimFOVSlide = GameObject.Find("AimFOVSlider").GetComponent<Slider>();
        aimFOVSlide.value = 0.5f;
        mouseSens = GameObject.Find("MouseSensitivitySlider").GetComponent<Slider>();
        mouseSens.value = 0.5f;
        //qualDrop = GameObject.Find("Quality").GetComponent<Dropdown>();
        //Get available, current resolutions for resolutions dropdown
        GetResolutions();

        //GetQuality();
    }

    //On volume slider change
    public void changeVol()
    {
        //Slider OnChange() is called when initialized, sometimes before start() can finish
        if (volSlide)
        {
            AudioListener.volume = volSlide.value * 2;
        }

    }

    public void changeBaseFOV()
    {
        //Slider OnChange() is called when initialized, sometimes before start() can finish
        if (baseFOVSlide)
        {
            orbitCamera.baseFOV = baseFOVSlide.value;
        }

    }

    public void changeAimFOV()
    {
        //Slider OnChange() is called when initialized, sometimes before start() can finish
        if (aimFOVSlide)
        {
            orbitCamera.aimFOV = aimFOVSlide.value;
        }

    }

    public void changeMouseSens()
    {
        if (mouseSens)
        {
            orbitCamera.rotationSpeed = mouseSens.value;
        }
    }

    public void SetDefaultMouseSens()
    {
        orbitCamera.rotationSpeed = 100f;
    }

    public void SetDefaultBaseFOV()
    {
        orbitCamera.baseFOV = 90f;
    }

    public void SetDefaultAimFOV()
    {
        orbitCamera.aimFOV = 50f;
    }

    //Get available, current resolutions for resolutions dropdown
    private void GetResolutions()
    {
        //Find current resolution by index
        int currentResIndex = 0;

        //FOR all resolutions, add width x height to options
        List<string> options = new List<string>();
        for (int i = 0; i < resolutions.Length; i++)
        {
            options.Add(resolutions[i].width + "x" + resolutions[i].height);
            //IF resolution i is current resolution, save index
            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        //Add options to dropdown, set active to current resolution
        resDrop.AddOptions(options);
        resDrop.value = currentResIndex;
    }

    //Set screen resolution
    public void SetResolution(int resolutionIndex)
    {
        //Get resolution based on list index
        Resolution resolution = resolutions[resolutionIndex];
        //Set screen resolution
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    //Set whether window is fullscreen
    public void SetFullscreen(bool fullscreen)
    {
        Screen.SetResolution(Screen.width, Screen.height, fullscreen);
    }

  //  private void GetQuality()
  //  {
        //Get current quality index
   //     int currentQuality = QualitySettings.GetQualityLevel();

        //IF there are available quality settings
    //    if (QualitySettings.names.Length > 0) {
            //Remove placeholder
    //        qualDrop.ClearOptions();
            //Set dropdown options to all available quality settings
   //         qualDrop.AddOptions(new List<string>(QualitySettings.names));
   //     }
   // }

    //Set graphics engine quality preset
    public void setQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }
}
