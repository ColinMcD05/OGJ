using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] PlayerController playerController;
    [SerializeField] PlayerMovement playerMovement;
    [SerializeField] WeaponControllerScript weaponControllerScript;

    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference sprintAction;

    [SerializeField] float cooldown=.5f; // Hardcoded (!)
    bool canAttack = true;

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
        playerMovement.Sprint();
    }

    private void OnAttackPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Attack!");
        if(!canAttack) return;
        StartCoroutine(AttackRoutine());
            // weaponControllerScript.anim.Play("Swing",0,0f);
        // weaponControllerScript.anim.speed = 1f;
    }

    private void OnAttackCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("No Attack!");
        // weaponControllerScript.anim.speed = 0f;
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

    IEnumerator AttackRoutine()
    {
        canAttack = false;


        weaponControllerScript.anim.Play("Swing",0,0f);

        yield return new WaitForSeconds(cooldown);

        canAttack = true;
    }
}