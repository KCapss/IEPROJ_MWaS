using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingController : MonoBehaviour
{
    [Header("Sounds")]
    [SerializeField] private Slider sliderBGM;
    [SerializeField] private Slider sliderSFX;

    [Header("Cheat")]
    [SerializeField] private bool isCheatTabOn;
    [SerializeField] private GameObject cheatTab;


    private void OnEnable()
    {
        //cheatTab.SetActive(isCheatTabOn);
        SetupAudio();
        
    }

    //Sounds Section
    public void OnValueBGMChange()
    {
        float value = sliderBGM.value;
        if (value == 0)
        {
            value = 0.01f;
        }

        AudioManager.Instance.SetBGMVolume(value);
        PlayerPrefs.SetFloat("BGM", value);

        Debug.Log($"BGM Volume Change {value} ");

    }

    public void OnValueSFXChange()
    {
        float value = sliderSFX.value;
        if (value == 0)
        {
            value = 0.01f;
        }
        AudioManager.Instance.SetSFXVolume(value);
        PlayerPrefs.SetFloat("SFX", value);

        Debug.Log($"SFX Volume Change {value} ");
    }

    private void SetupAudio()
    {
        float BGMval = PlayerPrefs.GetFloat("BGM");
        float SFXval = PlayerPrefs.GetFloat("SFX");

        if (BGMval == 0)
        {
            sliderBGM.value = 1;
            OnValueBGMChange();
        }

        else
        {
            sliderBGM.value = BGMval;
            OnValueBGMChange();
        }


        if (SFXval == 0)
        {
            sliderSFX.value = 1;
            OnValueSFXChange();
        }

        else
        {
            sliderSFX.value = SFXval;
            OnValueSFXChange();
        }

    }

}
