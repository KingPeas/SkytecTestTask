using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(CompactAttribute))]
public class CompactDrawer : PropertyBaseDrawer
{
#if UNITY_4_0 || UNITY_4_1 || UNITY_4_2 || UNITY_4_3 || UNITY_4_5 || UNITY_4_6
    private const string tVector2 = "Vector2f";
    private const string tVector3 = "Vector3f";
    private const string tVector4 = "Vector4f";
    private const string tRect = "Rectf";
    private const string tQuaternion = "Quaternionf";
    private const string tBounds = "AABB";
#else
    private const string tVector2 = "Vector2";
    private const string tVector3 = "Vector3";
    private const string tVector4 = "Vector4";
    private const string tRect = "Rect";
    private const string tQuaternion = "Quaternion";
    private const string tBounds = "Bounds";
#endif
    public override void Draw(Rect position, SerializedProperty property, GUIContent label)
    {
        //EditorGUIUtility.LookLikeControls();
        EditorGUI.BeginChangeCheck();
        switch (property.type)
        {
            case tVector2:
                {
                    Vector2 v2 = EditorGUI.Vector2Field(position, label.text, property.vector2Value);
                    if (EditorGUI.EndChangeCheck())
                    {
                        property.vector2Value = v2;
                    }
                    break;
                }
            case tVector3:
                {
                    Vector3 v3 = EditorGUI.Vector3Field(position, label.text, property.vector3Value);
                    if (EditorGUI.EndChangeCheck())
                    {
                        property.vector3Value = v3;
                    }
                    break;
                }
            case tVector4:
                {
                    float x = GetProperty(property, "x").floatValue;
                    float y = GetProperty(property, "y").floatValue;
                    float z = GetProperty(property, "z").floatValue;
                    float w = GetProperty(property, "w").floatValue;
                    Vector4 v4 = EditorGUI.Vector4Field(position, label.text, new Vector4(x, y, z, w));
                    if (EditorGUI.EndChangeCheck())
                    {
                        GetProperty(property, "x").floatValue = v4.x;
                        GetProperty(property, "y").floatValue = v4.y;
                        GetProperty(property, "z").floatValue = v4.z;
                        GetProperty(property, "w").floatValue = v4.w;
                    }
                    break;
                }
            case tRect:
                {
                    Rect r = property.rectValue = EditorGUI.RectField(position, label, property.rectValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        property.rectValue = r;
                    }
                    break;
                }
            case tQuaternion:
                {
                    Quaternion q = property.quaternionValue;
                    Vector4 v4 = EditorGUI.Vector4Field(position, label.text, new Vector4(q.x, q.y, q.z, q.w));
                    if (EditorGUI.EndChangeCheck())
                    {
                        property.quaternionValue = new Quaternion(v4.x, v4.y, v4.z, v4.w);
                    }
                    break;
                }
            case tBounds:
                {
                    Bounds b = EditorGUI.BoundsField(position, label, property.boundsValue);
                    if (EditorGUI.EndChangeCheck())
                    {
                        property.boundsValue = b;
                    }
                    break;
                }
            default:
                {
                    EditorGUI.LabelField(position, label.text, "Not Implemented");
                    EditorGUI.EndChangeCheck();
                    break;
                }
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        if (CheckHide(property)) return 0f;
        return GetHeight(property);
    }

    private const float SingleLineHeight = 16f;

    private static float GetHeight(SerializedProperty property)
    {
        float height = 0;
        switch (property.type)
        {
            case tVector2:
            case tVector3:
                height = EditorGUIUtility.wideMode ? SingleLineHeight : SingleLineHeight * 2;
                break;
            case tVector4:
            case tQuaternion:
                height = SingleLineHeight * 2;
                break;
            case tRect:
                height = SingleLineHeight * (EditorGUIUtility.wideMode ? 2 : 3);
                break;
            case tBounds:
                height = SingleLineHeight * 3;
                break;
            default:
                height = SingleLineHeight;
                break;
        }
        return height;
    }

    private static SerializedProperty GetProperty(SerializedProperty property, string name)
    {
        return property.FindPropertyRelative(name);
    }
}