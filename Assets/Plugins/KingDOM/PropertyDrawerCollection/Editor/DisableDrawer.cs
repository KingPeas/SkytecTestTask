using KingDOM.Util;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(DisableAttribute))]
public class DisableDrawer : PropertyBaseDrawer
{
    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginDisabledGroup(true);
        EditorExt.PropertyField(position, property, label, fieldInfo);
        EditorGUI.EndDisabledGroup();
    }
}