using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [SerializeField] Sound[] BGM;
    [SerializeField] Sound[] SFX;
    [SerializeField] Sound[] UI_SFX;

    [SerializeField] private float volumeBGMMultiplier;
    [SerializeField] private float volumeSFXMultiplier;
    [SerializeField] private AudioData audioData;


    public static AudioManager Instance;

    // Start is called before the first frame update
    void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        volumeBGMMultiplier = PlayerPrefs.GetFloat("BGM");

        foreach (Sound s in BGM)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume * volumeBGMMultiplier;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }

        foreach (Sound s in SFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }

        foreach (Sound s in UI_SFX)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    private void Start()
    {
        volumeBGMMultiplier = audioData.BGMVolume;
        volumeSFXMultiplier = audioData.SFXVolume;
    }

    public void PlayBGM(string name)
    {
        Sound s = Array.Find(BGM, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError($"Error! Sound of name {name} was not found!");
            return;
        }

        s.source.volume = s.volume * volumeBGMMultiplier; 

        if(!s.source.isPlaying)
        {
            s.source.Play();
        }
        
        Debug.Log($"Playing {s.name}");
    }

    public void StopBGM(string name)
    {
        Sound s = Array.Find(BGM, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError($"Error! Sound of name {name} was not found!");
            return;
        }

        if(s.source.isPlaying)
        {
            s.source.Stop();
        }
        
        Debug.Log($"{s.name} Stopped");
    }

    public Sound PlaySFX(string name)
    {
        Sound s = Array.Find(SFX, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError($"Error! SFX of name {name} was not found!");
            return null;
        }
        s.source.volume = s.volume * volumeSFXMultiplier;
        

        if(!s.source.isPlaying)
        {
            s.source.Play();
        }

        return s;
        
    }

    public Sound PlayUI_SFX(string name)
    {
        Sound s = Array.Find(UI_SFX, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogError($"Error! UI_SFX of name {name} was not found!");
            return null;
        }
        s.source.volume = s.volume * volumeSFXMultiplier;
        
        if(!s.source.isPlaying)
        {
            s.source.Play();
        }

        return s;
       
    }

    // Method for changing value
    public void SetBGMVolume(float value)
    {
        audioData.SetVolumeBGM(value);
        volumeBGMMultiplier = audioData.BGMVolume;

        foreach(Sound s in BGM)
        {
            s.source.volume = s.volume * volumeBGMMultiplier;
        }
    } 

    public void SetSFXVolume(float value)
    {
        audioData.SetVolumeSFX(value);
        volumeBGMMultiplier = audioData.SFXVolume;

        foreach(Sound s in SFX)
        {
            s.source.volume = s.volume * volumeSFXMultiplier;
        }
    }




}

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;

    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}