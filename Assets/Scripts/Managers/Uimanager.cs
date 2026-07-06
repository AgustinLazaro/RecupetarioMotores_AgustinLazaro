using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;


public class Uimanager : MonoBehaviour
{
    [Header("Manager de Audio")]
    [SerializeField] private AudioManager classAudioManager;

    [Header("Sliders")]
    [SerializeField] private Slider SliderMaster;
    [SerializeField] private Slider SliderMusic;
    [SerializeField] private Slider SliderSFX;

    [Header("Escena de combate")]
    [SerializeField] private Button btnPlay;

    [Header("Botones")]
    [SerializeField] private Button btnOptions;
    [SerializeField] private Button btnCredits;
    [SerializeField] private Button btnAutor;
    [SerializeField] private Button btnAudio;

    [Header("Paneles")]
    [SerializeField] private GameObject OptionsPanel;
    [SerializeField] private GameObject CreditsPanel;
    [SerializeField] private GameObject AudioPanel;
    [SerializeField] private GameObject AutorPanel;

    [Header("Botones Back")]
    [SerializeField] private Button btnBackOptions;
    [SerializeField] private Button btnBackCredits;
    [SerializeField] private Button btnBackAutor;
    [SerializeField] private Button btnBackAudio;





    private void Start()
    {
        btnPlay.onClick.AddListener(LoadGame);

        btnOptions.onClick.AddListener(() => Panel(OptionsPanel));
        btnCredits.onClick.AddListener(() => Panel(CreditsPanel));
        btnAutor.onClick.AddListener(() => Panel(AutorPanel));
        btnAudio.onClick.AddListener(() => Panel(AudioPanel));
        //BackBtn
        btnBackOptions.onClick.AddListener(() => Back(OptionsPanel));
        btnBackCredits.onClick.AddListener(() => Back(CreditsPanel));
        btnBackAutor.onClick.AddListener(() => Back(AutorPanel));
        btnBackAudio.onClick.AddListener(() => Back(AudioPanel));
        //SFX
        btnPlay.onClick.AddListener(classAudioManager.PlayClick);
        btnOptions.onClick.AddListener(classAudioManager.PlayClick);
        btnCredits.onClick.AddListener(classAudioManager.PlayClick);
        btnAutor.onClick.AddListener(classAudioManager.PlayClick);
        btnAudio.onClick.AddListener(classAudioManager.PlayClick);

        btnBackOptions.onClick.AddListener(classAudioManager.PlayClick);
        btnBackCredits.onClick.AddListener(classAudioManager.PlayClick);
        btnBackAutor.onClick.AddListener(classAudioManager.PlayClick);
        btnBackAudio.onClick.AddListener(classAudioManager.PlayClick);
        //sliders
        // Conexión de Sliders al AudioManager
        SliderMaster.onValueChanged.AddListener(classAudioManager.SetMasterVolume);
        SliderMusic.onValueChanged.AddListener(classAudioManager.SetMusicVolume);
        SliderSFX.onValueChanged.AddListener(classAudioManager.SetSFXVolume);

    }

    private void LoadGame()
    {
        SceneManager.LoadScene("Scene02");
    }


    private void Panel(GameObject OpenPanel)
    {
        OpenPanel.SetActive(true);
    }


    private void Back(GameObject ClosePanel)
    {
        ClosePanel.SetActive(false);
    }

}







