using UnityEngine;

/// <summary>
/// The range of values
/// </summary>
public class RangeAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// The minimum allowed value.
    /// </summary>
    public float left;
    /// <summary>
    /// The maximum allowed value.
    /// </summary>
    public float right;
    /// <summary>
    /// Increment value
    /// </summary>
    public float step;
    /// <summary>
    /// The range of values
    /// </summary>
    /// <param name="left">The minimum allowed value.</param>
    /// <param name="right">The maximum allowed value.</param>
    /// <param name="step"> Increment value</param>
    public RangeAttribute(float left, float right, float step = 0)
    {
        this.left = left;
        this.right = right;
        this.step = Mathf.Abs(step);
    }
}