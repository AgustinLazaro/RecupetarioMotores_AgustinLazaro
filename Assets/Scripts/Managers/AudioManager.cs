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

    public void SetMasterVolume(float sliderValue)
    {
        mainMixer.SetFloat("MASTERvol", Mathf.Log10(sliderValue) * 20f);
    }

    public void SetMusicVolume(float sliderValue)
    {
        mainMixer.SetFloat("MUSICvol", Mathf.Log10(sliderValue) * 20f);
    }

    public void SetSFXVolume(float sliderValue)
    {
        mainMixer.SetFloat("SFXvol", Mathf.Log10(sliderValue) * 20f);
    }
}