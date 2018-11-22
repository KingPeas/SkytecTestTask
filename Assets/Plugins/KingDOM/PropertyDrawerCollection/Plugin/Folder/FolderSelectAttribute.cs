using KingDOM;
using UnityEngine;

/// <summary>
/// A folder selection dialogue
/// </summary>
public class FolderSelectAttribute : PropertyBaseAttribute
{
    public enum DialogType
    {
        Load,
        Save
    }
    public DialogType dialog = DialogType.Load;
    public string title = "";
    public string folder = "";
    public string defaultName = "";
    public bool absolute = false;
    /// <summary>
    /// Field to select the path to the folder
    /// </summary>
    /// <param name="dialog">Dialogue type</param>
    /// <param name="title">Dialogue caption</param>
    /// <param name="folder">Default directory</param>
    /// <param name="defaultName">Default folder name</param>
    public FolderSelectAttribute(DialogType dialog, string title = "", string folder = "", string defaultName = "")
    {
        this.dialog = dialog;
        if (!string.IsNullOrEmpty(title)) this.title = title;
        if (!string.IsNullOrEmpty(folder)) 
			this.folder = folder;
		else
			this.folder = KingUtil.AppPath("");
        if (!string.IsNullOrEmpty(defaultName)) this.defaultName = defaultName;
    }
    
}