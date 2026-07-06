using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform enemyEyes;
    [SerializeField] private Animator animator;

    [Header("Visión ")]
    [SerializeField] private float viewDistance = 20f;

    [Header("Movimiento & Rotación")]
    [SerializeField] private float rotationSpeed = 5f;


    [Header("Estados")]
    [SerializeField] private bool WatchingPlayer;


    void Start()
    {
        
    }

    void Update()
    {
       // Vector3 targetPosition = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
        //transform.LookAt(targetPosition);
        CheckLineOfSight();
    }
    private void CheckLineOfSight()
    {
        Vector3 rayOrigin = enemyEyes.position;
        Vector3 rayTarget = playerTarget.position;
        Vector3 directionToPlayer = (rayTarget - rayOrigin).normalized;

        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, directionToPlayer, out hit, viewDistance))
        {
            if (hit.transform.root == playerTarget.root)
            {
                
                WatchingPlayer = true;
                Debug.DrawRay(rayOrigin, directionToPlayer * hit.distance, Color.green);

                
                Vector3 lookPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                
                Vector3 lookDirection = (lookPosition - transform.position).normalized;

                
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                WatchingPlayer = false;
                Debug.DrawRay(rayOrigin, directionToPlayer * hit.distance, Color.red);
            }
        }
        else
        {
            WatchingPlayer = false;
            Debug.DrawRay(rayOrigin, directionToPlayer * viewDistance, Color.red);
        }
    }
}

