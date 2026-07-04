using System.Net.NetworkInformation;
using UnityEngine;

public class WeaponShot : MonoBehaviour
{
    
    public Vector3 rotation;    
    public Vector3 recoilRotation; 
    public float recoilSpeed = 20f; 
    public float returnSpeed= 5f;    
    private float recoilTime = 0f;

    void Start()
    {
        
    }

  
    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            recoilTime = 1f;
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
