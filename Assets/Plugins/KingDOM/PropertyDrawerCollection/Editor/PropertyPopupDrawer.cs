using System;
using KingDOM;
using KingDOM.Util;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(PropertyPopupAttribute))]
public class PropertyPopupDrawer : PopupTextEdit
{
    private PropertyPopupAttribute propertyPopupAttribute
    {
        get
        {
            return (PropertyPopupAttribute)attribute;
        }
    }

    public string Target
    {
        get { return target; }
        set
        {
            /*if (value != propertyPopupAttribute.target)
            {
                propertyPopupAttribute.selectedValue = -1;
            }*/
            target = value;
            //propertyPopupAttribute.target = value;
        }
    }

    private string target = "";

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {

        SerializedProperty prop = null;
        if (propertyPopupAttribute.sourceType == PropertyPopupAttribute.SourceType.TypeName)
        {
            Target = propertyPopupAttribute.sourcePropertyName;
        }
        else
        {
            if (propertyPopupAttribute.sourceType == PropertyPopupAttribute.SourceType.CalculatedType)
            {
                PropertyInfo pInfo = property.serializedObject.targetObject.GetType().GetProperty(propertyPopupAttribute.sourcePropertyName);
                if (pInfo != null)
                {
                    Type type = pInfo.GetValue(property.serializedObject.targetObject, null) as Type;
                    if (type != null)
                    {
                        Target = type.AssemblyQualifiedName;
                    }
                    else
                    {
                        LogError(string.Format("Can't calculate Property ({1}) in object({0}).", property.serializedObject.targetObject, label.text), property.serializedObject.targetObject);
                    }
                }
            }
            else if (!string.IsNullOrEmpty(propertyPopupAttribute.sourcePropertyName))
            {
                //SerializedObject obj = property.serializedObject;
                //prop = obj.FindProperty(propertyPopupAttribute.sourcePropertyName);
                prop = EditorExt.GetPropertyByName(property, propertyPopupAttribute.sourcePropertyName);
            }

            if (propertyPopupAttribute.sourceType == PropertyPopupAttribute.SourceType.FieldTarget)
            {
                if (!string.IsNullOrEmpty(propertyPopupAttribute.sourcePropertyName))
                {
                    if (prop != null && prop.objectReferenceValue != null)
                        Target = prop.objectReferenceValue.GetType().AssemblyQualifiedName;
                }
                else
                {
                    Target = property.serializedObject.targetObject.GetType().AssemblyQualifiedName;
                }
            }

            if (propertyPopupAttribute.sourceType == PropertyPopupAttribute.SourceType.FieldTypeName)
            {
                if (prop != null)
                    Target = prop.stringValue;
            } 
            
        }
        
        base.Draw(position, property, label);
    }
    public override string[] GetOptions()
    {
        string[] propertyList = new string[0];

        if (!string.IsNullOrEmpty(Target))
        {
            Type target = KingUtil.GetType(Target);
            if (null != target)
            {
                List<string> list = new List<string>();
                foreach (FieldInfo field in target.GetFields())
                {
                    if (propertyPopupAttribute.canSet && field.IsInitOnly)
                        continue;
             
                    GUIContent fContent = SetBaseContent(new GUIContent(), field);
                    list.Add(field.Name + (fContent.text != "" ? "(" + fContent.text + ")": ""));
                }
                foreach (PropertyInfo property in target.GetProperties())
                {
                    if (propertyPopupAttribute.canSet && !property.CanWrite)
                        continue;

                    GUIContent pContent = SetBaseContent(new GUIContent(), property);
                    list.Add(property.Name + (pContent.text != "" ? "(" + pContent.text + ")" : ""));
                }
                list.Sort();
                propertyList = list.ToArray();
            }
        }
        return propertyList;
    }
    
}