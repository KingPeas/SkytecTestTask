using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class MakeDamage : MonoBehaviour
    {
        public delegate void DamagedMethod(float power, DamageType kind = DamageType.Physics);

        public event DamagedMethod SetDamage;
        public float DamagePower = 1f;
        [BitMask]
        public DamageType DamageMethod = DamageType.Physics;
        public int NumModifier = -1;
        public MoveUnitData source = null;
        public bool OnlyForOne = true;

        private void Start()
        {
            if (!source) source = GetComponentInParent<MoveUnitData>();
        }


        private void OnTriggerEnter2D(Collider2D collision)
        {
            TakeDamage taken = collision.GetComponent<TakeDamage>();
            OnDamage(taken);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            TakeDamage taken = collision.gameObject.GetComponent<TakeDamage>();
            OnDamage(taken);
        }

        private void OnDamage(TakeDamage taken)
        {
            if (taken)
            {
                taken.GetDamage(DamagePower, DamageMethod, source);
                if (SetDamage != null) SetDamage(DamagePower, DamageMethod);

                if (!ResourceCollection.Instance) return;
                UnitModifier modifier = ResourceCollection.GetModifier(NumModifier);
                if (modifier == null) return;
                UnitData data = taken.GetComponentInParent<UnitData>();
                if (data == null) return;
                if (OnlyForOne)
                {
                    var mod = Instantiate(modifier, data.transform);
                    mod.transform.localPosition = Vector3.zero;
                    mod.transform.localRotation = Quaternion.identity;
                    mod.source = source;
                }
                else
                {
                    if (GameLogic.Instance) GameLogic.Instance.ApplyModifiers(source, modifier);
                }
            }
        }
    }
}