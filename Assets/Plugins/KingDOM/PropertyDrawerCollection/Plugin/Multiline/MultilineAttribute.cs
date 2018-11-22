using UnityEngine;

/// <summary>
/// Multiline input field
/// </summary>
public class MultilineAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// The number of rows to display
    /// </summary>
    public int lines;
    /// <summary>
    /// Multiline input field
    /// </summary>
    /// <param name="lines">The number of rows to display</param>
    public MultilineAttribute(int lines = 3)
    {
        this.lines = lines;
    }
}