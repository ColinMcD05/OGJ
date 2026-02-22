using UnityEngine;
using UnityEngine.InputSystem;
using System;

namespace Jack
{
    [RequireComponent(typeof(PlayerController))]
    [RequireComponent(typeof(PlayerMovementScript))]
    public class PlayerInputScript : MonoBehaviour
    {
        PlayerController playerController;
        PlayerMovementScript playerMovement;

        // public static event Action<Vector2,float> onAttack;
        public static event Action<Vector2> onAttack;
        public static event Action<TempWeapon.Temporary,int> onTempToggle;

        Vector2 playerDir = new Vector2(0,1);


        void Start()
        {
            playerController = GetComponent<PlayerController>();
            playerMovement = GetComponent<PlayerMovementScript>();
        }


        // Player Input Callback(s)
        public void OnMove(InputValue value)
        {
            playerDir = value.Get<Vector2>();
            playerMovement.GetInput(playerDir);
        }

        public void OnAttack(InputValue value)
        {
            // float cooldown = playerController.tWeapons[0][0]._cooldown; // (!)
            // onAttack?.Invoke(value.Get<Vector2>(),cooldown);
            onAttack?.Invoke(playerDir);
        }

        public void OnToggleTempWeapon(InputValue value)
        {
            int nextIdx = ((int)playerController.activeWeaponIdx+1)%TempWeapon.TempEnumCount;
            // playerController.activeWeaponIdx=(TempWeapon.Temporary)nextIdx;
            onTempToggle?.Invoke((TempWeapon.Temporary)nextIdx,0); // "0" is an artifact of SwitchTempWeapon
        }
    }
}
