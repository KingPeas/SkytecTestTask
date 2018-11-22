using UnityEngine;
/// <summary>
/// Helping message for property
/// </summary>
public class HelpAttribute : DecoratorBaseAttribute
{
    /// <summary>
    /// Helping text
    /// </summary>
    public string HelpMessageText;
    /// <summary>
    /// Helping message for property
    /// </summary>
    /// <param name="text">Helping text</param>
    public HelpAttribute(string text)
    {
        this.HelpMessageText = text;
    }
}