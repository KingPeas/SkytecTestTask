using UnityEngine;

/// <summary>
///  Name of the scene selection
/// </summary>
public class SceneNameAttribute : PropertyBaseAttribute
{
    public enum SceneSelectType
    {
        AllInProject,
        EnabledOnly,
        ActiveOnly
    }
    /// <summary>
    /// Show only active
    /// </summary>
    public bool enableOnly = true;

    public SceneSelectType SelectType = SceneSelectType.AllInProject;
    /// <summary>
    /// Scene selection from the list added to the BuildSettings
    /// </summary>
    /// <param name="enableOnly">Show only active</param>
    public SceneNameAttribute(bool enableOnly = true)
    {
        if (enableOnly)
        {
            SelectType = SceneSelectType.EnabledOnly;
        }
        else
        {
            SelectType = SceneSelectType.ActiveOnly;
        }
    }

    public SceneNameAttribute(SceneSelectType selectType)
    {
        SelectType = selectType;
    }
}