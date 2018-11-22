using System;
using UnityEngine;
using System.Collections;

public class ComponentSelectAttribute : PropertyBaseAttribute
{

	/// <summary>
    /// Parent type class
    /// </summary>
    public Type className = typeof(Component);
    public ComponentSelectAttribute(Type className)
    {
        if (className != null)
            this.className = className;
    }
    public ComponentSelectAttribute()
    {
    }
}
