using UnityEngine;
using UnityEditor;
using System;

[CustomPropertyDrawer(typeof(PopupAttribute))]
public class PopupDrawer : PropertyBaseDrawer
{

    private Action<int> setValue;
    private Func<int, int> validateValue;
    private GUIContent[] _list = null;

    private GUIContent[] list
    {
        get
        {
            if (_list == null)
            {
                _list = new GUIContent[popupAttribute.list.Length];
                for (int i = 0; i < popupAttribute.list.Length; i++)
                {
                    _list[i] = new GUIContent(popupAttribute.list[i].ToString());
                }
            }
            return _list;
        }
    }

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        if (validateValue == null && setValue == null)
            SetUp(property);


        if (validateValue == null && setValue == null)
        {
            base.OnGUI(position, property, label);
            return;
        }

        int selectedIndex = 0;

        for (int i = 0; i < list.Length; i++)
        {
            selectedIndex = validateValue(i);
            if (selectedIndex != 0)
                break;
        }

        EditorGUI.BeginChangeCheck();
        selectedIndex = EditorGUI.Popup(position, label, selectedIndex, list);
        if (EditorGUI.EndChangeCheck())
        {
            setValue(selectedIndex);
        }
    }

    void SetUp(SerializedProperty property)
    {
        if (variableType == typeof(string))
        {

            validateValue = (index) =>
            {
                return property.stringValue == list[index].text ? index : 0;
            };

            setValue = (index) =>
            {
                property.stringValue = list[index].text;
            };
        }
        else if (variableType == typeof(int))
        {

            validateValue = (index) =>
            {
                return property.intValue == Convert.ToInt32(list[index].text) ? index : 0;
            };

            setValue = (index) =>
            {
                property.intValue = Convert.ToInt32(list[index].text);
            };
        }
        else if (variableType == typeof(float))
        {
            validateValue = (index) =>
            {
                return property.floatValue == Convert.ToSingle(list[index].text) ? index : 0;
            };
            setValue = (index) =>
            {
                property.floatValue = Convert.ToSingle(list[index].text);
            };
        }

    }

    PopupAttribute popupAttribute
    {
        get { return (PopupAttribute)attribute; }
    }

    private Type variableType
    {
        get
        {
            return popupAttribute.list[0].GetType();
        }
    }
}