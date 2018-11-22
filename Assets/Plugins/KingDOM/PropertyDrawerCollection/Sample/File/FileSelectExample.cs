using UnityEngine;
using System.Collections;

public class FileSelectExample : MonoBehaviour
{
	[FileSelect(FileSelectAttribute.DialogType.Load)]
	public string dialog;
    [FileSelect(FileSelectAttribute.DialogType.Load, "Loading file", "", "txt")]
    public string dialog2;
    [FileSelect(FileSelectAttribute.DialogType.Save, "Saving file", "", "txt")]
    [PropertyArgs("Dialog", "Here you can also select the path to the file.")]
    public string[] DialogTest;
	
}
