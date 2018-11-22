using UnityEngine;

/// <summary>
/// Drop-down list of the constants
/// </summary>
public class PopupAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// Drop-down list of the constants
    /// </summary>
    public object[] list;
    /// <summary>
    /// Drop-down list of the constants
    /// </summary>
    /// <param name="list">Constants list</param>
    public PopupAttribute (params object[] list)
    {
        this.list = list;
    }
}