using System;
using System.Collections;
using UnityEngine;

namespace Jack
{
    public class WeaponController : MonoBehaviour
    {
        Animator playerWeaponAnim;
        [SerializeField] AudioClip swordAudio;
        [SerializeField] AudioClip bowAudio;
        [SerializeField] AudioClip clubAudio;
        AudioSource audioSource;
        [SerializeField] TempWeapon hitBox;
        bool canAttack=true;
        [SerializeField] float cooldown=.5f;

        void OnEnable()
        {
            PlayerController.equippedWeapon += Equip;
            PlayerInput.onAttack += Attack;
        }

        void OnDisable()
        {
            PlayerController.equippedWeapon -= Equip;
            PlayerInput.onAttack -= Attack;
        }

        void Start()
        {
            playerWeaponAnim = transform.GetChild(0).GetComponent<Animator>(); // This is a terrible assumption (!)
            audioSource = GetComponent<AudioSource>();
        }

        void Equip(TempWeapon activeWeapon)
        {
            if(activeWeapon==null)
            {
                playerWeaponAnim.gameObject.SetActive(false);
                return;
            }

            HardCopyWeapon(activeWeapon);
        }

        void HardCopyWeapon(TempWeapon activeWeapon)
        {
            playerWeaponAnim.gameObject.SetActive(true);
            hitBox.sr.sprite = activeWeapon.sr.sprite;
            hitBox.sr.color = activeWeapon.sr.color;
            HardCopyRB(hitBox.rb,activeWeapon.rb);
            HardCopyBC(hitBox.bc,activeWeapon.bc);
            hitBox._damage = activeWeapon._damage;
            hitBox._stun = activeWeapon._stun;
            hitBox._cooldown = activeWeapon._cooldown;
            hitBox._name = activeWeapon._name;
            hitBox._rank = activeWeapon._rank;
            hitBox._id = activeWeapon._id;
            hitBox._durability = activeWeapon._durability;
            hitBox.tempType = activeWeapon.tempType;
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
            if(hitBox._name=="") return; // This is fragile (!)
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
            
            switch(hitBox.tempType)
            {
                case TempWeapon.Temporary.Sword:
                    playerWeaponAnim.Play("SwordSwing",0,0f);
                    audioSource.pitch = 1f;
                    audioSource.PlayOneShot(swordAudio);
                    break;
                case TempWeapon.Temporary.Bow:
                    playerWeaponAnim.Play("Shoot",0,0f);
                    audioSource.pitch = 1f;
                    audioSource.PlayOneShot(bowAudio);
                    break;
                case TempWeapon.Temporary.Axe:
                    playerWeaponAnim.Play("AxeSwing",0,0f);
                    audioSource.pitch = .5f;
                    audioSource.PlayOneShot(swordAudio);
                    break;
                default:
                    playerWeaponAnim.Play("ClubSwing",0,0f);
                    audioSource.pitch = 1f;
                    audioSource.PlayOneShot(clubAudio);
                    break;
            }
            // if(hitBox.tempType!=TempWeapon.Temporary.Bow)
            //     playerWeaponAnim.Play("Swing",0,0f);
            // else
            //     playerWeaponAnim.Play("Shoot",0,0f);
            yield return new WaitForSeconds(cooldown);
            canAttack=true;
        }
    }
}