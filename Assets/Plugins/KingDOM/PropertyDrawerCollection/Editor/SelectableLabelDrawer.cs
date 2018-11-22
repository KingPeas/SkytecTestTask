using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SelectableLabelAttribute))]
public class SelectableLabelDrawer : DecoratorDrawer
{
    public override void OnGUI(Rect position)
    {
        EditorGUI.SelectableLabel(position, selectableLabelAttribute.text);
    }

    private SelectableLabelAttribute selectableLabelAttribute
    {
        get
        {
            return (SelectableLabelAttribute)attribute;
        }
    }

    public override float GetHeight()
    {
        int i = selectableLabelAttribute.text.Split('\n').Length;
        return (i > 0 ? i: 0 + 1) * EditorGUIUtility.singleLineHeight;
    }
}