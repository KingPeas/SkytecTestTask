using KingDOM.SimpleFSM;
using KingDOM.SimpleFSM.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitBrain : MonoBehaviour
    {
        [AnimatorParameter(SourceName = "animator")]
        public int AnimParameter = 0;
        private DamageBehaviour damageBehaviour = null;
        private MoveUnitData data = null;
        protected SimpleMachine fsm;
        public Animator animator = null;
        public bool UseLog = false;

        public enum BrainState
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
        protected const string DIE = "die";

        public MoveUnitData Data
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
            Data = GetComponent<MoveUnitData>();
            if (!Data)
            {
                enabled = false;
                return;
            }
            TakeDamage taken = GetComponent<TakeDamage>();
            if (taken) taken.Damage += GetDamage;
            damageBehaviour = GetComponent<DamageBehaviour>();
            if (!animator && data.Avatar) animator = data.Avatar.GetComponent<Animator>();
            InitFSM();
            if (UseLog) LogUnity.Add(fsm);
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
            if (animator != null) animator.SetInteger(AnimParameter, (int)state);
        }
    }
}