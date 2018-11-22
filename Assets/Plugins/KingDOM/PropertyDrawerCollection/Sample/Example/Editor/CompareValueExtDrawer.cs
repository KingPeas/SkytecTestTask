using UnityEditor;
using UnityEngine;
using System.Collections;

[CustomPropertyDrawer(typeof(CompareValueExt))]
public class CompareValueExtDrawer : PropertyDrawer {

	
	override public void OnGUI (Rect position, SerializedProperty property, GUIContent label) {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.

	    SerializedProperty propName = property.FindPropertyRelative("property");
        EditorGUI.BeginProperty(position, label, property);

        // Draw label
        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

        // Don't make child fields be indented
        int indent = EditorGUI.indentLevel;
        EditorGUI.indentLevel = 0;

        // Calculate rects
        Rect fieldRect = new Rect(position.x, position.y, 150, position.height);
        Rect compareRect = new Rect(position.x + 160, position.y, 45, position.height);
        Rect valueRect = new Rect(position.x + 210, position.y, 100, position.height);

        // Draw fields - passs GUIContent.none to each so they are drawn without labels
        EditorGUI.PropertyField(fieldRect, propName, GUIContent.none);
	    SerializedProperty p = property.FindPropertyRelative("compare");
        EditorGUI.PropertyField(compareRect, p, GUIContent.none);
        if (p.enumValueIndex != ((int)CompareValueExt.CompareType.None) && p.enumValueIndex != ((int)CompareValueExt.CompareType.NotEmpty))
        EditorGUI.PropertyField(valueRect, property.FindPropertyRelative("propertyValue"), GUIContent.none);

        // Set indent back to what it was
        EditorGUI.indentLevel = indent;

        EditorGUI.EndProperty();
	}

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return EditorGUIUtility.singleLineHeight;
    }
}
