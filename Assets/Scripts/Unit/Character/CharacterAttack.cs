using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterAttack : CharacterAction
    {
        public string AxisAttack= "Fire1";
        public UnitBrain.BrainState State = UnitBrain.BrainState.Attack;
        public int numWeapon = 0;

        // Update is called once per frame
        protected virtual void Update()
        {
            if (data.attack.CanAttack && Input.GetButtonDown(AxisAttack))
            {
                data.attack.Active = true;
                data.attack.StateAttack = State;
                data.attack.numWeapon = numWeapon;
            }

        }
    }
}
