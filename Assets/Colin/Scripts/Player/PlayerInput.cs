using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] private InputActionReference moveAction;

    void Awake()
    {
        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;
    }

    private void OnMovePerformed(InputAction.CallbackContext context)
    {
        Vector2 movementInput = context.ReadValue<Vector2>();
        playerMovement.GetInput(movementInput);
    }

    private void OnMoveCanceled(InputAction.CallbackContext context)
    {
        playerMovement.GetInput(Vector2.zero);
    }
}
