using UnityEngine;

public class SceneNameExample : MonoBehaviour
{
	[SceneName]
    [PropertyArgs(tip = "Here you can select only active scenes added in BuildSettings.")]
	public string sceneName;

    [SceneName(false)]
    [PropertyArgs(tip = "Here you can select all the scenes added in BuildSettings.")]
    public string sceneName2;
    [SceneName(SceneNameAttribute.SceneSelectType.AllInProject)]
    [PropertyArgs(tip = "Here you can select all the scenes added in BuildSettings.")]
    public string sceneName3;
}
