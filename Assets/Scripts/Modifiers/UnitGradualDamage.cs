using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitGradualDamage : UnitModifier
    {
        public float TimeBreak = 1f;
        public float Power = 0.5f;
        public DamageType Kind = DamageType.Physics;
        private TakeDamage taker = null;
        public override void Activate()
        {
            taker = GetComponentInParent<TakeDamage>();
            if (taker)
                InvokeRepeating("RepeatDamage", TimeBreak, TimeBreak);
        }

        public override void Deactivate()
        {
            
        }

        private void RepeatDamage()
        {
            taker.GetDamage(Power, Kind);
        }
    }
}
