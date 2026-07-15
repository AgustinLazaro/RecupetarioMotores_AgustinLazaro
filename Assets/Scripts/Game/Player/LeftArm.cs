using UnityEngine;

public class LeftArm : MonoBehaviour
{
    #region Variables
    [Header("Configuración")]
    [SerializeField] private Vector3 startRotation;
    [SerializeField] private Vector3 upRotation;
    [SerializeField] private float speed = 5f;
    [SerializeField] private KeyCode actionKey = KeyCode.Q;

    [Header("control")]
    [SerializeField] private bool isPlayerControlled = true;
    [SerializeField] private bool isArmUp = false;
    #endregion

    #region Ciclos de Vida
    private void Update()
    {
        if (isPlayerControlled)
        {
            InputControl();
        }
        HandleRotation();
    }
    #endregion

    #region Input 

    private void InputControl()
    {
        if (Input.GetKeyDown(actionKey))
        {
            isArmUp = !isArmUp;
        }
    }

    #endregion

    #region Lógica Movimiento
    private void HandleRotation()
    {
        Quaternion targetRotation = isArmUp ? Quaternion.Euler(upRotation) : Quaternion.Euler(startRotation);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * speed);
    }

    public void ActivateAction()
    {
        isArmUp = true;
    }
    #endregion
}