using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace KingDOM.Platformer2D
{
    public class CharacterBrain : UnitBrain
    {
        public override void InitFSM()
        {
            
            base.InitFSM();
            fsm.AddStates(new[] { IDLE, MOVE, JUMP, ATTACK, DIE });
            fsm.In().To(IDLE);
            fsm.AnyState().To(DIE).If(m => Data.Energy <= 0).Jump(EndGame);
            fsm.AnyState().To(ATTACK).If(m => Data.attack.CanAttack && Data.attack.Active);
            fsm.AnyState().To(JUMP).If(m => !Data.move.IsGrounded && m.current.name != ATTACK);
            fsm.In(ATTACK).To(IDLE).If(m => m.Timer >= Data.attack.TimeAttack);
            fsm.In(JUMP).To(IDLE).If(m => Data.move.IsGrounded);
            //fsm.In(DIE).To(IDLE).If(m => m.Timer >= Data.avatar.TimeRespawn && Data.Energy > 0);
            fsm.In(DIE).To();
            fsm.In(IDLE).To(MOVE).If(m => Mathf.Abs(Data.move.moveDirection.x) > float.Epsilon);
            fsm.In(MOVE).To(IDLE).If(m => Mathf.Abs(Data.move.moveDirection.x) < float.Epsilon);

            //fsm
            fsm.In(IDLE).Enter(() => SetState(BrainState.Idle));
            fsm.In(DIE).Enter(() => fsm.AnyState().noMoreTransitions = true)
                .Exit(() => { fsm.AnyState().noMoreTransitions = false;
                                Data.IsDestroyed = false;});
            fsm.In(JUMP).Enter(() => SetState(BrainState.Jump));
            fsm.In(MOVE).Enter(() => SetState(BrainState.Move));
            fsm.In(ATTACK).Enter(() => { Data.attack.Active = false; Data.attack.CanAttack = false; SetState(Data.attack.StateAttack); ActivateWepon(true); })
                .Exit(() => { Data.attack.CanAttack = true; ActivateWepon(false); });
            fsm.Start();
        }

        public void EndGame()
        {
            Data.gameObject.SetActive(false);
            //var c = Data.avatar.body.GetComponent<Collider2D>();
            //c.enabled = false;
            Invoke("StartMenu", 5);
        }

        public void StartMenu()
        {
            SceneManager.LoadScene(0);
        }

    }
}