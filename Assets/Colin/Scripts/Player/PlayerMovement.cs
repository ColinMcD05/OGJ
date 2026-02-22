using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D playerRigidbody;

    // Variables not subject to change, but need to be called upon
    [HideInInspector] public Vector2 playerMovement;
    [HideInInspector] public bool inControl;
    [HideInInspector] public bool isSprinting;
    private float sprintTimer;
    [HideInInspector] public float playerSpeed;
    [HideInInspector] public bool isSlimed;
    [SerializeField] Sprite[] sprite;

    // Variables subject to change in inspector
    public float walkSpeed;
    public float sprintSpeed;
    public float stamina;

    void Start()
    {
        sprintTimer = stamina;
        playerSpeed = walkSpeed;
        inControl = true;
    }

    void Update()
    {
        if (inControl)
        {
            Movement();
            ChangeAnimator();
        }
        if (isSprinting && sprintTimer > 0)
        {
            sprintTimer -= Time.deltaTime;
        }
        else if (!isSprinting && sprintTimer <= stamina)
        {
            sprintTimer += Time.deltaTime * 3;
        }
        else if(sprintTimer <= 0)
        {
            Sprint();
        }
    }

    public void GetInput(Vector2 movement)
    {
        playerMovement = movement;
    }

    void Movement()
    {
        transform.Translate(playerMovement * Time.deltaTime * playerSpeed);
    }

    public void Sprint()
    {
        if (!isSlimed)
        {
            isSprinting = !isSprinting;
            if (playerSpeed == walkSpeed)
            {
                playerSpeed = sprintSpeed;
            }
            else
            {
                playerSpeed = walkSpeed;
            }
        }
    }

    public void Slimed()
    {
        isSlimed = true;
        playerSpeed = playerSpeed * 0.75f;
        Invoke("Cleaned", 5);
    }

    private void Cleaned()
    {
        playerSpeed = walkSpeed;
        isSlimed = false;
    }

    private void ChangeAnimator()
    {
        if (playerMovement.x > 0.1 || playerMovement.x < -0.1)
        {
            spriteRenderer.sprite = sprite[1];
            if (playerMovement.x > 0.01)
            {
                spriteRenderer.flipX = false;
            }
            else
            {
                spriteRenderer.flipX = true;
            }
        }
        else if (playerMovement.y > 0.01)
        {
            spriteRenderer.sprite = sprite[2];
        }
        else if (playerMovement.y < -0.01)
        {
            spriteRenderer.sprite = sprite[0];
        }
    }
}
