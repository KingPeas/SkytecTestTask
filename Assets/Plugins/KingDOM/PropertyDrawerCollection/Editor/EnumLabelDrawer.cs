using System;
using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

[CustomPropertyDrawer(typeof(EnumLabelAttribute))]
public class EnumLabelDrawer : PropertyBaseDrawer
{
    protected Dictionary<string, GUIContent> customEnumNames = new Dictionary<string, GUIContent>();

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        SetUpCustomEnumNames(property, property.enumNames);

        SetBaseContent(label);
        if (!string.IsNullOrEmpty(enumLabelAttribute.label))
            label.text = enumLabelAttribute.label;

        if (property.propertyType == SerializedPropertyType.Enum)
        {
            EditorGUI.BeginChangeCheck();
            GUIContent[] displayedOptions = property.enumNames
                    .Where(enumName => customEnumNames.ContainsKey(enumName))
                    .Select<string, GUIContent>(enumName => customEnumNames[enumName])
                    .ToArray();
            GUIContent[] options = new GUIContent[displayedOptions.Length];
            for (int i = 0; i < displayedOptions.Length; i++)
            {
                options[i] = new GUIContent(displayedOptions[i]);
            }
            int selectedIndex = EditorGUI.Popup(position, label, property.enumValueIndex, options);
            if (EditorGUI.EndChangeCheck())
            {
                property.enumValueIndex = selectedIndex;
            }
        }
    }

    private EnumLabelAttribute enumLabelAttribute
    {
        get
        {
            return (EnumLabelAttribute)attribute;
        }
    }

    public void SetUpCustomEnumNames(SerializedProperty property, string[] enumNames)
    {
        /*Type type = property.serializedObject.targetObject.GetType();
        foreach (FieldInfo fieldInfo in type.GetFields(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public))
        {
            object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(EnumLabelAttribute), false);
            foreach (EnumLabelAttribute customAttribute in customAttributes)
            {
                Type enumType = fieldInfo.FieldType;

                foreach (string enumName in enumNames)
                {
                    FieldInfo field = enumType.GetField(enumName);
                    if (field == null) continue;
                    EnumLabelAttribute[] attrs = (EnumLabelAttribute[])field.GetCustomAttributes(customAttribute.GetType(), false);

                    if (!customEnumNames.ContainsKey(enumName))
                    {
                        foreach (EnumLabelAttribute labelAttribute in attrs)
                        {
                            GUIContent label = new GUIContent(labelAttribute.label);
                            label = SetBaseContent(label, field);
                            customEnumNames.Add(enumName, label);
                        }
                    }
                }
            }
        }*/
        if (customEnumNames.Count > 0)
            return;


        //FieldInfo fieldInfo = this.fieldInfo;//type.GetField(property.name,
        //BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
        Type type = this.GetDataType();
        if (type != null)//fieldInfo != null)			                                
        {
            Type enumType = type;//fieldInfo.FieldType;
            if (enumType.IsEnum)
            {

                foreach (string enumName in enumNames)
                {
                    FieldInfo field = enumType.GetField(enumName);
                    if (field == null) continue;
                    EnumLabelAttribute[] attrs = (EnumLabelAttribute[])field.GetCustomAttributes(typeof(EnumLabelAttribute), false);

                    if (!customEnumNames.ContainsKey(enumName))
                    {
                        if (attrs.Length == 0)
                        {
                            GUIContent label = new GUIContent(enumName);
                            customEnumNames.Add(enumName, label);
                        }
                        else
                        {
                            foreach (EnumLabelAttribute labelAttribute in attrs)
                            {
                                GUIContent label = new GUIContent(labelAttribute.label);
                                label = SetBaseContent(label, field);
                                customEnumNames.Add(enumName, label);

                            }
                        }
                    }
                }
            }
        }
    }
}