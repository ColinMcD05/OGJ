using Unity.VisualScripting;
using UnityEngine;

namespace Jack
{
    [RequireComponent(typeof(Animator))]
    public class PlayerMovementScript : MonoBehaviour
    {
        Vector2 playerDir;       
        Animator playerAnim;
        [SerializeField] float playerSpeed=5f;

        void Start()
        {
            playerAnim = GetComponent<Animator>();
        }

        void Update()
        {
            Movement();
        }
    
        public void GetInput(Vector2 playerInput)
        {
            playerDir = playerInput;
            if(playerDir.x<0) playerAnim.Play("WalkWest");
            else if(playerDir.x>0) playerAnim.Play("WalkEast");
            else if(playerDir.y<0) playerAnim.Play("WalkSouth");
            else if(playerDir.y>0) playerAnim.Play("WalkNorth");
            else playerAnim.Play("Idle");
        }

        void Movement() =>
            transform.Translate(playerDir*playerSpeed*Time.deltaTime);

    }
}
