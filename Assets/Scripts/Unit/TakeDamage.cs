using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class TakeDamage : MonoBehaviour
    {
        public delegate void DamageMethod(float power, DamageType kind);

        public event DamageMethod Damage;

        public void GetDamage(float power, DamageType kind = DamageType.Physics)
        {
            if (Damage != null)
            {
                Damage(power, kind);
            }
            else
            {
                UnitData data = GetComponent<UnitData>();
                data.Energy -= power;
                if (data.Energy <=0 ) Destroy(gameObject);
            }
        }
    }
}