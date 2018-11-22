using System;
using UnityEditor;
using UnityEngine;
using System.Collections;

public class WindowUsePropertyDrawer : EditorWindow
{
    // Register a menu item and the functions performed by opening the window
    //[MenuItem("KingDOM/Window VS PropertyDrawer")]
    static void CreateWindow()
    {
        WindowUsePropertyDrawer window = GetWindow<WindowUsePropertyDrawer>();
        window.titleContent = new GUIContent("PropertyDrawer");
        for (int i = 0; i < window.setups.Length; i++)
        {
            window.setups[i] = new CompareValueExt();
        }
    }

    public bool EventToPause = false;

    [Range(0, 10)]
    public int z = 3;

    [PropertyBase]
    [PropertyArgs(label = "Event", tip = "Name of your event")]
    public string EventName = "";

    [TypePopup(parentType=typeof(Camera))]
    [PropertyArgs(tip = "Select type.")]
    public string script = "";

    public CompareValueExt setup = new CompareValueExt();
    
    public CompareValueExt[] setups = new CompareValueExt[5];

    // Output
    public void OnGUI()
    {
        GUILayout.BeginVertical();
        SerializedObject so = new SerializedObject(this);
        so.Update();
        SerializedProperty sp = so.FindProperty("z");
        EditorGUILayout.PropertyField(sp, new GUIContent("Step"));
        EventToPause = EditorGUILayout.Toggle(new GUIContent("Event Pause"), EventToPause);
        EditorGUILayout.PropertyField(so.FindProperty("EventName"));
        EditorGUILayout.PropertyField(so.FindProperty("script"));
        EditorGUILayout.PropertyField(so.FindProperty("setup"));
        EditorGUILayout.PropertyField(so.FindProperty("setups"), true);
        GUILayout.EndVertical();
        so.ApplyModifiedProperties();
    }
	
}
