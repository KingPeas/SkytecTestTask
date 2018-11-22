using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterAttack : CharacterAction
    {
        public string AxisAttack= "Fire1";
        public UnitBrain.BrainState State = UnitBrain.BrainState.Attack;
        // Update is called once per frame
        void Update()
        {
            if (data.CanAttack && Input.GetButtonDown(AxisAttack))
            {
                data.Attack = true;
                data.StateAttack = State;
            }

        }
    }
}
