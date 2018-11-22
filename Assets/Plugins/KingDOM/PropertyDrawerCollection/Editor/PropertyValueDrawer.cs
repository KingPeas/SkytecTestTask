using System;
using KingDOM;
using KingDOM.Util;
using UnityEngine;
using UnityEditor;
using System.Reflection;

[CustomPropertyDrawer(typeof(PropertyValueAttribute))]
public class PropertyValueDrawer : PropertyBaseDrawer
{
    //Type lastTypeProperty = null;
    private PropertyValueAttribute propertyValueAttribute
    {
        get
        {
            return (PropertyValueAttribute)attribute;
        }
    }

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        string errorMsg = "";

        if (string.IsNullOrEmpty(propertyValueAttribute.sourceTypeName))
        {
            errorMsg = "Not specified object type as the source.";
        }
        else if (string.IsNullOrEmpty(propertyValueAttribute.sourcePropertyName))
        {
            errorMsg = "There is not setting the name property of the object as a source.";
        }
        else
        {

            //object obj = property.serializedObject.targetObject;
            SerializedProperty prop = EditorExt.GetPropertyByName(property, propertyValueAttribute.sourceTypeName);
            string typeName = prop == null ? null: prop.stringValue;

            if (string.IsNullOrEmpty(typeName) || KingUtil.GetType(typeName) == null)
            {
                errorMsg = "Failed to determine the type of object.";
            }
            else
            {
                Type typeObj = KingUtil.GetType(typeName);
                SerializedProperty prp = EditorExt.GetPropertyByName(property, propertyValueAttribute.sourcePropertyName);
                string propertyName = prp == null ? null : prp.stringValue; //GetPropertyValue(obj, propertyValueAttribute.sourcePropertyName);

                if (string.IsNullOrEmpty(propertyName))
                {
                    errorMsg = "Failed  to determine the property as the source.";
                }
                else
                {
                    Type typeProperty = null;

                    propertyName = KingUtil.GetOnlyName(propertyName);
                    FieldInfo field = typeObj.GetField(propertyName);
                    if (field != null)
                    {
                        typeProperty = field.FieldType;
                    }
                    else
                    {
                        PropertyInfo pInfo = typeObj.GetProperty(propertyName);
                        if (pInfo != null)
                        {
                            typeProperty = pInfo.PropertyType;
                        }
                    }

                    if (typeProperty == null)
                    {
                        LogError("Failed to determine the type property of the source.");
                        return;
                    }
                    else
                    {
                        try
                        {
                            //if (lastTypeProperty != null && lastTypeProperty != typeProperty)
                            //    property.stringValue = "";

                            if (typeProperty == typeof(int))
                            {
                                if (string.IsNullOrEmpty(property.stringValue))
                                    property.stringValue = "0";
                                int i = Convert.ToInt32(property.stringValue);
                                i = EditorGUI.IntField(position, label, i);
                                property.stringValue = Prop2Str(i);
                            }
                            else
                                if (typeProperty == typeof(float))
                                {
                                    if (string.IsNullOrEmpty(property.stringValue))
                                        property.stringValue = 0f.ToString();
                                    float f = Convert.ToSingle(property.stringValue);
                                    f = EditorGUI.FloatField(position, label, f);
                                    property.stringValue = Prop2Str(f);
                                }
                                else
                                    if (typeProperty == typeof(string))
                                    {
                                        property.stringValue = EditorGUI.TextField(position, label, property.stringValue);
                                    }
                                    else
                                        if (typeProperty == typeof(bool))
                                        {
                                            if (string.IsNullOrEmpty(property.stringValue))
                                                property.stringValue = bool.FalseString;

                                            bool b = Convert.ToBoolean(property.stringValue);
                                            b = EditorGUI.Toggle(position, label, b);
                                            property.stringValue = Prop2Str(b);
                                        }
                                        else if (typeProperty.IsEnum)
                                        {
                                            if (string.IsNullOrEmpty(property.stringValue))
                                                property.stringValue = Enum.GetValues(typeProperty).GetValue(0).ToString();

                                            int e = (int)Enum.Parse(typeProperty, property.stringValue);

                                            object o = EditorGUI.EnumPopup(position, label, Enum.ToObject(typeProperty, e) as Enum);
                                            e = (int)o;
                                            property.stringValue = Prop2Str(Enum.Parse(typeProperty, e.ToString()));
                                        }
                                        else
                                        {
                                            errorMsg = "No support."; 
                                        }
                        }
                        catch (Exception)
                        {
                            property.stringValue = "";
                        }
                        
                        //lastTypeProperty = typeProperty;
                    }
                    
                }

                

                
            }
            
        }

        if (!string.IsNullOrEmpty(errorMsg))
        {
            Rect rect = EditorGUI.PrefixLabel(position, label);
            Color oldColor = GUI.color;
            GUI.color = Color.grey;
            EditorGUI.LabelField(rect, errorMsg);
            GUI.color = oldColor;
        }
        
    }


    string Prop2Str(object obj)
    {
        return obj.ToString();
    }

    string GetPropertyValue(object obj, string propName)
    {
        Type type = obj.GetType();
        string result = "";
        FieldInfo field = type.GetField(propName);
        if (field != null && field.FieldType == typeof(string))
        {
            result = field.GetValue(obj) as string;
        }
        else
        {
            PropertyInfo pInfo = type.GetProperty(propName);
            if (pInfo != null)
            {
                result = pInfo.GetValue(propName, null) as string;
            }
        }

        return result;
    }
    
}