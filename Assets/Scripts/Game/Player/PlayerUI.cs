using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    [Header("Referencias UI")]
    [SerializeField] private Image healthBarFill;

    #region ciclos de vida
    private void OnEnable()
    {
        SubscribeToEvents();
    }

    private void OnDisable()
    {
        UnsubscribeFromEvents();
    }
    #endregion

    #region events y logica de ui
    private void SubscribeToEvents()
    {
        PlayerController.OnPlayerHealthChanged += UpdateHealthBar;
    }

    private void UnsubscribeFromEvents()
    {
        PlayerController.OnPlayerHealthChanged -= UpdateHealthBar;
    }

    private void UpdateHealthBar(float currentHealth, float maxHealth)
    {
        healthBarFill.fillAmount = currentHealth / maxHealth;
    }
    #endregion
}