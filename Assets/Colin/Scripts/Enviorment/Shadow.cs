using System;
using UnityEngine;

public class Shadow : MonoBehaviour
{
    [Range(0, 1)] public float fade;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().inShadow = true;
            collision.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, fade);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerController>().inShadow = false;
            collision.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        }
    }
}
