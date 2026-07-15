using System.Collections.Generic;
using UnityEngine;
using System;

public class WeaponController : MonoBehaviour
{
    public static event Action OnEnemyHit;

    [Header("Object pool")]
    [SerializeField] private int poolSize = 20;
    private Queue<GameObject> bulletPool = new Queue<GameObject>();

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

    //pool
    void Start()
    {
        //(inicio ; condición ; paso)
        for (int i = 0; i <poolSize; i++)
        {
            GameObject obj = Instantiate(weaponStats.bulletPrefab);
            obj.SetActive(false);
            bulletPool.Enqueue(obj);
        }
    }
    void Update()
    {
        HandleAim();
        HandleShoot();
        HandleRecoil();
    }
    //aim
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
    //shoot
    private void HandleShoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            recoilTime = 1f;

            GameObject bullet = bulletPool.Dequeue();

            bullet.transform.position = firePoint.position;
            bullet.transform.rotation = firePoint.rotation;

            bullet.SetActive(true);

            bulletPool.Enqueue(bullet);

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
    //recoil
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
}

