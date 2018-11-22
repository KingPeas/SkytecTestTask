using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(HeaderAttribute))]
public class HeaderDrawer : DecoratorDrawer
{
    const float TextPad = 3;
    public override void OnGUI(Rect position)
    {
        GUIContent labl = new GUIContent(headerAttribute.text);
        //DrawHeader
        EditorGUI.LabelField(position, labl, headerStyle);

    }

    public override float GetHeight()
    {
        return headerAttribute.size + TextPad * 2;
    }

    HeaderAttribute headerAttribute
    {
        get
        {
            return (HeaderAttribute)attribute;
        }
    }

    private GUIStyle headerStyle
    {
        get
        {
            GUIStyle style = new GUIStyle();
            style.fontStyle = (FontStyle)Enum.ToObject(typeof(FontStyle), (headerAttribute.bold ? (int)FontStyle.Bold: 0)  + (headerAttribute.italic ? (int)FontStyle.Italic: 0));
            style.fontSize = (int)(headerAttribute.size);
            style.normal.textColor = getColor();
            return style;
        }
    }

    private Color getColor()
    {
        if (!string.IsNullOrEmpty(headerAttribute.color))
        {
            var color = typeof (Color).GetProperty(headerAttribute.color, BindingFlags.Public | BindingFlags.Static);
            if (color != null)
            {
                var colorVal = color.GetValue(null, null);
                if (colorVal != null && colorVal is Color)
                {
                    return (Color)colorVal;
                }
            }
        }
        return EditorGUIUtility.isProSkin ? new Color(0.7f, 0.7f, 0.7f, 1f) : new Color(0.4f, 0.4f, 0.4f, 1f);
    }
}