using System;
using UnityEngine;

/// <summary>
/// Label for enumeration or elements enumeration 
/// </summary>
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class EnumLabelAttribute : PropertyBaseAttribute
{
    public string label = "";
    /// <summary>
    /// Label for enumeration or elements enumeration 
    /// </summary>
    /// <param name="label">Text for label. The text can be set by attribute PropertyArgs.label</param>
    public EnumLabelAttribute(string label = "")
    {
        if (!string.IsNullOrEmpty(label))
            this.label = label;
    }
}