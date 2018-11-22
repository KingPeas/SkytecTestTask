using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(MultilineAttribute))]
public class MultilineDrawer : PropertyBaseDrawer
{
    MultilineAttribute multilineAttribute
    {
        get
        {
            return (MultilineAttribute)attribute;
        }
    }

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        position.height = EditorGUIUtility.singleLineHeight;
        Rect r = EditorGUI.PrefixLabel(position, label);
        r.height = EditorGUIUtility.singleLineHeight*multilineAttribute.lines;
        property.stringValue = EditorGUI.TextArea(r, property.stringValue);
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (CheckHide(property)) return 0f;
        return EditorGUIUtility.singleLineHeight * multilineAttribute.lines;//(multilineAttribute.lines + 1);
    }

}