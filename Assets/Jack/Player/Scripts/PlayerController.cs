using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System;

    public class PlayerController : MonoBehaviour
    {
        public int lives;
        public int maxLives;
        public bool inShadow;
        public bool isCaught=false;
        public event Action<bool> onCaught;
        [HideInInspector] public Dictionary<TempWeapon.Temporary,List<TempWeapon>> weaponsDict;
            
        [HideInInspector] public TempWeapon.Temporary activeWeaponIdx = 0;
        [HideInInspector] public TempWeapon activeWeapon {get; private set;}
        public static event Action<TempWeapon> equippedWeapon;

        void OnEnable()
        {
            PlayerInput.onTempToggle += SwitchTempWeapon;
        }

        void OnDisable()
        {
            PlayerInput.onTempToggle -= SwitchTempWeapon;
        }
            
        void Start()
        {
            // Initialize weaponsDict
            weaponsDict = new Dictionary<TempWeapon.Temporary,List<TempWeapon>>();
            weaponsDict[TempWeapon.Temporary.Sword] = new List<TempWeapon>();
            weaponsDict[TempWeapon.Temporary.Axe] = new List<TempWeapon>();
            weaponsDict[TempWeapon.Temporary.Bow] = new List<TempWeapon>();
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            TempWeapon temp;

            if((temp=col.GetComponent<TempWeapon>())
                && !col.CompareTag("Player"))
            {
                weaponsDict[temp.tempType].Add(temp); // Initializes List
                if(activeWeapon!=null)
                    SwitchTempWeapon(temp.tempType);
            }
        }

        void SwitchTempWeapon(TempWeapon.Temporary nextTempIdx, int count=0)
        {
            if(weaponsDict[nextTempIdx]!=null)
            {
                activeWeaponIdx=nextTempIdx;
                if(weaponsDict[activeWeaponIdx].Count!=0)
                    activeWeapon = weaponsDict[activeWeaponIdx][0];
                else
                {
                    TempWeapon.Temporary cycleIdx = (TempWeapon.Temporary)(((int)activeWeaponIdx+1)%TempWeapon.TempEnumCount);
                    if(count<TempWeapon.TempEnumCount)
                        SwitchTempWeapon(cycleIdx,count+1); // Recursive Case
                    else
                    {
                        activeWeapon = new TempWeapon(); // Base Case
                        return;
                    }
                }
            }
            equippedWeapon?.Invoke(activeWeapon);
        }
    }
