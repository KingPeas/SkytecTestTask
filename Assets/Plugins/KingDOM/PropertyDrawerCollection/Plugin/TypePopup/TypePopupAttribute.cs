using System;
using UnityEngine;

/// <summary>
/// Drop-down list to select the type name
/// </summary>
public class TypePopupAttribute : PropertyBaseAttribute
{
    //public int selectedValue = 0;
    //public string type = "";
    public Type parentType = typeof (MonoBehaviour);

    /// <summary>
    /// Drop-down list to select the type name
    /// </summary>
    /// <param name="parentType">Type name of parent</param>
    public TypePopupAttribute(Type parentType)
    {
        if (parentType != null)
            this.parentType = parentType;
    }

    public TypePopupAttribute()
    {
        
    }
}