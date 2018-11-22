using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(HelpAttribute))]
public class HelpDrawer : DecoratorDrawer
{

    private int helpHeight = 50;
    private int textHeight = 16;
    private int HeaderY = 2;

    HelpAttribute helpAttribute
    {
        get
        {
            return (HelpAttribute)attribute;
        }
    }

    public override void OnGUI(Rect position)
    {
        if (!string.IsNullOrEmpty(helpAttribute.HelpMessageText))
        {
            position.y += HeaderY;
            position.height = helpHeight;

            EditorGUI.HelpBox(position, helpAttribute.HelpMessageText, MessageType.Info);
        }
    }

    public override float GetHeight()
    {
        return helpHeight + (string.IsNullOrEmpty(helpAttribute.HelpMessageText) ? 0 : textHeight);
    }
}