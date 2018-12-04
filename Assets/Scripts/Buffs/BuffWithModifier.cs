using UnityEngine;
using System.Collections;
using KingDOM.Event;

namespace KingDOM.Platformer2D
{
    public class BuffWithModifier : Buff
    {
        //public UnitModifier modifier = null;
        public int NumModifier = -1;
        public bool OnlyForMe = true;
        protected override void Activate(UnitData data)
        {
            if (!ResourceCollection.Instance) return; 
            UnitModifier modifier = ResourceCollection.GetModifier(NumModifier);
            if (modifier == null) return;
            if (OnlyForMe)
            {
                var mod = Instantiate(modifier, data.transform);
                mod.transform.localPosition = Vector3.zero;
                mod.transform.localRotation = Quaternion.identity;
            }
            else
            {
                if (GameLogic.Instance) GameLogic.Instance.ApplyModifiers(data, modifier);
            }
        }
    }
}