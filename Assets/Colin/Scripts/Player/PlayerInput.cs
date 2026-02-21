using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerMovement playerMovement;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference sprintAction;

    void OnEnable()
    {
        moveAction.action.performed += OnMovePerformed;
        moveAction.action.canceled += OnMoveCanceled;

        sprintAction.action.performed += OnSprintPerformed;

        attackAction.action.performed += OnAttackPerformed;
        attackAction.action.canceled += OnAttackCanceled;
    }

    void OnDisable()
    {
        moveAction.action.performed -= OnMovePerformed;
        moveAction.action.canceled -= OnMoveCanceled;

        sprintAction.action.performed -= OnSprintPerformed;

        attackAction.action.performed -= OnAttackPerformed;
        attackAction.action.canceled -= OnAttackCanceled;
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

    // Player Input Callback(s)
    public void OnToggleTempWeapon(InputValue value)
    {
        playerController.activeTWeapon++;
        playerController.activeTWeapon%=TempWeapon.TempEnumCount;
        Debug.Log("Toggle TempWeapon: " + playerController.activeTWeapon);
    }

    public void OnTogglePermWeapon(InputValue value)
    {
        playerController.activePWeapon++;
        playerController.activePWeapon%=PermWeapon.PermEnumCount;
        Debug.Log("Toggle PermWeapon: " + playerController.activePWeapon);
    }
}