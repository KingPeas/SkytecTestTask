using KingDOM.Util;
using UnityEngine;
using UnityEditor;
using KingDOM;

[CustomPropertyDrawer(typeof(RangeAttribute))]
public class RangeDrawer : PropertyBaseDrawer
{
    RangeAttribute rangeAttribute
    {
        get
        {
            return (RangeAttribute)attribute;
        }
    }

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginChangeCheck();
        switch (property.type)
        {
            case "int":
                property.intValue = (int)getValue(position, label, property.intValue);
                break;
            case "float":
                property.floatValue = getValue(position, label, property.floatValue);
                break;
            default:
                EditorExt.PropertyField(position, property, label, fieldInfo);
                break;
        }

    }

    float getValue(Rect position, GUIContent label, float value)
    {
        float left = rangeAttribute.left;
        float right = rangeAttribute.right;
        float step = rangeAttribute.step;
        float newValue = EditorGUI.Slider(position, label, value, left, right);

        if (newValue != left && newValue != right && step > float.Epsilon)
        {

            newValue = newValue - left;
            newValue = KingUtil.Round(newValue, step);
            newValue = newValue + left;
            newValue = Mathf.Clamp(newValue, Mathf.Min(left, right), Mathf.Max(left, right));
        }

        return newValue;
    }

}
