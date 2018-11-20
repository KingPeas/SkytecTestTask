using KingDOM.SimpleFSM;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitBrain : MonoBehaviour
    {
        [AnimatorParameter]
        public int AnimParameter = 0;
        private DamageBehaviour damageBehaviour = null;
        private UnitData data = null;
        protected SimpleMachine fsm;

        protected enum BrainState
        {
            Idle,
            Move,
            Jump,
            Attack,
            SpecialAttack
        }

        // состояния
        protected const string IDLE = "idle";
        protected const string MOVE = "move";
        protected const string JUMP = "jump";
        protected const string ATTACK = "attack";
        protected const string SPECIAL_ATTACK = "spec_attack";

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
            fsm = new SimpleMachine("CharacterBrain");
        }
         protected void SetState(BrainState state)
        {
            if (data.animator != null) data.animator.SetInteger(AnimParameter, (int)state);
        }
    }
}