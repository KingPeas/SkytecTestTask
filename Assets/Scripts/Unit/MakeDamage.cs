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
                taken.GetDamage(DamagePower, DamageMethod);
                if (SetDamage != null) SetDamage(DamagePower, DamageMethod);
            }
        }
    }
}