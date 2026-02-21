using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private float leftClamp;
    private float rightClamp;
    private float upClamp;
    private float downClamp;
    private GameObject player;

    void Start()
    {
        player = GameObject.Find("Player");
        Camera.main.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, transform.position.z);
        leftClamp = -(Camera.main.orthographicSize * Camera.main.aspect) * 0.25f + 0.5f + Camera.main.transform.position.x;
        rightClamp = (Camera.main.orthographicSize * Camera.main.aspect) * 0.25f - 0.5f + Camera.main.transform.position.x;
        upClamp = Camera.main.orthographicSize * 0.25f - 0.5f + Camera.main.transform.position.y;
        downClamp = -Camera.main.orthographicSize * 0.25f + 0.5f + Camera.main.transform.position.y;

        // Debug.Log($"{leftClamp} {rightClamp} {upClamp} {downClamp}");
    }

    void LateUpdate()
    {
        MoveCamera();
    }

    void MoveCamera()
    {
        if (player.transform.position.x <= leftClamp || player.transform.position.x >= rightClamp)
        {
            transform.Translate(new Vector2(player.GetComponent<PlayerMovement>().playerMovement.x * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed, 0));
            leftClamp += player.GetComponent<PlayerMovement>().playerMovement.x * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed;
            rightClamp += player.GetComponent<PlayerMovement>().playerMovement.x * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed;
            // Debug.Log("Past!");
        }
        if (player.transform.position.y <= downClamp || player.transform.position.y >= upClamp)
        {
            transform.Translate(new Vector2(0, player.GetComponent<PlayerMovement>().playerMovement.y * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed));
            upClamp += player.GetComponent<PlayerMovement>().playerMovement.y * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed;
            downClamp += player.GetComponent<PlayerMovement>().playerMovement.y * Time.deltaTime * player.GetComponent<PlayerMovement>().playerSpeed;
        }
    }
}
