using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitShield : UnitModifier
    {
        DamageBehaviour damage = null;
        public override void Activate()
        {
            if (data)
            {
                damage = data.GetComponent<DamageBehaviour>();
                damage.ScaleDamage /= 1000;
            }
        }

        public override void Deactivate()
        {
            if (damage) damage.ScaleDamage *= 1000;
        }        
    }
}