using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.Platformer2D
{
    public class CharacterBrain : UnitBrain
    {
        public float TimeRespawn = 1f;
        public float TimeAttack = 0.5f;
        public float TimeSpecialAttack = 0.5f;
        public override void InitFSM()
        {
            base.InitFSM();
            fsm.AddStates(new[] { IDLE, MOVE, JUMP, ATTACK, DIE });
            fsm.In().To(IDLE);
            fsm.AnyState().To(DIE).If(m => Data.IsDead);
            fsm.AnyState().To(ATTACK).If(m => Data.CanAttack && Data.Attack);
            fsm.In(ATTACK).To(IDLE).If(m => m.Timer >= TimeAttack);
            fsm.AnyState().To(JUMP).If(m => !Data.IsGrounded && m.current.name != ATTACK);
            fsm.In(JUMP).To(IDLE).If(m => Data.IsGrounded);
            fsm.In(DIE).To(IDLE).If(m => m.Timer >= TimeRespawn);
            fsm.In(IDLE).To(MOVE).If(m => Mathf.Abs(Data.MoveDirection.x) > float.Epsilon);
            fsm.In(MOVE).To(IDLE).If(m => Mathf.Abs(Data.MoveDirection.x) < float.Epsilon);

            //fsm
            fsm.In(IDLE).Enter(() => SetState(BrainState.Idle));
            fsm.In(DIE).Enter(() => fsm.AnyState().noMoreTransitions = true)
                .Exit(() => { fsm.AnyState().noMoreTransitions = false;
                                Data.IsDead = false;});
            fsm.In(JUMP).Enter(() => SetState(BrainState.Jump));
            fsm.In(MOVE).Enter(() => SetState(BrainState.Move));
            fsm.In(ATTACK).Enter(() => { Data.Attack = false; Data.CanAttack = false; SetState(Data.StateAttack); })
                .Exit(() => Data.CanAttack = true);
            fsm.Start();
        }

    }
}