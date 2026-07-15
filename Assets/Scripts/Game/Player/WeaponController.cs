using System;
using UnityEngine;
public class WeaponController : MonoBehaviour
{
    public static event Action OnEnemyHit;

    [Header("Estadísticas SO")]
    [SerializeField] private WeaponData weaponStats;

    [Header("Referencias Disparo")]
    [SerializeField] private Transform firePoint;

    private float hitmarkerTimer = 0f;

    [Header("Coordenadas Aim")]
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 positionAim;

    [Header("Coordenadas Recoil")]
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 recoilRotation;

    [Header("Variables Estado")]
    [SerializeField] private float recoilTime = 1f;

    #region ciclo de vida
    void Update()
    {
        HandleAim();
        HandleShoot();
        HandleRecoil();
    }
    #endregion

    #region aim
    private void HandleAim()
    {
        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionAim, Time.deltaTime * weaponStats.speedAim);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, position, Time.deltaTime * weaponStats.speedAim);
        }
    }
    #endregion

    #region shoot
    private void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            recoilTime = 1f;
            GameObject bullet = GameManager.Instance.GetPlayerBullet();
            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;
            RaycastHit hit;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, weaponStats.range))
            {
                Debug.Log("Pegaste a esto: " + hit.transform.name);

                EnemyController enemyShocked = hit.transform.GetComponentInParent<EnemyController>();
                if (enemyShocked != null)
                {
                    enemyShocked.TakeDamage(weaponStats.damage);
                }

                Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                OnEnemyHit?.Invoke();
            }
        }
    }
    #endregion

    #region recoil
    private void HandleRecoil()
    {
        if (recoilTime > 0f)
        {
            Quaternion targetRetroceso = Quaternion.Euler(recoilRotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRetroceso, Time.deltaTime * weaponStats.recoilSpeed);
            recoilTime -= Time.deltaTime * weaponStats.returnSpeed;
        }
        else
        {
            Quaternion targetNormal = Quaternion.Euler(rotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetNormal, Time.deltaTime * weaponStats.returnSpeed);
        }
    }
    #endregion
}