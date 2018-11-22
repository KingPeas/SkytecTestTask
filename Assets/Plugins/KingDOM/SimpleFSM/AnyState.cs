using KingDOM.SimpleFSM.Exception;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.SimpleFSM
{
    public class AnyState : State
    {
        /// <summary>
        /// отключение переходов из любого состояния. Например после смерти персонажа надо оставаться в состоянии смерть
        /// </summary>
        public bool noMoreTransitions = false;
        internal AnyState()
        {

        }
        internal AnyState(SimpleMachine machine, string name)
        {
            this.name = name;
            this.machine = machine;
            transitions = new List<Transition>();
        }
        internal State UpdateAny(State current)
        {
            if (noMoreTransitions) return this;
            State res = current;
            State next = null;
            foreach (Transition transition in transitions)
            {
                next = transition.Go();
                if (next != null && next != current)
                {
                    current.OnExit();
                    transition.OnJump();
                    res = next;
                    next.OnEnter();
                    break;
                }
            }
            return res;
        }

        public override State Enter(Action callback)
        {
            throw new SimpleMachineCanNotAddTransition("В данное состояния нельзя войти.");
        }
        public override State EnterDel(Action callback)
        {
            throw new SimpleMachineCanNotAddTransition("В данное состояния нельзя войти.");
        }
        public override State Stay(Action callback)
        {
            throw new SimpleMachineCanNotAddTransition("В данное состояния нельзя находиться.");
        }
        public override State StayDel(Action callback)
        {
            throw new SimpleMachineCanNotAddTransition("В данное состояния нельзя находиться.");
        }
    }
}
