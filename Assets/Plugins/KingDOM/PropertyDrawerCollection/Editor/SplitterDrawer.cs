using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(SplitterAttribute))]
public class SplitterDrawer : DecoratorDrawer
{
    private const float space = 8.0f;
    public override void OnGUI(Rect position)
    {
        position.y += space / 2;

        position.height = splitterAttribute.height;
        GUI.Box(position, "");
    }

    public override float GetHeight()
    {
        return splitterAttribute.height + space;
    }

    SplitterAttribute splitterAttribute
    {
        get
        {
            return (SplitterAttribute)attribute;
        }
    }
}
