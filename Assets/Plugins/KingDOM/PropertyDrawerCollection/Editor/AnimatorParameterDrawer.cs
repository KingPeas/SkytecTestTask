using KingDOM.Util;
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
using UnityEditorInternal;
#else
using UnityEditor.Animations;
#endif
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

// <summary>
// Displays the name of the parameter animation in class
// </summary>
[CustomPropertyDrawer(typeof (AnimatorParameterAttribute))]
public class AnimatorParameterDrawer : AnimatorHashDrawer
{
    
    /// <summary>
    /// Determines whether this instance can add event name the specified animatorController index.
    /// </summary>
    /// <returns>
    /// <c>true</c> if this instance can add event name the specified animatorController i; otherwise, <c>false</c>.
    /// </returns>
    /// <param name='animatorController'>
    /// If set to <c>true</c> animator controller.
    /// </param>
    /// <param name='index'>
    /// If set to <c>true</c> index.
    /// </param>

    bool CanAddEventName(AnimatorController animatorController, int index)
    {
        return !(animatorParameterAttribute.parameterType != AnimatorParameterAttribute.ParameterType.None
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
            && (int)animatorController.GetParameter(index).type != (int)animatorParameterAttribute.parameterType);
#else
            && (int)controller.parameters[index].type != (int)animatorParameterAttribute.parameterType);
#endif
                 
    }

    public override bool CheckCorrect(string selected)
    {
        if (controller == null)
        {
            LogError("Unable to find the controller animation.");
            return false;
        }
        else
            return base.CheckCorrect(selected);
    }

    public override string[] GetOptions()
    {
        List<string> paramList = new List<string>();

        if (controller != null)
        {
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
            int parameterCount = controller.parameterCount;
#else
            int parameterCount = controller != null ? controller.parameters.Length : 0;
#endif

            if (parameterCount == 0)
            {
                LogError("AnimationParamater is 0", controller);
                //property.stringValue = string.Empty;
                //DefaultInspector(position, property, label);
            }
            else
            {

                for (int i = 0; i < parameterCount; i++)
                {
                    if (CanAddEventName(controller, i))
                    {
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
                        paramList.Add(controller.GetParameter(i).name);
#else
                        paramList.Add(controller.parameters[i].name);
#endif
                    }

                }
                if (paramList.Count == 0)
                {
                    LogError("AnimationParamater type(" + animatorParameterAttribute.parameterType + ") is 0", controller);
                    //property.stringValue = string.Empty;
                    //DefaultInspector(position, property, label);
                }
            }
        }
        return paramList.ToArray();
    }

    public override string GetSourceName()
    {
        return animatorParameterAttribute.SourceName;
    }

    AnimatorParameterAttribute animatorParameterAttribute
    {
        get
        {
            return (AnimatorParameterAttribute)attribute;
        }
    }

}
