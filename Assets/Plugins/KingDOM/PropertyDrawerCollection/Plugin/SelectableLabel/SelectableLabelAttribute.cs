using UnityEngine;

/// <summary>
/// Label with the text available for selection
/// </summary>
public class SelectableLabelAttribute : DecoratorBaseAttribute
{
    public string text;
    /// <summary>
    /// Label with the text available for selection
    /// </summary>
    /// <param name="text">Text for the label</param>
    public SelectableLabelAttribute(string text)
    {
        this.text = text;
    }
}