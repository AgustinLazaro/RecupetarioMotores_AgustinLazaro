using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Collider))]
public class EnemyController : Character
{
    [Header("Referencias")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private Transform enemyEyes;
    [SerializeField] private Transform enemyFirePoint;
    [SerializeField] private Image healthBarFill;
    [SerializeField] private SphereCollider detectionCollider;

    [Header("Estados")]
    [SerializeField] private EnemyState currentState = EnemyState.Idle;

    [Header("Combate")]
    [SerializeField] private float detectionRange = 5f;
    [SerializeField] private float aimingTime = 1.5f;
    [SerializeField] private float attackCooldown = 1f;

    [Header("Accion de Muerte")]
    [SerializeField] private LeftArm leftArmScript;
    [SerializeField] private float deathDelay = 2.0f;

    private float nextAttackTime;
    private float aimingTimer;
    #region ciclos de vida
    protected override void Start()
    {
        base.Start();
        InitHealth();
    }
    protected override void Update()
    {
        base.Update();
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
        if (Vector3.Distance(transform.position, playerTarget.position) <= detectionRange)
        {
            currentState = EnemyState.Aiming;
            aimingTimer = 0f;
        }
    }
    private void StateAiming()
    {
        FaceTarget();

        if (Vector3.Distance(transform.position, playerTarget.position) > detectionRange || !CanSeePlayer())
        {
            currentState = EnemyState.Tracking;
            aimingTimer = 0f;
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

        if (Vector3.Distance(transform.position, playerTarget.position) > detectionRange)
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
            GameObject visualBullet = GameManager.Instance.GetEnemyBullet();
            visualBullet.transform.position = enemyFirePoint.position;
            visualBullet.transform.rotation = Quaternion.LookRotation(shootDirection);

            if (Physics.Raycast(enemyFirePoint.position, shootDirection, out RaycastHit hit, detectionRange))
            {
                if (IsPlayer(hit.collider))
                {
                    if (hit.collider.transform.root.TryGetComponent<Character>(out Character playerCharacter))
                    {
                        playerCharacter.TakeDamage(MyStats.damage);
                        Debug.Log("enemy disparo y saco " + MyStats.damage + " de vida");
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
        OnHealthChanged();
    }
    protected override void Die()
    {
        leftArmScript.ActivateAction();
        GetComponent<Collider>().enabled = false;
        Destroy(gameObject, deathDelay);
    }
    protected override void OnHealthChanged()
    {
        base.OnHealthChanged();
        UpdateHealthBar();
    }
    private void UpdateHealthBar()
    {
        healthBarFill.fillAmount = currentHealth / MyStats.maxHealth;
    }
    #endregion
}