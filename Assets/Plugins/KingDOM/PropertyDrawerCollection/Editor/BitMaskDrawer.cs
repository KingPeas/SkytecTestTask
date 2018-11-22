using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

[CustomPropertyDrawer(typeof(BitMaskAttribute))]
public class BitMaskDrawer : PropertyBaseDrawer
{
    private Dictionary<int, int> ind2val;
    private string[] options;
    private int[] itemValues;
    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        SetBaseContent(label);
        if (!string.IsNullOrEmpty(bitMaskAttribute.label))
            label.text = bitMaskAttribute.label;

        Type t = GetDataType();
        if (property.propertyType == SerializedPropertyType.Integer)
        {
            t = bitMaskAttribute.type;
            if (t == null)
            {
                LogError("Please setup type for BiteMask", property.serializedObject.targetObject);
                return;
            }
        }
        if (!t.IsEnum)
        {
            LogError("BiteMask need enum type.", property.serializedObject.targetObject);
            return;
        }

        GetOptions(t);

        EditorGUI.BeginChangeCheck();
        int maskVal = PropertyVal2Mask(property.intValue);

        int newMaskVal = EditorGUI.MaskField(position, label, maskVal, options);

        if (EditorGUI.EndChangeCheck())
        {
            property.intValue = Mask2PropertyVal(newMaskVal, maskVal ^ newMaskVal, property.intValue);
        }
    }

    private void GetOptions(Type enumType)
    {
        options = new string[0];
        ind2val = new Dictionary<int, int>();
        itemValues = System.Enum.GetValues(enumType) as int[];
        string[] enumNames = Enum.GetNames(enumType);
        options = new string[enumNames.Length];
        for (int i = 0; i < enumNames.Length; i++)
        {
            if (itemValues[i] == 0) continue;
            FieldInfo field = enumType.GetField(enumNames[i]);
            if (field == null) continue;
            EnumLabelAttribute[] attrs = (EnumLabelAttribute[])field.GetCustomAttributes(typeof(EnumLabelAttribute), false);

            if (attrs.Length != 0)
            {
                foreach (EnumLabelAttribute labelAttribute in attrs)
                {
                    if (!string.IsNullOrEmpty(labelAttribute.label))
                    {
                        options[i] = labelAttribute.label;
                    }

                }
            }
            if (string.IsNullOrEmpty(options[i]))
            {
                options[i] = enumNames[i];
            }
            ind2val.Add(i, itemValues[i]);
        }
    }

    private int PropertyVal2Mask(int val)
    {
        int maskVal = 0;
        bool all = true; 
        for (int i = 0; i < itemValues.Length; i++)
        {
            if (itemValues[i] != 0)
            {
                if ((val & itemValues[i]) == itemValues[i])
                    maskVal |= 1 << i;
                else
                {
                    all = false;
                }
            }
            
        }
        if (all) maskVal = -1;
        return maskVal;
    }

    private int Mask2PropertyVal(int mask,int changes, int val)
    {
        if (mask == 0) return 0;
        bool all = mask == -1;
        for (int i = 0; i < itemValues.Length; i++)
        {
            //if ((all || (mask | 1 << i) != 0) && ind2val.ContainsKey(i))
            //    val |= ind2val[i];
            if ((changes & (1 << i)) != 0)            // has this list item changed?
            {
                if ((mask & (1 << i)) != 0)     // has it been set?
                {
                    if (itemValues[i] == 0)           // special case: if "0" is set, just set the val to 0
                    {
                        val = 0;
                        break;
                    }
                    else
                        val |= itemValues[i];
                }
                else                                  // it has been reset
                {
                    val &= ~itemValues[i];
                }
            }
        }

        return val;
    }

    protected override bool IsSupported(SerializedProperty property)
    {
        if (property.propertyType == SerializedPropertyType.Enum) return true;
        if (property.propertyType == SerializedPropertyType.Integer && bitMaskAttribute.type != null && bitMaskAttribute.type.IsEnum && bitMaskAttribute.type.IsDefined(typeof(FlagsAttribute), false))
        {
            return true;
        }

        return false;
    }

    private BitMaskAttribute bitMaskAttribute
    {
        get
        {
            return (BitMaskAttribute)attribute;
        }
    }

}
