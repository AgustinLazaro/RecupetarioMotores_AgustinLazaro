using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Mixer Principal")]
    [SerializeField] private AudioMixer mainMixer;

    [Header("Fuentes de Audio (En este mismo objeto)")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    public void PlayClick()
    { 
        sfxSource.Play();
    }


    // convertor a decibelios 
    public void SetMasterVolume(float sliderValue)
    {
        mainMixer.SetFloat("MASTERvol", LogConversion(sliderValue));
    }

    public void SetMusicVolume(float sliderValue)
    {
        mainMixer.SetFloat("MUSICvol", LogConversion(sliderValue));
    }

    public void SetSFXVolume(float sliderValue)
    {
        mainMixer.SetFloat("SFXvol", LogConversion(sliderValue));
    }

    private static float LogConversion(float sliderValue)
    {
        return Mathf.Log10(sliderValue) * 20f;
    }
}