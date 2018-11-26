using UnityEngine;
using System.Collections;
using KingDOM.Event;

namespace KingDOM.Platformer2D {
    public class BuffWithModifier : Buff
    {
        public UnitModifier modifier = null;
        protected override void Activate(UnitData data)
        {
            if (GameLogic.Instance)
            {
                GameLogic.Instance.ApplyModifiers(data, modifier);
            }
        }
    }
}