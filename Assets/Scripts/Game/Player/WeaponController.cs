using UnityEngine;
using UnityEngine.UI;

public class WeaponController : MonoBehaviour
{
    [Header("Referencias Disparo")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private float range = 100f;
    [SerializeField] private GameObject bulletPrefab;

    [Header("UI y Hitmarker")]
    [SerializeField] private GameObject hitmarkerImage;
    [SerializeField] private float hitmarkerDuration = 0.5f;
    private float hitmarkerTimer = 0f;

    [Header("Coordenadas Locales Aim")]
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 positionAim;
    [Header("Coordenadas Locales Recoil")]
    [SerializeField] private Vector3 rotation;
    [SerializeField] private Vector3 recoilRotation;

    [Header("Configuración")]
    [SerializeField] private float speedAim = 8f;
    [SerializeField] private float returnSpeed = 5f;
    [SerializeField] private float recoilTime = 1f;
    [SerializeField] private float recoilSpeed = 20f;



    void Start()
    {
        
    }


    void Update()
    {
        
        if (Input.GetMouseButton(1))
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, positionAim, Time.deltaTime * speedAim);
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, position, Time.deltaTime * speedAim);
        }

    
        if (Input.GetMouseButtonDown(0))
        {
            recoilTime = 1f;


            RaycastHit hit;
            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, range))
            {
                Debug.Log("Pegaste a : " + hit.transform.name);

                Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);

                hitmarkerImage.SetActive(true);
                hitmarkerTimer = hitmarkerDuration;

            }
        }

      
        if (hitmarkerTimer > 0)
        {
            hitmarkerTimer -= Time.deltaTime;
            if (hitmarkerTimer <= 0)
            {
                
                hitmarkerImage.SetActive(false);
            }
        }

        
        if (recoilTime > 0f)
        {
            Quaternion targetRetroceso = Quaternion.Euler(recoilRotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRetroceso, Time.deltaTime * recoilSpeed);

            recoilTime -= Time.deltaTime * returnSpeed;
        }
        else
        {
            Quaternion targetNormal = Quaternion.Euler(rotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetNormal, Time.deltaTime * returnSpeed);
        }
    }
}


