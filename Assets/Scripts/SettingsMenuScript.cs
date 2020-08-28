using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Rendering;

public class SettingsMenuScript : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Slider volumeSlider;

    public TMP_Dropdown resolutionDropDown;

    private Resolution[] resolutions;

  
    
    void Start()
    {
     
    
        //This line will store our slider value
  
        //Resolution Stuff
    
        //For the resolutions options, we created a resolution array beforehand
        //And create it start
        resolutions = Screen.resolutions;
       
        //We clear out the oens in the options menu for now...
        resolutionDropDown.ClearOptions();

       //And we begin to convert the resolution into strings
       //As we can't add number options to the drop down. It wants a string
       List<string> options = new List<string>();

       //This int is important in selecting the correct default resolution.
       int currentResolutionIndex = 0;
       
       //loop through each element in the resolutions array, and then create it into a string...
       for (int i = 0; i < resolutions.Length; i++)
       {
           string option = resolutions[i].width + "x" + resolutions[i].height;
           //Add it to the options list
           options.Add(option);

           //Check to see if current resolution on the left is equal to the one on the screen)
           if (resolutions[i].width == Screen.width && 
               resolutions[i].height == Screen.height )
           {
               currentResolutionIndex = i;
           }
           
       }
       
       //Now we can add it to the drop down.
       resolutionDropDown.AddOptions(options);
       resolutionDropDown.value = currentResolutionIndex;
       resolutionDropDown.RefreshShownValue();
    }

    public void Update()
    {
       
    }


    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height, Screen.fullScreen);
    }

    public void SetVolume(float volume)
    {
        Debug.Log(volume);
        audioMixer.SetFloat("Volume",Mathf.Log10(volume) *20);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    
    public static void DontDestroyChildOnLoad( GameObject child )
    {
        Transform parentTransform = child.transform;
 
        // If this object doesn't have a parent then its the root transform.
        while ( parentTransform.parent != null )
        {
            // Keep going up the chain.
            parentTransform = parentTransform.parent;
        }
        GameObject.DontDestroyOnLoad(parentTransform.gameObject);
    }
}
