using KingDOM.SimpleFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitBrain : MonoBehaviour
    {
        private DamageBehaviour damageBehaviour = null;
        private UnitData data = null;
        SimpleMachine fsm;

        public UnitData Data
        {
            get
            {
                return data;
            }

            set
            {
                data = value;
            }
        }

        private void Awake()
        {
            Data = GetComponent<UnitData>();
            if (!Data)
            {
                enabled = false;
                return;
            }
            TakeDamage taken = GetComponent<TakeDamage>();
            if (taken) taken.Damage += GetDamage;
            damageBehaviour = GetComponent<DamageBehaviour>();
        }

        public virtual void Update()
        {
            fsm.Update();
        }

        public void GetDamage(float power, DamageType kind = DamageType.Physics)
        {

        }

        public virtual void InitFSM()
        {

        }
    }
}