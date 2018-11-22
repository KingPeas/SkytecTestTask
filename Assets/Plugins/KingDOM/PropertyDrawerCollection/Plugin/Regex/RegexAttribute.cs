using UnityEngine;
using System.Text.RegularExpressions;

/// <summary>
/// Input field with text mask 
/// </summary>
public class RegexAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// The regular expression pattern to match.
    /// </summary>
    public readonly string pattern;
    /// <summary>
    /// Explanatory message
    /// </summary>
    public readonly string helpMessage;
    /// <summary>
    /// Input field with text mask 
    /// </summary>
    /// <param name="pattern">The regular expression pattern to match.</param>
    /// <param name="helpMessage">Explanatory message</param>
    public RegexAttribute(string pattern, string helpMessage)
    {
        this.pattern = pattern;
        this.helpMessage = helpMessage;
    }
}