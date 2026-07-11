using UnityEngine;
public class Character : MonoBehaviour
{
    [Header("Base data (Heredados)")]
    [SerializeField] protected CharacterStats MyStats;

    [Header("Reference base (Heredadas)")]
    [SerializeField] protected Rigidbody rb;
    [SerializeField] protected Animator animator;

    [Header("Deteccion piso (Heredadas")]
    [SerializeField] protected float rayDistance = 1.2f;
    protected bool isGrounded;
    protected float currentHealth;

    protected virtual void Start()
    {
        currentHealth = MyStats.maxHealth;
    }

    //movimineto fisico
    protected void ExecuteMovement(Vector3 direction, float speed, bool run, float inputX, float inputZ)
    {
        
        rb.linearVelocity = new Vector3(direction.x * speed, rb.linearVelocity.y, direction.z * speed);
        animator.SetBool("isSprinting", run);
        animator.SetFloat("InputX", inputX);
        animator.SetFloat("InputY", inputZ);
    }


    //Salto
    protected void ExecuteJump(float jumpForce)
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

    }

    //Detecccion de piso
    protected void CheckGroundStatus()
    {
        
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, rayDistance);
        animator.SetBool("Grounded", isGrounded);
    }

    // Función compartida para que el cuerpo se agache
    protected void ExecuteCrouch(bool isCrouching)
    { 
        animator.SetBool("isCrouching", isCrouching);
    }

    public virtual void TakeDamage(float damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Die();
        }
        
    }
    protected virtual void Die()
    {

    }
}
