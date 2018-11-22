using System.IO;
using KingDOM;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class SceneNameDrawer : PopupTextEdit
{
    private SceneNameAttribute sceneNameAttribute
    {
        get
        {
            return (SceneNameAttribute)attribute;
        }
    }

    private const string LEVEL_FILE_EXT = ".unity";
    private string[] sceneNames = new string[0];

    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        if (sceneNames == null || sceneNames.Length == 0) ResearchScenes();
        base.Draw(position, property, label);
    }

    public override void ShowMenu(SerializedProperty property)
    {
        var menu = new GenericMenu();
        string[] options = GetOptions();
        foreach (string s in options)
        {
            string localS = s;
            int idx = s.IndexOf('(');
            string disp = (idx > 0)
                ? localS.Substring(0, idx) + localS.Substring(idx).Replace("/", "\\")
                : localS.Replace("/", "\\");
            menu.AddItem(new GUIContent(ToDisplay(disp)),
                         false,
                         () =>
                         {
                             selectVal = localS;
                             selectPath = property.propertyPath;
                         }
                         );

        }
        menu.ShowAsContext();
    }

    public override string[] GetOptions()
    {
        
        return sceneNames;
    }

    /// <summary>
    /// Get only the name without additional information
    /// </summary>
    /// <param name="fullName">Full name with additional information</param>
    /// <returns>Only the value of</returns>
    virtual protected string GetOnlyName(string fullName)
    {
        int idx = fullName.IndexOf('(');
        string name = idx > 0? fullName.Substring(0, idx): fullName;
        return name;
    }


    void ResearchScenes()
    {
        
        switch (sceneNameAttribute.SelectType)
        {
            case SceneNameAttribute.SceneSelectType.EnabledOnly:
            case SceneNameAttribute.SceneSelectType.ActiveOnly:
                HashSet<string> sceneNames = new HashSet<string>();
                List<EditorBuildSettingsScene> scenes = (sceneNameAttribute.SelectType == SceneNameAttribute.SceneSelectType.EnabledOnly ? EditorBuildSettings.scenes.Where(scene => scene.enabled) : EditorBuildSettings.scenes).ToList();
                scenes.ForEach(scene =>
                {
                    sceneNames.Add(scene.path.Substring(scene.path.LastIndexOf("/") + 1).Replace(".unity", string.Empty));
                });
                this.sceneNames = sceneNames.ToArray();
                break;
            case SceneNameAttribute.SceneSelectType.AllInProject:
                string[] guids = AssetDatabase.FindAssets("t:Scene");
                this.sceneNames = new string[guids.Length];
                for (int i = 0; i < guids.Length; i++)
                {
                    string path = AssetDatabase.GUIDToAssetPath(guids[i]);
                    path = path.Substring(0, path.LastIndexOf(".unity"));
                    this.sceneNames[i] = string.Format("{0} ({1})", Path.GetFileNameWithoutExtension(path), path);
                }  
               break;
            default:
                this.sceneNames = new string[0];
                break;
        }
    }
    
}