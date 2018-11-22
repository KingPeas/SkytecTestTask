using System;
using UnityEngine;

/// <summary>
/// Execution methods list when the value in the editor changes. Methods will be invoked only when the user changes the value in the editor.
/// </summary>
public class ObserveAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// Execution methods list.
    /// </summary>
    public string[] callbackNames;
    /// <summary>
    /// Execution methods list when the value in the editor changes. Methods will be invoked only when the user changes the value in the editor.
    /// </summary>
    /// <param name="callbackNames"> Execution methods list </param>
    [Obsolete("Use the list of calling the attribute PropertyArgs in conjunction with an attribute inherited from the PropertyBase")]
    public ObserveAttribute(params string[] callbackNames)
    {
        this.callbackNames = callbackNames;
    }
}