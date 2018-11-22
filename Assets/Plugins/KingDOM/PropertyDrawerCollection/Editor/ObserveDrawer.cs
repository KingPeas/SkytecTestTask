using KingDOM.Util;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(ObserveAttribute))]
public class ObserveDrawer : PropertyBaseDrawer
{
    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();
        EditorExt.PropertyField(position, property, label, fieldInfo);
        if (EditorGUI.EndChangeCheck())
        {
            if (IsMonoBehaviour(property))
            {

                MonoBehaviour mono = (MonoBehaviour)property.serializedObject.targetObject;

                foreach (var callbackName in observeAttribute.callbackNames)
                {
                    mono.Invoke(callbackName, 0);
                }

            }
        }
    }

    bool IsMonoBehaviour(SerializedProperty property)
    {
        return property.serializedObject.targetObject.GetType().IsSubclassOf(typeof(MonoBehaviour));
    }

    ObserveAttribute observeAttribute
    {
        get
        {
            return (ObserveAttribute)attribute;
        }
    }
}