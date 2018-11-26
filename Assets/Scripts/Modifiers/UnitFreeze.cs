using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class UnitFreeze : UnitModifier
    {
        private MoveUnitData moveData = null;
        public override void Activate()
        {
            if (data && data is MoveUnitData)
            {
                moveData = data as MoveUnitData;
                moveData.move.speed /= 1000;
                moveData.move.ForceJump /= 1000;
            }
        }

        public override void Deactivate()
        {
            if (moveData)
            {
                moveData.move.speed *= 1000;
                moveData.move.ForceJump *= 1000;
            }
        }
    }
}