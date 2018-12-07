using KingDOM.Event;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class TakeDamage : MonoBehaviour
    {
        public delegate void DamageMethod(float power, DamageType kind, MoveUnitData source);

        public event DamageMethod Damage;

        public void GetDamage(float power, DamageType kind = DamageType.Physics, MoveUnitData source = null)
        {
            if (Damage != null)
            {
                Damage(power, kind, source);
            }
            else
            {
                UnitData data = GetComponent<UnitData>();
                data.Energy -= power;
                if (data.Energy <= 0) {
                    Sender.SendEvent(EventName.DESTROYER, this, ParmName.TARGET, data, ParmName.SOURCE, source);
                    Destroy(gameObject);
                }
            }
        }
    }
}