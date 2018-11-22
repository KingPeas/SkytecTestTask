using System;
using System.Reflection;
using KingDOM;
using UnityEditor;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

public class ComponentSelectWindow : EditorWindow
{

    //private static SerializedProperty property = null;
    public Type parentType = null;
    public Object refObject = null;
    private string propertyName = null;
    public PropertyInfo PInfo = null;
    public FieldInfo FInfo = null;
    public Component Link = null;

    private static void Init(SerializedProperty property)
    {
        Init(property, null);
    }

    // Use this for initialization
    public static void Init(/*Object obj, */SerializedProperty property, Type parentType)
    {

        // Get existing open window or if none, make a new one:
        ComponentSelectWindow window = (ComponentSelectWindow)EditorWindow.GetWindow(typeof(ComponentSelectWindow));
        window.Show();
        if (property == null/* || obj == null*/) return;
        window.refObject = property.serializedObject.targetObject;
        string propertyName = property.propertyPath;//KingUtil.GetOnlyName(property.name);
        window.propertyName = propertyName;
        //window.FInfo = t.GetField(propertyName);
        //window.PInfo = t.GetProperty(propertyName);

        if (parentType != null)
        {
            window.parentType = parentType;
        }
        else
        {
            window.parentType = Type.GetType(property.type);
        }
    }

    void OnGUI()
    {
        if (refObject != null)
        {
            GUI.enabled = false;
            EditorGUILayout.TextField("Object", refObject.name);
            //if (PInfo != null) EditorGUILayout.TextField("Property", PInfo.Name);
            //if (FInfo != null) EditorGUILayout.TextField("Field", FInfo.Name);
            if (!string.IsNullOrEmpty(propertyName)) EditorGUILayout.TextField("Property", propertyName);
            GUI.enabled = true;
            Link = (Component)EditorGUILayout.ObjectField("Link", Link, parentType, true);
        }
        else
        {
              EditorGUILayout.LabelField("Not linked property.");
        }
        EditorGUILayout.BeginHorizontal();
        if (GUILayout.Button("Set"))
            SetValue();
        if (GUILayout.Button("Cancel"))
            this.Close();

        EditorGUILayout.EndHorizontal();
    }

    void SetValue()
    {
        //if (FInfo != null) FInfo.SetValue(refObject, Link);
        //if (PInfo != null) PInfo.SetValue(refObject, Link, null);
        SerializedObject so = new SerializedObject(refObject);
        SerializedProperty sp = so.FindProperty(propertyName);
        if (sp != null)
            sp.objectReferenceValue = Link;
        //so.Update();
        so.ApplyModifiedProperties();
        //FInfo = null;
        //PInfo = null;
        refObject = null;
        Close();
    }
}
