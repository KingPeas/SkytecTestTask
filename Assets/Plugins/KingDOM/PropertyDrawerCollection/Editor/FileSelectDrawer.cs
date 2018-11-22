using System.IO;
using KingDOM;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(FileSelectAttribute))]
public class FileSelectDrawer : PopupTextEdit
{
    FileSelectAttribute FileSelectAttribute
    {
        get
        {
            return (FileSelectAttribute)attribute;
        }
    }

    public override void ShowMenu(SerializedProperty property)
    {
        string directory = property.stringValue;
        string sel = "";
        directory = KingUtil.AbsolutePath(directory);
        if (string.IsNullOrEmpty(directory))
            directory = FileSelectAttribute.directory;
        if (FileSelectAttribute.dialog == FileSelectAttribute.DialogType.Load)
        {
            sel = EditorUtility.OpenFilePanel(FileSelectAttribute.title, directory,
                FileSelectAttribute.extensions);
        }
        else
        {
            sel = EditorUtility.SaveFilePanel(FileSelectAttribute.title, directory,
                FileSelectAttribute.defaultName, FileSelectAttribute.extensions);
        }

        if (!string.IsNullOrEmpty(sel))
        {
            selectVal = FileSelectAttribute.absolute ? KingUtil.AbsolutePath(sel): KingUtil.RelativePath(sel);
            selectPath = property.propertyPath;
        }
    }

    public override bool CheckCorrect(string selected)
    {
        return File.Exists(KingUtil.AbsolutePath(selected));
    }
    protected override string FindOption(string selected)
    {
        return selected;
    }

    public override string[] GetOptions()
    {
        return new string[1];
    }

    public override void SetValue(SerializedProperty property, string option)
    {
        string res = option;
        property.stringValue = !string.IsNullOrEmpty(res) ? res : option;
    }

}
