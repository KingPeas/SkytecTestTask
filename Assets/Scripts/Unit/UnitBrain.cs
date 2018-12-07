using KingDOM.Event;
using KingDOM.SimpleFSM;
using KingDOM.SimpleFSM.Log;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitBrain : MonoBehaviour
    {
        private DamageBehaviour damageBehaviour = null;
        private MoveUnitData data = null;
        protected SimpleMachine fsm;
        public bool UseLog = false;

        public enum BrainState
        {
            Idle = 0,
            Move = 1,
            Jump = 2,
            Attack = 3,
            SpecialAttack = 4,
            Run = 5,
            Block = 6,
            Follow = 7,
            Die = 8
        }

        // состояния
        protected const string IDLE = "idle";
        protected const string MOVE = "move";
        protected const string RUN = "run";
        protected const string JUMP = "jump";
        protected const string ATTACK = "attack";
        protected const string BLOCK = "block";
        protected const string FOLLOW = "follow";
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
            InitFSM();
            if (UseLog)
                LogUnity.Add(fsm);
        }

        public virtual void Update()
        {
            fsm.Update();
            Render();
        }

        public void GetDamage(float power, DamageType kind = DamageType.Physics, MoveUnitData source = null)
        {
            if (damageBehaviour)
            {
                data.Energy -= damageBehaviour.CalcDamage(power, kind);
            }
            else
            {
                data.Energy -= power;
            }
            if (Data.Energy <= 0)
            {
                data.IsDestroyed = true;
                Sender.SendEvent(EventName.DESTROYER, this, ParmName.TARGET, data, ParmName.SOURCE, source);
            }
        }

        public void Render()
        {
            if (data.avatar.render)
            {
                if (Mathf.Abs(data.move.moveDirection.x) > float.Epsilon)
                {
                    data.avatar.flip = data.move.moveDirection.x < 0 ;
                    Vector3 scale = data.avatar.render.localScale;
                    scale.x = Mathf.Abs(scale.x) * (data.avatar.flip ? -1 : 1);
                    data.avatar.render.localScale = scale;
                }
            }
        }

        public void ActivateWepon(bool active)
        {
            GameObject go = data.GetWeapon();
            if (go) go.SetActive(active);
        }

        public virtual void InitFSM()
        {
            fsm = new SimpleMachine("Brain");
        }
         protected void SetState(BrainState state)
        {
            if (data.avatar.animator != null) data.avatar.animator.SetInteger(data.avatar.AnimParameter, (int)state);
        }
    }
}