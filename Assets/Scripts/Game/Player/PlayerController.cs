using UnityEngine;


public class PlayerController : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Animator animator;

    [Header("Movement")]
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float jumpForce = 2f;

    [Header("Detección de Suelo")]
    [SerializeField] private float rayDistance = 1f;
    [SerializeField] private bool isGrounded;

    [Header("Crouched")]
    [SerializeField] private float standHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;

    [Header("Camera Look")]
    [SerializeField] private float mouseSensitivity = 2f;
    private float xRotation = 0f; 

    void Start()
    {
        
    }

    void Update()
    {
        
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, rayDistance);

        
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = transform.right * x + transform.forward * z;
        rb.linearVelocity = new Vector3(moveDirection.x * walkSpeed, rb.linearVelocity.y, moveDirection.z * walkSpeed);

        
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, 0f, rb.linearVelocity.z);
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        //Crouch
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, crouchHeight, cameraTransform.localPosition.z);
            animator.SetBool("isCrouching", true);
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, standHeight, cameraTransform.localPosition.z);
            animator.SetBool("isCrouching", false);
        }

       
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

       
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); 

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        transform.Rotate(Vector3.up * mouseX);
    }
}

