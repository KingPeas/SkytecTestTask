using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KingDOM.SimpleFSM.Log
{
    public class LogUnity : MonoBehaviour
    {
        [Flags]
        public enum LogEntry
        {
            None = 0,
            MachineChange = 1,
            StateEnter = 2,
            StateStay = 4,
            StateExit = 8,
            TransitionJump = 16
        }
        [BitMask]
        public LogEntry listen = (LogEntry)255;
        private List<SimpleMachine> journals;
        private Dictionary<SimpleMachine, Dictionary<LogEntry, List<Action>>> actions;
        private static LogUnity instance = null;

        public static void Add(SimpleMachine machine)
        {
            if (instance != null)
            {
                if (instance.isActiveAndEnabled)
                    instance.AddMachine(machine);
            }
        }

        // Use this for initialization
        void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
                return;
            }
            instance = this;
            journals = new List<SimpleMachine>();
            actions = new Dictionary<SimpleMachine, Dictionary<LogEntry, List<Action>>>();
        }

        // Update is called once per frame
        public void AddMachine(SimpleMachine machine)
        {
            if (!journals.Contains(machine))
            {
                journals.Add(machine);
                ActivateLog(machine);
            }
        }

        public void RemoveMachine(SimpleMachine machine)
        {
            if (journals.Contains(machine))
            {
                journals.Remove(machine);
                DeactivateLog(machine);
            }
        }

        private void ActivateLog(SimpleMachine machine)
        {
            if ((listen & LogEntry.MachineChange) > 0) machine.Change(RegisterAction(machine, LogEntry.MachineChange, Change, machine));
            if (!(listen == LogEntry.None || listen == LogEntry.MachineChange))
            {
                foreach (State state in machine.states.Values)
                {
                    if ((listen & LogEntry.StateEnter) > 0) state.Enter(RegisterAction(machine, LogEntry.StateEnter, Enter, state));
                    if ((listen & LogEntry.StateStay) > 0) state.Stay(RegisterAction(machine, LogEntry.StateStay, Stay, state));
                    if ((listen & LogEntry.StateExit) > 0) state.Exit(RegisterAction(machine, LogEntry.StateExit, Exit, state));
                    foreach (Transition transition in state.transitions)
                    {
                        if ((listen & LogEntry.TransitionJump) > 0) transition.Jump(RegisterAction(machine, LogEntry.TransitionJump, Jump, transition));
                    }
                }
            }
        }

        private void DeactivateLog(SimpleMachine machine)
        {
            if (!actions.ContainsKey(machine)) return;
            UnregisterAction(machine, LogEntry.MachineChange, machine.Change);

            foreach (State state in machine.states.Values)
            {
                UnregisterAction(machine, LogEntry.StateEnter, state.Enter);
                UnregisterAction(machine, LogEntry.StateStay, state.Stay);
                UnregisterAction(machine, LogEntry.StateExit, state.Exit);
                foreach (Transition transition in state.transitions)
                {
                    UnregisterAction(machine, LogEntry.TransitionJump, transition.Jump);
                }
            }
        }

        private string TimeStr()
        {
            return string.Format("{0:##.00}", Time.time);
        }

        private Action RegisterAction(SimpleMachine machine, LogEntry entry, Action<object> action, object info)
        {
            Action res = (() => action(info));
            if (!actions.ContainsKey(machine)) actions.Add(machine, new Dictionary<LogEntry, List<Action>>());
            if (!actions[machine].ContainsKey(entry)) actions[machine].Add(entry, new List<Action>());
            actions[machine][entry].Add(res);
            return res;
        }

        private void UnregisterAction(SimpleMachine machine, LogEntry entry, Func<Action, object> eventUnregister)
        {
            if (actions.ContainsKey(machine) && actions[machine].ContainsKey(entry))
            {
                foreach (Action action in actions[machine][entry]) eventUnregister(action); 
            }
        }

        private void Change(object source)
        {
            SimpleMachine machine = source as SimpleMachine;
            if (machine == null) return;
            Debug.Log(string.Format("({0}) Machine: {1} Change {2}-->{3}", TimeStr(), machine, machine.last.name,
                machine.current.name));
        }

        private void Enter(object source)
        {
            State state = source as State;
            if (state == null) return;
            Debug.Log(string.Format("({0}) Machine: {1} Enter to {2}", TimeStr(), state.machine, state.name));
        }
        private void Stay(object source)
        {
            State state = source as State;
            if (state == null) return;
            Debug.Log(string.Format("({0}) Machine: {1} Stay in {2}", TimeStr(), state.machine, state.name));
        }
        private void Exit(object source)
        {
            State state = source as State;
            if (state == null) return;
            Debug.Log(string.Format("({0}) Machine: {1} Exit from {2}", TimeStr(), state.machine, state.name));
        }
        private void Jump(object source)
        {
            Transition transition = source as Transition;
            if (transition == null) return;
            Debug.Log(string.Format("({0}) Machine: {1} Jump {2}-->{3}", TimeStr(), transition.from.machine, transition.from.name, transition.to.name));
        }

    }

}

