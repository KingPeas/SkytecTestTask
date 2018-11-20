using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterBrain : UnitBrain
    {
        public override void InitFSM()
        {
            base.InitFSM();
            fsm.AddStates(new[] { IDLE, MOVE, JUMP, ATTACK, SPECIAL_ATTACK });
            fsm.In().To(IDLE);
            //fsm
            fsm.In(IDLE).Enter(() => SetState(BrainState.Idle));
            fsm.Start();
        }
    }
}