using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 playerMovement;
    public bool inControl;
    public float playerSpeed;

    void Update()
    {
        GetInput();
        Movement();
    }

    void GetInput()
    {
        playerMovement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void Movement()
    {
        transform.Translate(playerMovement * Time.deltaTime * playerSpeed);
    }
}
