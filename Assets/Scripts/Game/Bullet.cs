using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]
public class Bullet : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float speed = 100f;
    [SerializeField] private float lifeTime = 2f;

    [Header("Owner de la bala")]
    [SerializeField] private bool isPlayerBullet = true;

    #region ciclos de vida
    private void OnEnable()
    {
        Invoke(nameof(DeactivateBullet), lifeTime);
    }

    private void Update()
    {
        HandleMovement();
    }

    private void OnDisable()
    {
        CancelInvoke();
    }
    #endregion

    #region movimiento bala
    private void HandleMovement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
    #endregion

    #region pool y colisiones
    private void OnTriggerEnter(Collider other)
    {
        DeactivateBullet();
    }

    private void DeactivateBullet()
    {
        GameManager.Instance.ReturnBullet(gameObject, isPlayerBullet);
    }
    #endregion
}