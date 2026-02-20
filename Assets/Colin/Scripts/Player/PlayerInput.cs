using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference sprintAction;
    [SerializeField] private InputActionReference pickUpAction;

    void OnEnable()
    {
        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;

        sprintAction.action.performed += OnSprintPerformed;

        attackAction.action.performed += OnAttackPerformed;
        attackAction.action.canceled += OnAttackCanceled;

        pickUpAction.action.performed += OnPickUpPerformed;
        pickUpAction.action.canceled += OnPickUpCanceled;
    }

    void OnDisable()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.canceled -= OnMoveCanceled;

        sprintAction.action.performed -= OnSprintPerformed;

        attackAction.action.performed -= OnAttackPerformed;
        attackAction.action.canceled -= OnAttackCanceled;

        pickUpAction.action.performed -= OnPickUpPerformed;
        pickUpAction.action.canceled -= OnPickUpCanceled;
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

    private void OnSprintPerformed(InputAction.CallbackContext context)
    {
        playerMovement.isSprinting = !playerMovement.isSprinting;
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("No Attack!");
    }

    void OnPickUpPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Picked Start");
    }

    void OnPickUpCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Picked End");
    }
}
