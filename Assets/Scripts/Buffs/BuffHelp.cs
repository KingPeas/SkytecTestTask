using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D {
    public class BuffHelp : Buff
    {
        public float AddEnergy = 1;
        protected override void Activate(UnitData data)
        {
            data.Energy += AddEnergy;
        }
    }
}