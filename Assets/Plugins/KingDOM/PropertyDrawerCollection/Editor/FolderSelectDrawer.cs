using System.IO;
using KingDOM;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FolderSelectAttribute))]
public class FolderSelectDrawer : PopupTextEdit
{
    FolderSelectAttribute FolderSelectAttribute
    {
        get
        {
            return (FolderSelectAttribute)attribute;
        }
    }

    public override void ShowMenu(SerializedProperty property)
    {
		string folder = property.stringValue;
		string sel = "";
		folder = string.IsNullOrEmpty(folder) ? "" : Path.GetDirectoryName (Path.GetFullPath(folder) + Path.DirectorySeparatorChar);
		if (string.IsNullOrEmpty(folder))
						folder = FolderSelectAttribute.folder;
		if (FolderSelectAttribute.dialog == FolderSelectAttribute.DialogType.Load)
        {
            sel = EditorUtility.OpenFolderPanel(FolderSelectAttribute.title, folder,
                FolderSelectAttribute.defaultName);
        }
        else
        {
            sel = EditorUtility.SaveFolderPanel(FolderSelectAttribute.title, folder,
                FolderSelectAttribute.defaultName);
        }
		if (!string.IsNullOrEmpty (sel)) 
		{
            selectVal = FolderSelectAttribute.absolute ? KingUtil.AbsolutePath(sel) : KingUtil.RelativePath(sel);
            selectPath = property.propertyPath;
		}
    }

    public override void SetValue(SerializedProperty property, string option)
    {
        string res = option;
        property.stringValue = !string.IsNullOrEmpty(res) ? res : option;
    }

    public override bool CheckCorrect(string selected)
    {
		return Directory.Exists(selected) || Directory.Exists (KingUtil.AppPath ("") + Path.DirectorySeparatorChar + selected);
    }

    protected override string FindOption(string selected)
    {
        return selected;
    }

    public override string[] GetOptions()
    {
        return new string[1];
    }

}