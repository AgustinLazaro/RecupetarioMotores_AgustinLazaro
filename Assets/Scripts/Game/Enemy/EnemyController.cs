using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Character
{
    [Header("Referencias")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform enemyEyes;
    [SerializeField] private Transform enemyFirePoint;
    [SerializeField] private Image healthBarFill;

    [Header("Estados")]
    [SerializeField] private EnemyState currentState = EnemyState.Idle;

    [Header("Combate")]
    [SerializeField] private float attackRange = 2f;
    [SerializeField] private float aimingTime = 1.5f;
    [SerializeField] private float attackCooldown = 1f;

    private float nextAttackTime;
    private float aimingTimer;

    #region ciclos de vida
    protected override void Start()
    {
        base.Start();
        InitHealth();
    }
    private void Update()
    {
        HandleStateMachine();
    }
    private void OnTriggerStay(Collider other)
    {
        HandleDetection(other);
    }
    private void OnTriggerExit(Collider other)
    {
        HandleLostDetection(other);
    }
    #endregion

    #region maquina estados
    private void HandleStateMachine()
    {
        switch (currentState)
        {
            case EnemyState.Idle:
                StateIdle();
                break;
            case EnemyState.Tracking:
                StateTracking();
                break;
            case EnemyState.Aiming:
                StateAiming();
                break;
            case EnemyState.Attack:
                StateAttack();
                break;
        }
    }
    private void StateIdle()
    {
    }
    private void StateTracking()
    {
        FaceTarget();
        if (Vector3.Distance(transform.position, playerTarget.position) <= attackRange)
        {
            currentState = EnemyState.Aiming;
            aimingTimer = 0f;
        }
    }
    private void StateAiming()
    {
        FaceTarget();

        
        if (Vector3.Distance(transform.position, playerTarget.position) > attackRange)
        {
            currentState = EnemyState.Tracking;
            return;
        }
        aimingTimer += Time.deltaTime;

        if (aimingTimer >= aimingTime)
        {
            currentState = EnemyState.Attack;
            nextAttackTime = Time.time;
        }
    }
    private void StateAttack()
    {
        FaceTarget();

        if (Vector3.Distance(transform.position, playerTarget.position) > attackRange)
        {
            currentState = EnemyState.Tracking;
        }
        else
        {
            HandleAttack();
        }
    }
    #endregion

    #region ataque
    private void HandleAttack()
    {
        if (Time.time >= nextAttackTime)
        {
            Vector3 shootDirection = (playerTarget.position - enemyFirePoint.position).normalized;

            if (Physics.Raycast(enemyFirePoint.position, shootDirection, out RaycastHit hit, attackRange))
            {
                if (IsPlayer(hit.collider))
                {
                    if (hit.collider.transform.root.TryGetComponent<Character>(out Character playerCharacter))
                    {
                        playerCharacter.TakeDamage(MyStats.damage);
                        Debug.Log("¡El enemigo disparó y te sacó " + MyStats.damage + " de vida!");
                    }
                }
                Debug.DrawRay(enemyFirePoint.position, shootDirection * hit.distance, Color.yellow, 0.5f);
            }
            nextAttackTime = Time.time + attackCooldown;
        }
    }
    #endregion

    #region deteccion
    private void HandleDetection(Collider other)
    {
        if (IsPlayer(other))
        {
            if (CanSeePlayer())
            {
                if (currentState == EnemyState.Idle) currentState = EnemyState.Tracking;
            }
            else
            {
                currentState = EnemyState.Idle;
            }
        }
    }
    private void HandleLostDetection(Collider other)
    {
        if (IsPlayer(other))
        {
            currentState = EnemyState.Idle;
        }
    }
    #endregion

    #region vision y rotacion 
    private bool CanSeePlayer()
    {
        Vector3 rayOrigin = enemyEyes.position;
        Vector3 directionToPlayer = (playerTarget.position - rayOrigin).normalized;

        if (Physics.Raycast(rayOrigin, directionToPlayer, out RaycastHit hit, MyStats.viewDistance))
        {
            if (IsPlayer(hit.collider))
            {
                Debug.DrawRay(rayOrigin, directionToPlayer * hit.distance, Color.green);
                return true;
            }
        }

        Debug.DrawRay(rayOrigin, directionToPlayer * MyStats.viewDistance, Color.red);
        return false;
    }
    private void FaceTarget()
    {
        Vector3 direction = playerTarget.position - transform.position;
        direction.y = 0;

        Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, MyStats.rotationSpeed * Time.deltaTime);
    }
    private bool IsPlayer(Collider other)
    {
        return other.transform.root == playerTarget.root;
    }
    #endregion

    #region health system
    private void InitHealth()
    {
        UpdateHealthBar();
    }
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
    #endregion
}