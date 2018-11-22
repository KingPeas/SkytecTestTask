using UnityEngine;
using System.Collections;

/// <summary>
/// Splitter to separate areas
/// </summary>
public class SplitterAttribute : DecoratorBaseAttribute
{
    /// <summary>
    /// Line height
    /// </summary>
    public float height;
    /// <summary>
    /// Split line
    /// </summary>
    /// <param name="height">Line height</param>
    public SplitterAttribute(float height = 2)
    {
        this.height = height;
    }
}
