using System;
using System.Collections.Generic;
using KingDOM.Util;
using UnityEditor;
using System.Linq;
using UnityEditor.Animations;
using UnityEngine;
using KingDOM;

public class AnimatorHashDrawer : PopupTextEdit {

    protected AnimatorController controller = null;

    public override void Draw(Rect position, UnityEditor.SerializedProperty property, GUIContent label)
    {
        controller = GetAnimatorController(property);

        base.Draw(position, property, label);
    }

    /// <summary>
    /// Gets the animator controller.
    /// </summary>
    /// <returns>
    /// The animator controller.
    /// </returns>
    /// <param name='property'>
    /// Property.
    /// </param>

    AnimatorController GetAnimatorController(SerializedProperty property)
    {
        Component component = null;
        string sourceName = GetSourceName();

        if (string.IsNullOrEmpty(sourceName))
        {
            component = property.serializedObject.targetObject as Component;
        }
        else
        {
            SerializedProperty source = EditorExt.GetPropertyByName(property, sourceName);
            if (source != null)
                component = source.objectReferenceValue as Component;
            if (component == null) component = KingUtil.GetPropertyValue<Animator>(property.serializedObject.targetObject, sourceName);

        }
        if (component == null)
        {
            LogError("Couldn't cast targetObject", property.serializedObject.targetObject);
            return null;
        }

        Animator anim = component.GetComponent<Animator>();
        if (anim == null)
        {
            LogError("Missing Animator Component", property.serializedObject.targetObject);
            return null;
        }

        AnimatorController animatorController = anim.runtimeAnimatorController as AnimatorController;
        return animatorController;
    }

    virtual public string GetSourceName()
    {
        return null;
    }

    protected string GetFullName(string level, string subLevel)
    {
        if (string.IsNullOrEmpty(level)) return subLevel;
        if (string.IsNullOrEmpty(subLevel)) return level;
        return string.Format("{0}.{1}", level, subLevel);
    }

    protected override string GetOnlyName(string fullName)
    {
        return fullName;
    }

    public override void SetValue(SerializedProperty property, string option)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                property.intValue = Animator.StringToHash(option);
                break;
            case SerializedPropertyType.String:
                property.stringValue = option;
                break;
        }
    }

    public override string GetValue(SerializedProperty property)
    {
        switch (property.propertyType)
        {
            case SerializedPropertyType.Integer:
                string[] states = GetOptions();
                foreach (string state in states)
                {
                    if (Animator.StringToHash(state) == property.intValue)
                    {
                        return state;
                    }
                }
                break;
            case SerializedPropertyType.String:
                return property.stringValue;
                break;
        }
        return "";
    }

    protected override bool IsSupported(SerializedProperty property)
    {
        return property.propertyType == SerializedPropertyType.String || property.propertyType == SerializedPropertyType.Integer;
    }

}
