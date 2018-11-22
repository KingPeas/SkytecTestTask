using System.IO;
using KingDOM;
using UnityEngine;

/// <summary>
/// A file selection dialogue
/// </summary>
public class FileSelectAttribute : PropertyBaseAttribute
{
    public enum DialogType
    {
        Load,
        Save
    }
    public DialogType dialog = DialogType.Load;
    public string title = "";
    public string directory = "";
    public string extensions = "";
    public string defaultName = "";
    public bool absolute = false;
    /// <summary>
    /// Field to select the path to the file
    /// </summary>
    /// <param name="dialog">Dialogue type</param>
    /// <param name="title">Dialogue caption</param>
    /// <param name="directory">Default directory</param>
    /// <param name="extensions">File extensions</param>
    /// <param name="defaultName">Default file name</param>
    public FileSelectAttribute(DialogType dialog, string title = "", string directory = "", string extensions = "", string defaultName = "")
    {
        this.dialog = dialog;
        if (!string.IsNullOrEmpty(title)) this.title = title;

        if (!string.IsNullOrEmpty (directory))
						this.directory = directory;
				else
						this.directory = KingUtil.AppPath("");

        if (!string.IsNullOrEmpty(extensions)) this.extensions = extensions;
        if (!string.IsNullOrEmpty(defaultName)) this.defaultName = defaultName;
    }
    
}