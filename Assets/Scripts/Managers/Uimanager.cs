using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Uimanager : MonoBehaviour
{

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

    [Header("Sliders")]
    [SerializeField] private Slider SliderMaster;
    [SerializeField] private Slider SliderMusic;
    [SerializeField] private Slider SliderSFX;

    [Header("AudioSource")]
    [SerializeField] private AudioSource musicSource;

    [SerializeField] private AudioSource sfxSource;


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


        //Sliders
        SliderMusic.onValueChanged.AddListener(NewValue => ChangeValue(musicSource, NewValue));
        SliderSFX.onValueChanged.AddListener(NewValue => ChangeValue(sfxSource, NewValue));

        //SFX
        btnPlay.onClick.AddListener(PlayClick);
        btnOptions.onClick.AddListener(PlayClick);
        btnCredits.onClick.AddListener(PlayClick);
        btnAutor.onClick.AddListener(PlayClick);
        btnAudio.onClick.AddListener(PlayClick);

        btnBackOptions.onClick.AddListener(PlayClick);
        btnBackCredits.onClick.AddListener(PlayClick);
        btnBackAutor.onClick.AddListener(PlayClick);
        btnBackAudio.onClick.AddListener(PlayClick);


    }


    private void LoadGame()
    {
        SceneManager.LoadScene("Scene02");
    }



    private void PlayClick()
    {
        sfxSource.Play();
    }


    private void Panel(GameObject OpenPanel)
    {
        OpenPanel.SetActive(true);
    }


    private void Back(GameObject ClosePanel)
    {
        ClosePanel.SetActive(false);
    }



    private void ChangeValue(AudioSource who, float NewValue)
    {
        who.volume = NewValue;
    }

}







