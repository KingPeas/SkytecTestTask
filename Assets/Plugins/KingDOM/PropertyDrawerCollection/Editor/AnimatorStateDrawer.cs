using System;
using System.Collections.Generic;
using KingDOM.Util;
using UnityEditor;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;


[CustomPropertyDrawer(typeof(AnimatorStateAttribute))]
public class AnimatorStateDrawer : AnimatorHashDrawer
{
    public override string GetSourceName()
    {
        return animatorStateAttribute.SourceName;
    }

    public override string[] GetOptions()
    {
        List<string> paramList = new List<string>();

        if (controller != null)
        {
            UnityEditor.Animations.AnimatorControllerLayer[] layers = controller.layers;
            //List<AnimatorStateInfo> states = new List<AnimatorStateInfo>();
            foreach (UnityEditor.Animations.AnimatorControllerLayer layer in layers)
            {
                UnityEditor.Animations.AnimatorStateMachine sm = layer.stateMachine as UnityEditor.Animations.AnimatorStateMachine;
                GetAllStates(sm, layer.name, paramList);
            }
        }
        return paramList.ToArray();
    }

    void GetAllStates(AnimatorStateMachine machine, string layer, List<string> parms)
    {
        foreach (var state in machine.states)
        {
            parms.Add(GetFullName(layer, state.state.name));
        }
        foreach (var sm in machine.stateMachines)
        {
            GetAllStates(sm.stateMachine, GetFullName(layer, sm.stateMachine.name), parms);
        }
    }

    private AnimatorStateAttribute animatorStateAttribute
    {
        get
        {
            return (AnimatorStateAttribute)attribute;
        }
    }

}