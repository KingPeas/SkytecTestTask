using System;
using System.Collections.Generic;
using KingDOM.SimpleFSM.Exception;

namespace KingDOM.SimpleFSM
{
    public class State
    {
        internal List<Transition> transitions { get; set; }
        internal SimpleMachine machine { get; set; }
        private event Action enter;
        private event Action exit;
        private event Action stay;

        public const string INIT = "__init";
        public const string FINAL = "__final";
        internal const string ANY_STATE = "__anystate";

        internal State()
        {
        }
        internal State(SimpleMachine machine, string name)
        {
            this.name = name;
            this.machine = machine;
            transitions = new List<Transition>();
        }

        public string name { get; protected set; }

        internal void  OnEnter()
        {
            machine.ResetTimer();
            if (enter != null)
                enter();
        }

        internal void OnExit()
        {
            if (exit != null)
                exit();
        }

        internal void OnStay()
        {
            if (stay != null)
            {
                stay();
            }
        }

        virtual public Transition To(string stateName = FINAL)
        {
            if (name == FINAL)
            {
                throw new SimpleMachineCanNotAddTransition("Из конечного состояния нельзя создать переход. Перезапустите машину состояний.");
            }
            State stateB = machine.In(stateName);
            if (stateB != null)
            {
                Transition transition = new Transition(this, stateB);
                transitions.Add(transition);
                return transition;
            }
            else
            {
                throw new SimpleMachineStateNotExist("Не удалось обнаружить состояние " + stateName);
            }
        }

        virtual public State Update()
        {
            State res = this;
            State next = null;
            OnStay();
            foreach (Transition transition in transitions)
            {
                next = transition.Go();
                if (next != null)
                {
                    OnExit();
                    transition.OnJump();
                    res = next;
                    next.OnEnter();
                    next = next.Update();
                    if (next != null) res = next;
                    break;
                }
            }
           return res;
        }

        virtual public State Enter(Action callback)
        {
            enter += callback;
            return this;
        }
        virtual public State EnterDel(Action callback)
        {
            enter -= callback;
            return this;
        }
        virtual public State Exit(Action callback)
        {
            exit += callback;
            return this;
        }
        virtual public State ExitDel(Action callback)
        {
            exit -= callback;
            return this;
        }
        virtual public State Stay(Action callback)
        {
            stay += callback;
            return this;
        }
        virtual public State StayDel(Action callback)
        {
            stay -= callback;
            return this;
        }

    }

}

