using UnityEngine;

public class PlayerController : Character
{
    [Header("Cosas únicas del Player")]
    [SerializeField] private Transform cameraTransform;

    [Header("Ajustes de Cámara")]
    [SerializeField] private float mouseSensitivity = 2f;
    private float xRotation = 0f;

    [Header("Keys")]
    [SerializeField] private KeyCode crouchKey = KeyCode.LeftControl;
    [SerializeField] private KeyCode sprintKey = KeyCode.LeftShift;

    [Header("Input Strings")]
    private const string AXIS_HORIZONTAL = "Horizontal";
    private const string AXIS_VERTICAL = "Vertical";
    private const string BUTTON_JUMP = "Jump";
    private const string MOUSE_X = "Mouse X";
    private const string MOUSE_Y = "Mouse Y";

    void Update()
    {
        HideCursor();
        ManageCamera();
        CheckGroundStatus(); // Heredado
        ManageJump();
        ManageCrouch();
        ManageMovement();

    }


    private void HideCursor()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    private void ManageCamera()
    {
        float mouseX = Input.GetAxis(MOUSE_X) * mouseSensitivity;
        float mouseY = Input.GetAxis(MOUSE_Y) * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }


    private void ManageJump()
    {
        if (Input.GetButtonDown(BUTTON_JUMP) && isGrounded)
        {
            ExecuteJump(MyStats.jumpForce);
        }
    }

    private void ManageCrouch()
    {
        
        if (Input.GetKeyDown(crouchKey) || Input.GetKeyUp(crouchKey)) //OR
        {
            bool isCrouching = Input.GetKey(crouchKey);

            float targetHeight = isCrouching ? MyStats.crouchHeight : MyStats.standHeight;//tenario

            cameraTransform.localPosition = new Vector3(cameraTransform.localPosition.x, targetHeight, cameraTransform.localPosition.z);
            ExecuteCrouch(isCrouching);
        }
    }

    private void ManageMovement()
    {
        float x = Input.GetAxis(AXIS_HORIZONTAL);
        float z = Input.GetAxis(AXIS_VERTICAL);

        //Remplazo de variables de controles en vez hardcodeo de tecla 
        bool isSprinting = Input.GetKey(sprintKey);

        if (Input.GetKey(crouchKey))
        {
            isSprinting = false;
        }

        float currentSpeed = isSprinting ? MyStats.runSpeed : MyStats.walkSpeed;//Ternario

        //movimineto fisico heredada de character
        Vector3 moveDirection = transform.right * x + transform.forward * z;
        ExecuteMovement(moveDirection, currentSpeed, isSprinting, x, z);
    }
}

