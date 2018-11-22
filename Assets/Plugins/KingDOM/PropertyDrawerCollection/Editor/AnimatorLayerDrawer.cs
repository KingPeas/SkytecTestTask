using System;
using System.Collections.Generic;
using KingDOM.Util;
using UnityEditor;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;


[CustomPropertyDrawer(typeof(AnimatorLayerAttribute))]
public class AnimatorLayerDrawer : AnimatorHashDrawer
{
    
    public override string[] GetOptions()
    {
        List<string> paramList = new List<string>();

        if (controller != null)
        {
            AnimatorControllerLayer[] layers = controller.layers;
            foreach (AnimatorControllerLayer layer in layers)
            {
                paramList.Add(layer.name);
            }
        }
        return paramList.ToArray();
    }

    public override string GetSourceName()
    {
        return animatorLayerAttribute.SourceName;
    }

    private AnimatorLayerAttribute animatorLayerAttribute
    {
        get
        {
            return (AnimatorLayerAttribute)attribute;
        }
    }

}