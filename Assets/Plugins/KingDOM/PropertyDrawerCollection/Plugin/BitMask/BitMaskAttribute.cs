using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// List for enumeration with multyselect 
/// </summary>
[AttributeUsage(AttributeTargets.Enum | AttributeTargets.Field)]
public class BitMaskAttribute:PropertyBaseAttribute  {

    public string label = "";
    public Type type = null;
    /// <summary>
    /// Label for enumeration or elements enumeration 
    /// </summary>
    /// <param name="label">Text for label. The text can be set by attribute PropertyArgs.label</param>
    public BitMaskAttribute(string label = "")
    {
        if (!string.IsNullOrEmpty(label))
            this.label = label;
    }
    /// <summary>
    /// Label for enumeration or elements enumeration 
    /// </summary>
    /// <param name="label">Text for label. The text can be set by attribute PropertyArgs.label</param>
    public BitMaskAttribute(Type type, string label = "")
    {
        if (type != null)
            this.type = type;
        if (!string.IsNullOrEmpty(label))
            this.label = label;
    }
}
