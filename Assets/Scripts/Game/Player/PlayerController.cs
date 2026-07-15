using UnityEngine;
using System;

public class PlayerController : Character
{
    [Header("Únicas de Player")]
    [SerializeField] private Transform cameraTransform;

    [Header("Ajustes de Camara")]
    [SerializeField] private float mouseSensitivity = 2f;
    private float xRotation = 0f;

    [Header("Keys")]
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Input Strings")]
    private const string AXIS_horizontal = "Horizontal";
    private const string AXIS_vertical = "Vertical";
    private const string BUTTON_jump = "Jump";
    private const string MOUSE_x = "Mouse X";
    private const string MOUSE_y = "Mouse Y";

    public static event Action<float, float> OnPlayerHealthChanged;

    #region ciclos de vida
    protected override void Start()
    {
        base.Start();
        OnPlayerHealthChanged?.Invoke(currentHealth, MyStats.maxHealth);
    }

    protected override void Update()
    {
        base.Update();
        HideCursor();
        ManageCamera();
        CheckGroundStatus();
        HandleRegeneration();
        ManageJump();
        ManageCrouch();
        ManageMovement();
    }
    #endregion

    #region health system y damage
    public override void TakeDamage(float damageAmount)
    {
        
        base.TakeDamage(damageAmount);
        OnPlayerHealthChanged?.Invoke(currentHealth, MyStats.maxHealth);
    }
    protected override void OnHealthChanged()
    {
        base.OnHealthChanged();
        OnPlayerHealthChanged?.Invoke(currentHealth, MyStats.maxHealth);
    }
    #endregion

    #region cursor
    private void HideCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ManageCamera()
    {
        float mouseX = Input.GetAxis(MOUSE_x) * mouseSensitivity;
        float mouseY = Input.GetAxis(MOUSE_y) * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }
    #endregion

    #region movimiento y fisicas
    private void ManageJump()
    {
        if (Input.GetButtonDown(BUTTON_jump) && isGrounded)
        {
            ExecuteJump(MyStats.jumpForce);
        }
    }

    private void ManageCrouch()
    {
        if (Input.GetKeyDown(crouchKey) || Input.GetKeyUp(crouchKey))
        {
            bool isCrouching = Input.GetKey(crouchKey);
            float targetHeight = isCrouching ? MyStats.crouchHeight : MyStats.standHeight;

            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, targetHeight, cameraTransform.localPosition.z);
            ExecuteCrouch(isCrouching);
        }
    }

    private void ManageMovement()
    {
        float x = Input.GetAxis(AXIS_horizontal);
        float z = Input.GetAxis(AXIS_vertical);

        bool isSprinting = Input.GetKey(sprintKey);

        if (Input.GetKey(crouchKey))
        {
            isSprinting = false;
        }

        float currentSpeed = isSprinting ? MyStats.runSpeed : MyStats.walkSpeed;
        Vector3 moveDirection = transform.right * x + transform.forward * z;

        ExecuteMovement(moveDirection, currentSpeed, isSprinting, x, z);
    }
    #endregion
}
