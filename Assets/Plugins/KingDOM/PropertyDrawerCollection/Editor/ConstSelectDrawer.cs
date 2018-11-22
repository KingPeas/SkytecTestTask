using System;
using System.Collections;
using System.Reflection;
using UnityEditor;

[CustomPropertyDrawer(typeof(ConstSelectAttribute))]
public class ConstSelectDrawer : PopupTextEdit
{
    ConstSelectAttribute ConstSelectAttribute
    {
        get
        {
            return (ConstSelectAttribute)attribute;
        }
    }

    public override string[] GetOptions()
    {
        Type type = ConstSelectAttribute.className;
        Type typeData = GetDataType();
        ArrayList constants = new ArrayList();

        FieldInfo[] fieldInfos = type.GetFields(
            // Gets all public and static fields

            BindingFlags.Public | BindingFlags.Static |
            // This tells it to get the fields from all base types as well

            BindingFlags.FlattenHierarchy);

        // Go through the list and only pick out the constants
        foreach (FieldInfo fi in fieldInfos)
            // IsLiteral determines if its value is written at 
            //   compile time and not changeable
            // IsInitOnly determine if the field can be set 
            //   in the body of the constructor
            // for C# a field which is readonly keyword would have both true 
            //   but a const field would have only IsLiteral equal to true
            if (fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeData)
                constants.Add(fi.Name + " (" + fi.GetValue(null) + ")");

        // Return an array of FieldInfos
        return (string[])constants.ToArray(typeof(string));
    }

    public override bool CheckCorrect(string selected)
    {
        FieldInfo fi = FieldInfoByName(selected);
        return fi != null;
        //return base.CheckCorrect(selected);
    }

    public override string GetValue(SerializedProperty property)
    {
        object selectValue;
        selectValue = SerializedPropertyValueExt.Value(property);
        FieldInfo fi = FieldInfoByValue(selectValue);
        if (fi != null) return fi.Name;
        return "";
    }

    public override void SetValue(SerializedProperty property, string option)
    {
        FieldInfo fi = FieldInfoByName(option);

        if (fi != null)
        {
            SerializedPropertyValueExt.SetValue(property, fi.GetValue(null));
        }

    }

    /*protected override string ToDisplay(string selected)
    {
        FieldInfo fi = FieldInfoByName(selected);
        if (fi != null)
        {
            return fi.Name + "(" + fi.GetValue(null) + ")";
        }
        return "";
    }*/

    private FieldInfo FieldInfoByValue(object value)
    {
        Type type = ConstSelectAttribute.className;
        Type typeData = GetDataType();
        FieldInfo[] fieldInfos = type.GetFields(
            // Gets all public and static fields

            BindingFlags.Public | BindingFlags.Static |
            // This tells it to get the fields from all base types as well

            BindingFlags.FlattenHierarchy);

        // Go through the list and only pick out the constants
        foreach (FieldInfo fi in fieldInfos)
            if (fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeData)
            {
                object constValue = fi.GetValue(null);
                if (constValue.Equals(value)) return fi;
            }
        return null;
    }

    private FieldInfo FieldInfoByName(string name)
    {
        name = GetOnlyName(name);
        Type type = ConstSelectAttribute.className;
        Type typeData = GetDataType();
        FieldInfo[] fieldInfos = type.GetFields(
            // Gets all public and static fields

            BindingFlags.Public | BindingFlags.Static |
            // This tells it to get the fields from all base types as well

            BindingFlags.FlattenHierarchy);

        // Go through the list and only pick out the constants
        foreach (FieldInfo fi in fieldInfos)
            if (fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeData)
                if (fi.Name == name)
                {
                    return fi;
                }
        return null;
    }

    protected override bool Editable()
    {
        return false;
    }

    protected override bool IsSupported(SerializedProperty property)
    {
        return true;
    }
}