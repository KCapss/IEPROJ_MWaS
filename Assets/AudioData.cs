using UnityEngine;

[CreateAssetMenuAttribute(menuName = "AudioData", fileName = "New Audio Settings")]
public class AudioData : ScriptableObject
{
    [SerializeField] private float volumeBGM;
    [SerializeField] private float volumeSFX;

    public float BGMVolume => volumeBGM;
    public float SFXVolume => volumeSFX;

    public void SetVolumeBGM(float volume)
    {
        volumeBGM = Mathf.Clamp(volume, 0.0f, 1.0f);
    }

    public void SetVolumeSFX(float volume)
    {
        volumeSFX = Mathf.Clamp(volume, 0.0f, 1.0f);
    }
}
