using System;
using System.Collections.Generic;

namespace KingDOM.SimpleFSM
{
    public class Transition
    {
        
        public State from { get; private set; }
        public State to { get; private set; }
        public List<Func<SimpleMachine, bool>> conditions { get; private set; }
        private event Action jump;

        internal Transition(State from, State to)
        {
            conditions= new List<Func<SimpleMachine, bool>>();
            this.from = from;
            this.to = to;
        }

        internal State Go()
        {
            State res = null;
            if (conditions.Count == 0)
            {
                res = to;
            }
            else
            {
                foreach (Func<SimpleMachine, bool> condition in conditions)
                {
                    if (condition(from.machine))
                    {
                        res = to;
                        break;
                    }
                }
            }
            
            return res;
        }

        internal void OnJump()
        {
            if (jump != null)
                jump();
        }

        public Transition If(Func<SimpleMachine, bool> condition)
        {
            conditions.Add(condition);
            return this;
        }

        virtual public Transition Jump(Action callback)
        {
            jump += callback;
            return this;
        }
        virtual public Transition JumpDel(Action callback)
        {
            jump -= callback;
            return this;
        }
    }
}
