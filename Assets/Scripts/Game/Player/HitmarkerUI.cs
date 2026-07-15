using UnityEngine;

public class HitmarkerUI : MonoBehaviour
{
    [Header("Configuración Visual")]
    [SerializeField] private GameObject hitmarkerImage;
    [SerializeField] private float hitmarkerDuration = 0.5f;

    private float hitmarkerTimer = 0f;

    // llamado prendido
    private void OnEnable()
    {
        WeaponController.OnEnemyHit += ActivateHitmarker;
    }

    private void Update()
    {
        HandleTimer();
    }

    //Prendo hit market y arranca 
    private void ActivateHitmarker()
    {
        hitmarkerImage.SetActive(true);
        hitmarkerTimer = hitmarkerDuration;
    }

    // countdown frame x frame para apagar hitmarker
    private void HandleTimer()
    {
        if (hitmarkerTimer > 0)
        {
            hitmarkerTimer -= Time.deltaTime;
            if (hitmarkerTimer <= 0)
            {
                hitmarkerImage.SetActive(false);
            }
        }
    }

    // llamado apagado
    private void OnDisable()
    {
        WeaponController.OnEnemyHit -= ActivateHitmarker;
    }
}