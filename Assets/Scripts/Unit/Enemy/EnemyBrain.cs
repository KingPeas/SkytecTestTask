﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class EnemyBrain : UnitBrain
    {

        public override void InitFSM()
        {
            base.InitFSM();
            fsm.AddStates(new[] { IDLE, MOVE, RUN, JUMP, BLOCK, ATTACK, DIE });
            fsm.AnyState().To(DIE).If(m => Data.IsDestroyed);
            fsm.AnyState().To(ATTACK).If(m => Data.attack.CanAttack && Data.attack.Active);
            fsm.AnyState().To(JUMP).If(m => !Data.move.IsGrounded && m.current.name != ATTACK);
            fsm.In().To(IDLE);
            fsm.In(ATTACK).To(IDLE).If(m => m.Timer >= Data.attack.TimeAttack);
            fsm.In(JUMP).To(IDLE).If(m => Data.move.IsGrounded);
            fsm.In(DIE).To(IDLE).If(m => m.Timer >= Data.avatar.TimeRespawn);
            fsm.In(IDLE).To(MOVE).If(m => Mathf.Abs(Data.move.moveDirection.x) > float.Epsilon);
            fsm.In(MOVE).To(IDLE).If(m => Mathf.Abs(Data.move.moveDirection.x) < float.Epsilon);

            //fsm
            fsm.In(IDLE).Enter(() => SetState(BrainState.Idle));
            fsm.In(DIE).Enter(() => fsm.AnyState().noMoreTransitions = true)
                .Exit(() => {
                    fsm.AnyState().noMoreTransitions = false;
                    Data.IsDestroyed = false;
                });
            fsm.In(JUMP).Enter(() => SetState(BrainState.Jump));
            fsm.In(MOVE).Enter(() => SetState(BrainState.Move));
            fsm.In(ATTACK).Enter(() => { Data.attack.Active = false; Data.attack.CanAttack = false; SetState(Data.attack.StateAttack); ActivateWepon(true); })
                .Exit(() => { Data.attack.CanAttack = true; ActivateWepon(false); });
            fsm.Start();
        }
    }
}