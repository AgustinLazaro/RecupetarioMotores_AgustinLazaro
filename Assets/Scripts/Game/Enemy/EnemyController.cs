using UnityEngine;
using UnityEngine.UI;
public class EnemyController : Character
{
    [Header("Referencias")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform enemyEyes;
    [SerializeField] private Image healthBarFill;

    [Header("Estados")]
    [SerializeField] private EnemyState currentState = EnemyState.Idle;

    protected override void Start()
    {
        base.Start();
        UpdateHealthBar();
    }
    private void Update()
    {
        switch (currentState)
        {
            case EnemyState.Chase:
                FaceTarget();
                break;
            case EnemyState.Idle:
                break;
        }
    }

    // Inputs
    private void OnTriggerStay(Collider other)
    {
        if (IsPlayer(other))
        {
            //ternario: Lo veo? Chase. No lo veo? Idle.
            currentState = CanSeePlayer() ? EnemyState.Chase : EnemyState.Idle;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            currentState = EnemyState.Idle;
        }
    }

    private bool IsPlayer(Collider other)
    {
        return other.transform.root == playerTarget.root;
    }

    //vision 
    private bool CanSeePlayer()
    {
        Vector3 rayOrigin = enemyEyes.position;
        Vector3 directionToPlayer = (playerTarget.position - rayOrigin).normalized;

        if (Physics.Raycast(rayOrigin, directionToPlayer, out RaycastHit hit, MyStats.viewDistance))
        {
            if (IsPlayer(hit.collider)) // DRY aplicado
            {
                Debug.DrawRay(rayOrigin, directionToPlayer * hit.distance, Color.green);
                return true;
            }
        }

        Debug.DrawRay(rayOrigin, directionToPlayer * MyStats.viewDistance, Color.red);
        return false;
    }

    // acciones
    private void FaceTarget()
    {
        // 1. calculo direccion
        Vector3 direction = playerTarget.position - transform.position;

        // 2. Anulo eje Y
        direction.y = 0;

        // 3. Rotacion
        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, MyStats.rotationSpeed * Time.deltaTime);
    }

    // health system
    public override void TakeDamage(float damageAmount)
    {
        base.TakeDamage(damageAmount);
        UpdateHealthBar();
    }

    protected override void Die()
    {
        
        Destroy(gameObject);
    }
    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / MyStats.maxHealth;
    }
}