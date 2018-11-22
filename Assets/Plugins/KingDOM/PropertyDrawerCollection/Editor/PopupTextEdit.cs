using System;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;
using System.Collections;
using KingDOM.Util;

public class PopupTextEdit :PropertyBaseDrawer
{
    protected string selectVal = null;
    protected string selectPath = null;
    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        Color oldColor = GUI.color;
        
        position.height = EditorGUIUtility.singleLineHeight;
        Rect r = EditorGUI.PrefixLabel(position, label);
        EditorGUI.BeginChangeCheck();
        r.width -= EditorGUIUtility.singleLineHeight * 2;
        string result = GetValue(property);

        if (!CheckCorrect(result))
        {
            GUI.color = Color.grey;
        }

        if (!Editable())
        {
            GUI.enabled = false;
        }

        result = GUI.TextField(r, ToDisplay(result), EditorStyles.textField);//result = GUI.TextField(r, result, EditorStyles.textField);
        GUI.color = oldColor;
        GUI.enabled = true;

        if (EditorGUI.EndChangeCheck())
        {
            SetValue(property, result);
        }
		r.x += r.width;
		r.width = EditorGUIUtility.singleLineHeight + 2;

		if (PDButton.IconButton(r, PDButton.ICON_LIST))
		{
			ShowMenu(property);
		}
		
		r.x += r.width;
        r.width = EditorGUIUtility.singleLineHeight + 2;

        if (PDButton.IconButton(r, PDButton.ICON_ERASE))
        {
            Clear(property);
        }
        if (GetOptions().Length == 0)
        {
            position.y += EditorGUIUtility.singleLineHeight;
            r = EditorGUI.PrefixLabel(position, new GUIContent(" "));

            EditorGUI.LabelField(position, " ", "Failed to get the list of values.");
            //LogError("Failed to get the list of values.");
            return;
        }

		if (!string.IsNullOrEmpty(selectVal) && selectPath == property.propertyPath)
        {
			SetValue(property, selectVal);
            selectVal = null;
            selectPath = null;
        }

    }
    /// <summary>
    /// Show menu to choose from. The pop-up menu sets the selected value and the selected property.
    /// </summary>
    /// <param name="property">Serialized property</param>
    virtual public void ShowMenu(SerializedProperty property)
    {
        var menu = new GenericMenu();
        string[] options = GetOptions();
        foreach (string s in options)
        {
            string localS = s;
            int idx = s.IndexOf('(');
            string disp = (idx > 0)
                ? localS.Substring(0, idx).Replace(".", "/") + localS.Substring(idx)
                : localS.Replace(".", "/");
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

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (CheckHide(property)) return 0f;
        return EditorGUIUtility.singleLineHeight * (GetOptions().Length > 0 ? 1 : 2) + 2;
    }
    /// <summary>
    /// Clear the value stored in the property
    /// </summary>
    /// <param name="property">Serialized property</param>
    virtual public void Clear(SerializedProperty property)
    {
        property.stringValue = "";
    }
    /// <summary>
    /// Get text display of the property
    /// </summary>
    /// <param name="property">Serialized property</param>
    /// <returns>Results to display</returns>
    virtual public string GetValue(SerializedProperty property)
    {
        return property.stringValue;
    }
    /// <summary>
    /// Set the value of
    /// </summary>
    /// <param name="property">Serialized property</param>
    /// <param name="option">Selected value</param>
    virtual public void SetValue(SerializedProperty property, string option)
    {
        //string[] options = GetOptions();
        //int idx = -1;
        string res = GetOnlyName(FindOption(option));
        property.stringValue = !string.IsNullOrEmpty(res) ? res : option;
        
    }
    /// <summary>
    /// Get only the name without additional information
    /// </summary>
    /// <param name="fullName">Full name with additional information</param>
    /// <returns>Only the value of</returns>
    virtual protected string GetOnlyName(string fullName)
    {
        Regex reg = new Regex(@"^[\w_.]*");
        return reg.Match(fullName).Value;
    }
    /// <summary>
    /// Check the correctness of the value
    /// </summary>
    /// <param name="selected">Value for a check</param>
    /// <returns>Check result</returns>
    virtual public bool CheckCorrect(string selected)
    {
        return !string.IsNullOrEmpty(FindOption(selected));
    }
    /// <summary>
    /// List of available options
    /// </summary>
    /// <returns>List of option names</returns>
    virtual public string[] GetOptions()
    {
        return new string[0];
    }
    /// <summary>
    /// Find option
    /// </summary>
    /// <param name="selected">Searched value</param>
    /// <returns>Found value or null</returns>
    virtual protected string FindOption(string selected)
    {
        string[] options = GetOptions();
        selected = GetOnlyName(selected);

        return Array.Find(options, r => GetOnlyName(r) == selected);
    }
    /// <summary>
    /// Value to display
    /// </summary>
    /// <param name="selected">Value from a storage</param>
    /// <returns>To the mapping</returns>
    virtual protected string ToDisplay(string selected)
    {
        return selected;
    }
    
    /// <summary>
    /// Checking for support of the type of variable
    /// </summary>
    /// <param name="property">Serialized property</param>
    /// <returns>Check result</returns>
	override protected bool IsSupported(SerializedProperty property){
		return property.propertyType == SerializedPropertyType.String;
	}

    virtual protected bool Editable()
    {
        return true;
    }
}
