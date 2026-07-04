using UnityEngine;

public class LeftArm : MonoBehaviour
{
    [SerializeField] private Vector3 StartRotation; 
    [SerializeField] private Vector3 UpRotation; 
    [SerializeField] private float speed = 5f;     


    [SerializeField] private bool isArmUp = false; 
    void Start()
    {
        
    }

    
    void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.Q))
        {
            isArmUp = !isArmUp;
        }

       
        if (isArmUp)
        {
            
            Quaternion targetRotation = Quaternion.Euler(UpRotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * speed);
        }
        else
        {
            
            Quaternion targetRotation = Quaternion.Euler(StartRotation);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * speed);
        }
    }
}