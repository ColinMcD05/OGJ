using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 playerMovement;
    public bool inControl;
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
    }
}
