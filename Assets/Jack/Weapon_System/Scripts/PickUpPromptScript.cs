using UnityEngine;

public class PickUpPromptScript : MonoBehaviour
{
    [SerializeField] GameObject promptUI;

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            promptUI.SetActive(true);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            promptUI.SetActive(false);
        }
    }
}
