using System;
using UnityEngine;
/// <summary>
///  Constant selection from class
/// </summary>
public class ConstSelectAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// Parent type class
    /// </summary>
    public Type className;
    public ConstSelectAttribute(Type className)
    {
        this.className = className;
    }

}