using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

namespace Jack
{
    public class WeaponController : MonoBehaviour
    {
        Animator playerWeaponAnim;
        [SerializeField] Weapon hitBox;
        bool canAttack=true;
        [SerializeField] float cooldown=.5f;

        void OnEnable()
        {
            PlayerController.equippedWeapon += Equip;
            PlayerInputScript.onAttack += Attack;
        }

        void OnDisable()
        {
            PlayerController.equippedWeapon -= Equip;
            PlayerInputScript.onAttack -= Attack;
        }

        void Start()
        {
            playerWeaponAnim = transform.GetChild(0).GetComponent<Animator>(); // This is a terrible assumption (!)
        }

        void Equip(Weapon activeWeapon)
        {
            // hitBox = activeWeapon;
            if(activeWeapon==null)
            {
                playerWeaponAnim.gameObject.SetActive(false);
                return;
            }

            HardCopyWeapon(activeWeapon);
            // // Set the properties of the active weapon
            // SpriteRenderer activeSR = hitBox.GetComponent<SpriteRenderer>()
            //     ?? hitBox.AddComponent<SpriteRenderer>();
            // activeSR.sprite = activeWeapon.sr.sprite;
            // Rigidbody2D activeRB = hitBox.GetComponent<Rigidbody2D>()
            //     ?? hitBox.AddComponent<Rigidbody2D>();
            // HardCopyRB(activeRB,activeWeapon.rb);
        }

        void HardCopyWeapon(Weapon activeWeapon)
        {
            playerWeaponAnim.gameObject.SetActive(true);
            hitBox.sr.sprite = activeWeapon.sr.sprite;
            hitBox.sr.color = activeWeapon.sr.color;
            HardCopyRB(hitBox.rb,activeWeapon.rb);
            HardCopyBC(hitBox.bc,activeWeapon.bc);
        }

        void HardCopyRB(Rigidbody2D activeRB,Rigidbody2D activeWRB)
        {
            activeRB.mass = activeWRB.mass;
            activeRB.gravityScale = activeWRB.gravityScale;
            activeRB.linearDamping = activeWRB.linearDamping;
            activeRB.angularDamping = activeWRB.angularDamping;
            activeRB.constraints = activeWRB.constraints;
        }

        void HardCopyBC(BoxCollider2D activeBC,BoxCollider2D activeWBC)
        {
            activeBC.size = activeWBC.size;
            activeBC.offset = activeWBC.offset;
            activeBC.isTrigger = activeWBC.isTrigger;
            activeBC.compositeOperation = activeWBC.compositeOperation;
            activeBC.usedByEffector = activeWBC.usedByEffector;
            activeBC.autoTiling = activeWBC.autoTiling;
        }

        // void Attack(Vector2 direction,float cooldown)
        void Attack(Vector2 direction)
        {
            if(!canAttack) return;
            // StartCoroutine(AttackRoutine(direction,cooldown));
            StartCoroutine(AttackRoutine(direction));
        }

        // IEnumerator AttackRoutine(Vector2 direction, float cooldown)
        IEnumerator AttackRoutine(Vector2 direction)
        {
            canAttack=false;

            // Lock the Direction of the attack
            if(direction.x>0) transform.eulerAngles = new Vector3(0,0,-90f);
            else if(direction.x<0) transform.eulerAngles = new Vector3(0,0,90f);
            else if(direction.y<0) transform.eulerAngles = new Vector3(0,0,180f);
            else if(direction.y>0) transform.eulerAngles = Vector3.zero;
            
            playerWeaponAnim.Play("Swing",0,0f);
            yield return new WaitForSeconds(cooldown);
            canAttack=true;
        }
    }
}