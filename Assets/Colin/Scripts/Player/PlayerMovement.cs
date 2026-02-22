using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] SpriteRenderer spriteRenderer;
    [SerializeField] Rigidbody2D playerRigidbody;
    Animator playerAnim;

    // Variables not subject to change, but need to be called upon
    public Vector2 playerDir;
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
        playerAnim = GetComponent<Animator>();
        sprintTimer = stamina;
        playerSpeed = walkSpeed;
        inControl = true;
    }

    void OnEnable()
    {
        PlayerInput.onSprint += Sprint;
    }

    void OnDisable()
    {
        PlayerInput.onSprint -= Sprint;
    }

    void Update()
    {
        if (inControl)
        {
            Movement();
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
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void GetInput(Vector2 playerInput)
    {
        playerDir = playerInput;
        if (playerDir.x < 0) playerAnim.Play("WalkWest");
        else if (playerDir.x > 0) playerAnim.Play("WalkEast");
        else if (playerDir.y < 0) playerAnim.Play("WalkSouth");
        else if (playerDir.y > 0) playerAnim.Play("WalkNorth");
        else playerAnim.Play("Idle");
    }

    void Movement() =>
        transform.Translate(playerDir * playerSpeed * Time.deltaTime);

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
}
