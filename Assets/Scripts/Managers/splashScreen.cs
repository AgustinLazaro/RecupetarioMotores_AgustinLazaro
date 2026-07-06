using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class SplashScreen : MonoBehaviour
{
    [Header("Configuración visual")]
    [SerializeField] private Image imageLogo;
    [SerializeField] private float timeVisible = 2f; 
    [SerializeField] private float SpeedFade = 1f; 

    [Header("Configuración Escena")]
    [SerializeField] private string sceneMenu = "Scene01";
    private float timer;

    void Start()
    {
        timer = timeVisible;
    }

     void Update()
    {
        if (timer > 0)
        {
           timer -= Time.deltaTime; 
        }
        else
        {
            Color colorLogo = imageLogo.color;
            colorLogo.a -= Time.deltaTime * SpeedFade;

            imageLogo.color = colorLogo;

            if (colorLogo.a <= 0)
            {
                SceneManager.LoadScene(sceneMenu);
            }
        }

        
    }
}