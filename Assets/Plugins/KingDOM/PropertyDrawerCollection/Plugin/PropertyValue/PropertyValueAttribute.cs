using System;
using UnityEngine;

/// <summary>
/// Field for storing the allowable value. Can be used as the test value
/// </summary>
public class PropertyValueAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// Name of the property where type name is stored 
    /// </summary>
    public string sourceTypeName = "";
    /// <summary>
    /// Name of the property where property name for this type is stored 
    /// </summary>
    public string sourcePropertyName = "";
    /// <summary>
    /// Field for storing the allowable value. Can be used as the test value
    /// </summary>
    /// <param name="sourceTypeName">Name of the property where type name is stored</param>
    /// <param name="sourcePropertyName">Name of the property where property name for this type is stored </param>
    public PropertyValueAttribute(string sourceTypeName, string sourcePropertyName)
    {
        if (!string.IsNullOrEmpty(sourceTypeName))
            this.sourceTypeName = sourceTypeName;
        
        if (!string.IsNullOrEmpty(sourcePropertyName))
            this.sourcePropertyName = sourcePropertyName;
    }

}