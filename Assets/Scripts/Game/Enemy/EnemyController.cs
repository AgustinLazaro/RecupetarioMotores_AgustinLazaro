using System;
using UnityEngine;
using UnityEngine.UI;

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

    [Header("Salud y UI")]
    [SerializeField] private float maxHealth = 100f;
    private float currentHealth;
    [SerializeField] private Image healthBarFill; 

    void Start()
    {
        currentHealth = maxHealth;
        updateHeatlbar();
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.transform.root == playerTarget.root)
        {
            CheckLineOfSight();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root == playerTarget.root)
        {
            WatchingPlayer = false;
        }

    }

    private void CheckLineOfSight()
    {
        // data de raycast
        Vector3 rayOrigin = enemyEyes.position;
        Vector3 rayTarget = playerTarget.position;
        Vector3 directionToPlayer = (rayTarget - rayOrigin).normalized;

        RaycastHit hit;

        //Deteccion rayo
        if (Physics.Raycast(rayOrigin, directionToPlayer, out hit, viewDistance))
        {
            if (hit.transform.root == playerTarget.root)

            {
                //Mira al player y rota con slerp
                WatchingPlayer = true;

                Debug.DrawRay(rayOrigin, directionToPlayer * hit.distance, Color.green);

                
                Vector3 lookPosition = new Vector3(playerTarget.position.x, transform.position.y, playerTarget.position.z);

                
                Vector3 lookDirection = (lookPosition - transform.position).normalized;

                
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);

                
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
            else
            //cobertura
            {
                WatchingPlayer = false;
                Debug.DrawRay(rayOrigin, directionToPlayer * hit.distance, Color.red);
            }
        }
        else
        //fuera de rango
        {
            WatchingPlayer = false;
            Debug.DrawRay(rayOrigin, directionToPlayer * viewDistance, Color.red);
        }
    }


    //Recibe daño
    public void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        
        //la vida no baja de 0
        if (currentHealth < 0)
        {
            currentHealth = 0;
        }
        updateHeatlbar();


        // Lógica de muerte
       // if (currentHealth <= 0)
        //{
       //     Die();
        //}
    }


    //Se actualiza la UI barra de vida u
    private void updateHeatlbar()
    {
        if (healthBarFill != null)
        {

            healthBarFill.fillAmount = currentHealth / maxHealth;
        }
    }


    //Enemigo muere
    private void Die()
    {
        Destroy(gameObject);
    }
}

