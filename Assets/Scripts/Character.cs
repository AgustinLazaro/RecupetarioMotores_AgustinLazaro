using UnityEngine;

public class Character : MonoBehaviour
{
    [Header("Base data (Heredados)")]
    [SerializeField] protected CharacterStats MyStats;

    [Header("Reference base (Heredadas)")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator animator;

    [Header("Detección piso (Heredadas)")]
    [SerializeField] protected float rayDistance = 1.2f;
    protected bool isGrounded;

    protected float currentHealth;
    protected float lastDamageTime;

    #region ciclos de vida
    protected virtual void Start()
    {
        currentHealth = MyStats.maxHealth;
    }
    protected virtual void Update()
    {
        HandleRegeneration();
    }
    #endregion

    #region movimiento y fisicas
    protected void ExecuteMovement(Vector3 direction, float speed, bool run, float inputX, float inputZ)
    {
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);
        animator.SetBool("isSprinting", run);
        animator.SetFloat("InputX", inputX);
        animator.SetFloat("InputY", inputZ);
    }

    protected void ExecuteJump(float jumpForce)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    protected void ExecuteCrouch(bool isCrouching)
    {
        animator.SetBool("isCrouching", isCrouching);
    }

    protected void CheckGroundStatus()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, rayDistance);
        animator.SetBool("Grounded", isGrounded);
    }
    #endregion

    #region health system y damage
    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;
        lastDamageTime = Time.time;

        OnHealthChanged();

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
    }
    protected virtual void HandleRegeneration()
    {
        if (currentHealth < MyStats.maxHealth && Time.time > lastDamageTime + MyStats.regenerationDelay)
        {
            currentHealth += MyStats.regenerationRate * Time.deltaTime;
            currentHealth = Mathf.Clamp(currentHealth, 0, MyStats.maxHealth);
            OnHealthChanged();
        }
    }
    protected virtual void OnHealthChanged()
    {

    }
    protected virtual void Die()
    {

    }
    #endregion
}
