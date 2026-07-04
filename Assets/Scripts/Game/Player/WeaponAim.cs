using UnityEngine;

public class WeaponAim : MonoBehaviour
{
    [Header("Coordenadas Locales")]
    [SerializeField] private Vector3 position;
    [SerializeField] private Vector3 positionAim;

    [Header("Configuración")]
    [SerializeField] private float speedAim = 8f;
    
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
    }
    
}
