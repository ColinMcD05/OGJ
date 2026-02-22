using Unity.VisualScripting;
using UnityEngine;

namespace Jack
{
    public class PlayerMovementScript : MonoBehaviour
    {
        Vector2 playerDir;       
        [SerializeField] float playerSpeed=5f;

        void Update()
        {
            Movement();
        }
    
        public void GetInput(Vector2 playerInput)
        {
            playerDir = playerInput;
        }

        void Movement() =>
            transform.Translate(playerDir*playerSpeed*Time.deltaTime);
    }
}
