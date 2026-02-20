using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;

    // Variables not subject to change, but need to be called upon
    [HideInInspector] public Vector2 playerMovement;
    [HideInInspector] public bool inControl;
    [HideInInspector] public bool isSprinting;
    public float sprintTimer;

    // Variables subject to change in inspector
    public float walkSpeed;
    public float sprintSpeed;
    public float playerSpeed;

    void Start()
    {
        inControl = true;
    }

    void Update()
    {
        if (inControl)
        {
            Movement();
        }
    }

    public void GetInput(Vector2 movement)
    {
        playerMovement = movement;
    }

    void Movement()
    {
        transform.Translate(playerMovement * Time.deltaTime * playerSpeed);
        /*if (playerMovement.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerMovement.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        */
    }

    void Sprint(bool isSprinting)
    {
        if (isSprinting && sprintTimer > 0)
        {
            sprintTimer -= Time.deltaTime;
            playerSpeed += 2;
        }
        {

        }
    }
}
