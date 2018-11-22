using System;
using UnityEditor;
using UnityEngine;
using System.Collections;
using KingDOM.Util;

[CustomPropertyDrawer(typeof(ComponentSelectAttribute))]
public class ComponentSelestDrawer : PropertyBaseDrawer
{
    ComponentSelectAttribute ComponentSelectAttribute
    {
        get
        {
            return (ComponentSelectAttribute)attribute;
        }
    }

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        Color oldColor = GUI.color;

        position.height = EditorGUIUtility.singleLineHeight;
        Rect r = EditorGUI.PrefixLabel(position, label);
        EditorGUI.BeginChangeCheck();
        r.width -= EditorGUIUtility.singleLineHeight * 2;
        //var result = property.objectReferenceValue;

        /*if (!CheckCorrect(result))
        {
            GUI.color = Color.grey;
        }      */

        /*if (!Editable())
        {
            GUI.enabled = false;
        }  */

        //result = GUI.TextField(r, result, EditorStyles.textField);
        Type type = ComponentSelectAttribute.className;
        if (type != null)
            EditorGUI.ObjectField(r, property, type, GUIContent.none);//result = GUI.TextField(r, result, EditorStyles.textField);
        else
            EditorGUI.ObjectField(r, property, GUIContent.none);
        //property.objectReferenceValue = result;
        GUI.color = oldColor;
        GUI.enabled = true;

        /*if (EditorGUI.EndChangeCheck())
        {
            SetValue(property, result);
        }        */
        r.x += r.width;
        r.width = EditorGUIUtility.singleLineHeight + 2;

        if (PDButton.IconButton(r, PDButton.ICON_LIST))
        {
            ShowMenu(property);
        }

        r.x += r.width;
        r.width = EditorGUIUtility.singleLineHeight + 2;

        if (PDButton.IconButton(r, PDButton.ICON_ERASE))
        {
            Clear(property);
        }
        /*if (GetOptions().Length == 0)
        {
            position.y += EditorGUIUtility.singleLineHeight;
            r = EditorGUI.PrefixLabel(position, new GUIContent(" "));

            EditorGUI.LabelField(position, " ", "Failed to get the list of values.");
            //LogError("Failed to get the list of values.");
            return;
        } */

        /*if (!string.IsNullOrEmpty(selectVal) && selectPath == property.propertyPath)
        {
            SetValue(property, selectVal);
            selectVal = null;
            selectPath = null;
        }   */
    }
    /// <summary>
    /// Show menu to choose from. The pop-up menu sets the selected value and the selected property.
    /// </summary>
    /// <param name="property">Serialized property</param>
    virtual public void ShowMenu(SerializedProperty property)
    {
        ComponentSelectWindow.Init(property, ComponentSelectAttribute.className);
    }
    /// <summary>
    /// Clear the value stored in the property
    /// </summary>
    /// <param name="property">Serialized property</param>
    virtual public void Clear(SerializedProperty property)
    {
        property.objectReferenceValue = null;
    }
}
