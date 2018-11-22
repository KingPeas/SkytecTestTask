using System;
using System.Linq;
using System.Reflection;
//using System.Runtime.CompilerServices;
using KingDOM.Util;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(PropertyBaseAttribute))]
public class PropertyBaseDrawer : PropertyDrawer
{
    private PropertyArgsAttribute _args = null;
    private bool hasError = false;

    public PropertyArgsAttribute args
    {
        get
        {
            if (_args == null)
            {
                setArgs();
            }
            return _args;
        }
    }

    PropertyBaseAttribute PropertyBaseAttribute
    {
        get
        {
            return (PropertyBaseAttribute)attribute;
        }
    }

    //[Obsolete("Use method Draw")]
    public sealed override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        try
        {

            label = SetBaseContent(label);

            if (CheckHide(property)) return;

            /*if (property.isArray == true)// && GetDataType() == typeof(string))
            {
                ReorderableListGUI.Title(position,label);
                //ReorderableListGUI.ListField(property, ReorderableListGUI.CalculateListFieldHeight(property));
            }

            else*/
            if (!IsSupported(property))
            {
                OutNotSupported(property, position, label);
            }
            else
            {
                EditorGUI.BeginChangeCheck();
                Draw(position, property, label);
                if (EditorGUI.EndChangeCheck())
                {
                    EndChange(property);
                }
            }
            label.tooltip = "";
        }
        catch (Exception exception)
        {
            LogError(exception.Message, property.serializedObject.targetObject);
        }
    }

    public bool CheckHide(SerializedProperty property)
    {
        if (!string.IsNullOrEmpty(args.hideIfEmpty))
        {
            SerializedProperty p = EditorExt.GetPropertyByName(property, args.hideIfEmpty);
            if (p != null)
            {
                switch (p.propertyType)
                {
                    case SerializedPropertyType.Boolean:
                        return p.boolValue == false;
                    case SerializedPropertyType.Enum:
                        return p.enumValueIndex == 0;
                    case SerializedPropertyType.Float:
                        return Math.Abs(p.floatValue) <= float.Epsilon;
                    case SerializedPropertyType.Integer:
                        return p.intValue == 0;
                    case SerializedPropertyType.ObjectReference:
                        return p.objectReferenceValue == null;
                    case SerializedPropertyType.String:
                        return string.IsNullOrEmpty(p.stringValue);
                    default:
                        LogError(string.Format("Can't ({0}) hide Property ({1}): No support for this type of property.", property.serializedObject.targetObject, property.name), property.serializedObject.targetObject);
                        break;
                }
            }
        }
        return false;
    }

    public GUIContent SetBaseContent(GUIContent label, MemberInfo propertyInfo = null)
    {
        //if (propertyInfo == null || args == null)
        setArgs(propertyInfo);
        //if (!string.IsNullOrEmpty(PropertyBaseAttribute.tip))
        label.tooltip = args.tip;

        if (!string.IsNullOrEmpty(args.label) && label != GUIContent.none)
            label.text = args.label;

        if (args.noLabel) label = GUIContent.none;

        return label;
    }
    private void setArgs(MemberInfo propertyInfo = null)
    {
        if (propertyInfo == null) propertyInfo = this.fieldInfo;
        PropertyArgsAttribute args = new PropertyArgsAttribute();
        object[] propertyAttributes = propertyInfo.GetCustomAttributes(typeof(PropertyArgsAttribute), true);
        foreach (object propertyAttribute in propertyAttributes)
        {
            PropertyArgsAttribute a = (PropertyArgsAttribute)propertyAttribute;
            if (!string.IsNullOrEmpty(a.tip))
                args.tip = a.tip;

            if (!string.IsNullOrEmpty(a.label))
                args.label = a.label;

            if (a.lines > 0)
                args.lines = a.lines;

            args.noLabel = a.noLabel;
            if (a.callbacks != null)
                args.callbacks = args.callbacks == null ? a.callbacks : args.callbacks.Concat(a.callbacks).ToArray();
            if (!string.IsNullOrEmpty(a.hideIfEmpty))
                args.hideIfEmpty = a.hideIfEmpty;
        }
        _args = args;
    }

    public virtual void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUIUtility.LookLikeControls();
        EditorExt.PropertyField(position, property, label, fieldInfo, property.hasVisibleChildren && property.isExpanded);
    }

    public void EndChange(SerializedProperty property)
    {
        if (IsMonoBehaviour(property))
        {

            MonoBehaviour mono = (MonoBehaviour)property.serializedObject.targetObject;

            if (args.callbacks != null && mono != null)
            {
                foreach (var callbackName in args.callbacks)
                {
                    try
                    {
                        mono.Invoke(callbackName, 0);
                    }
                    catch (Exception exception)
                    {
                        LogError(exception, mono);
                    }
                }
            }
        }
    }

    bool IsMonoBehaviour(SerializedProperty property)
    {
        if (property == null) return false;
        return property.serializedObject.targetObject.GetType().IsSubclassOf(typeof(MonoBehaviour));
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (CheckHide(property)) return 0f;
        
        if (args != null && args.lines > 0)
            return EditorGUIUtility.singleLineHeight * args.lines;

        return EditorExt.PropertyHeight(property);
        /*
        if (property.hasVisibleChildren)
            return EditorExt.CalculatePropertyHeight(EditorGUIUtility.singleLineHeight, property);
        else
            return  EditorExt.PropertyHeight(property);//*/
    }

    protected Type GetDataType()
    {
        Type type = this.fieldInfo.FieldType;
        if (type.IsArray)
            type = type.GetElementType();
        return type;
    }

    virtual protected bool IsSupported(SerializedProperty property)
    {
        return true;
    }

    virtual protected void OutNotSupported(SerializedProperty property, Rect position, GUIContent label)
    {
        Color oldColor = GUI.color;
        GUI.color = Color.gray;
        EditorGUI.LabelField(position, label, new GUIContent("No support for this type of property."));
        GUI.color = oldColor;
        LogError(string.Format("Object ({0}) Property ({1}): No support for this type of property.", property.serializedObject.targetObject, label.text), property.serializedObject.targetObject);
        return;
    }

    protected void LogError(object error, UnityEngine.Object source = null)
    {
        if (hasError) return;
        Debug.LogError(error, source);
        hasError = true;
    }
}
