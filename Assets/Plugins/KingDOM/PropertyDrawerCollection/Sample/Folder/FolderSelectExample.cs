using UnityEngine;
using System.Collections;

public class FolderSelectExample : MonoBehaviour
{
	[FolderSelect(FolderSelectAttribute.DialogType.Load)]
	public string dialog;
    [FolderSelect(FolderSelectAttribute.DialogType.Load, "Open from folder", "", "txt")]
    public string dialog2;
    [FolderSelect(FolderSelectAttribute.DialogType.Save, "Save to folder", "", "txt")]
    [PropertyArgs("Dialog", "Here you can also select the path to the folder.")]
    public string[] DialogTest;
	
}
