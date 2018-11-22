using System;
using System.Collections.Generic;
using KingDOM.SimpleFSM.Exception;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace KingDOM.SimpleFSM
{
    [Serializable]
    public class SimpleMachine
    {
        public int id = 0;
        public Dictionary<string, State> states;
        /// <summary>
        /// состояние до update машины
        /// </summary>
        public State last = null;
        public string name = "";
        private event Action change;
        private AnyState anyState = null;
#if UNITY_5_3_OR_NEWER
        private float timer = 0f;
#endif
        private DateTime dateTime;
        private static int counter = 0;


        public float Timer
        {
            get
            {
                if (!isWork) return 0f;
#if UNITY_5_3_OR_NEWER
                if
                    (Application.isPlaying) return Time.time - timer;
                else
                    return (float)((DateTime.Now - dateTime).TotalSeconds);
#else
                return (float)((DateTime.Now - dateTime).TotalSeconds);
#endif
            }
        }

        public bool isWork { get; private set; }

        public State current { get; private set; }

        public Parameters vals { get; private set; }

        public SimpleMachine(string name = "")
        {
            this.name = name;
            id = ++counter;
            states = new Dictionary<string, State>();
            vals = new Parameters();
            State initState = new State(this, State.INIT);
            states.Add(State.INIT, initState);
            states.Add(State.FINAL, new State(this, State.FINAL));
            current = initState;
            last = initState;
            anyState = new AnyState(this, State.ANY_STATE);
            isWork = false;
        }

        public T Get<T>(string name)
        {
            return vals.Get<T>(name);
        }

        public object Get(string name)
        {
            return vals[name];
        }

        public SimpleMachine Start()
        {
            isWork = true;
            current = In();
            ResetTimer();
            return this;
        }

        public SimpleMachine Pause()
        {
            return Pause(!isWork);
        }

        public SimpleMachine Pause(bool val)
        {
            isWork = val;
            return this;
        }


        public SimpleMachine Stop()
        {
            isWork = false;
            return this;
        }

        public State Add(string stateName)
        {
            if (!states.ContainsKey(stateName))
            {
                State state = new State(this, stateName);
                states.Add(stateName, state);
                return state;
            }
            else
            {
                throw new SimpleMachineCanNotAddState("Нельзя добавить состояние " + stateName + ", оно уже существует.");
            }
        }

        public SimpleMachine AddStates(params string[] stateNames)
        {
            foreach (string stateName in stateNames)
            {
                Add(stateName);
            }
            return this;
        }

        /*public SimpleMachine Remove(string stateName)
        {
            if (states.ContainsKey(stateName))
            {
                states.Remove(stateName);
            }
            else
            {
                throw new Exception("Нельзя удалить состояние " + stateName + ", оно не существует.");
            }
            return this;
        }  */
        public AnyState AnyState()
        {
            return anyState;
        }

        public State In(string stateName = State.INIT)
        {
            if (states.ContainsKey(stateName))
            {
                return states[stateName];
            }
            else
            {
                throw new SimpleMachineStateNotExist("Не определенное состояние с именем " + stateName);
            }
        }

        public State Update()
        {
            if (!isWork) return current;
            State newState = anyState.UpdateAny(current);
            if (newState != current)
            {
                last = current;
                current = newState;
                OnChange();
                if (current.name == State.FINAL)
                {
                    Stop();
                    return current;
                }
            }
            newState = current.Update();
            if (newState != current)
            {
                last = current;
                current = newState;
                OnChange();
                if (current.name == State.FINAL) Stop();
            }
            return current;
        }

        public State GoTo(string stateName, bool withActions = true)
        {
            if (withActions ) current.OnExit();
            State newState = In(stateName);
            if (withActions) newState.OnEnter();
            if (newState != current)
            {
                last = current;
                current = newState;
                OnChange();
                if (current.name == State.FINAL) Stop();
            }
            return current;
        }

        public SimpleMachine Reset()
        {
            current = In();
            return this;
        }

        public SimpleMachine Change(Action callback)
        {
            change += callback;
            return this;
        }
        public SimpleMachine ChangeDel(Action callback)
        {
            change -= callback;
            return this;
        }
        public override string ToString()
        {
            return (!string.IsNullOrEmpty(name) ? string.Format("FSM \"{0}\"({1})", name, id) : string.Format("FSM ({0})", id));
        }

        internal void ResetTimer()
        {
#if UNITY_5_3_OR_NEWER
            if (Application.isPlaying)
                timer = Time.time;
            else
                dateTime = DateTime.Now;
#else
            dateTime = DateTime.Now;
#endif
        }

        private void OnChange()
        {
            if (change != null)
            {
                change();
            }
        }

    }

}

