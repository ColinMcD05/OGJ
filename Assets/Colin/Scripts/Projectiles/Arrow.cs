using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public Vector3 targetPosition;
    public int speed = 5;
    void Update()
    {
        Movement();
    }

    void Movement()
    {
        transform.Translate(transform.up * Time.deltaTime * speed, Space.World);
    }
}
