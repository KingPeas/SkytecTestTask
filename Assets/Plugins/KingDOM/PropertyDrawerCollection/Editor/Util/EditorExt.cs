using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.InteropServices;
using UnityEditor;
using UnityEngine;
using System.Collections;
using UnityEngineInternal;

namespace KingDOM.Util
{
    public static class EditorExt
    {
        /// <summary>
        /// The calculation of the height control in the editor with all the placed elements.
        /// </summary>
        /// <param name="position">Area rendering control.</param>
        /// <param name="prop">The property displayed in editor.</param>
        /// <returns>Property height</returns>
        public static float CalculatePropertyHeight(Rect position, SerializedProperty prop)
        {
            
            return CalculatePropertyHeight(position.height, prop);
        }
        /// <summary>
        /// The calculation of the height control in the editor with all the placed elements.
        /// </summary>
        /// <param name="heightProperty">высота области отрисовки.</param>
        /// <param name="prop">The property displayed in editor</param>
        /// <returns>Property height</returns>
        public static float CalculatePropertyHeight(float heightProperty, SerializedProperty prop)
        {
            float h = heightProperty;// + 2f;
            SerializedProperty serializedProperty = prop.Copy();
            SerializedProperty endProperty = serializedProperty.GetEndProperty();

            while (serializedProperty.NextVisible(serializedProperty.isExpanded) &&
                   !SerializedProperty.EqualContents(serializedProperty, endProperty))
            {
                h += EditorGUI.GetPropertyHeight(serializedProperty, (GUIContent) null, false);// + 2f;
            }
            return h;
        }
        /// <summary>
        /// The calculation of the area control in the editor with all the placed elements
        /// </summary>
        /// <param name="position">Area rendering control</param>
        /// <param name="prop">The property displayed in editor.</param>
        /// <returns>Drawing area with the placed elements.</returns>
        public static Rect CalculatePropertyPosition(Rect position, SerializedProperty prop)
        {
            float h = CalculatePropertyHeight(position, prop) - 2f;
            return new Rect(position.x, position.y, position.width, h);
        }
        /// <summary>
        /// Search properties by name of parent objects from the bottom of the hierarchy chain.
        /// </summary>
        /// <param name="property">Property for which the desired is the same level, or a property of the parent from the searched.</param>
        /// <param name="name"> The name of the searched properties.</param>
        /// <returns>Founded property, or null if the property is not found.</returns>
        public static SerializedProperty GetPropertyByName(SerializedProperty property, string name)
        {
            SerializedProperty ret = null;
            if (property == null || string.IsNullOrEmpty(name)) return null;
            SerializedObject obj = property.serializedObject;
            string path = property.propertyPath;
            string searchPath = path;
            string[] pathStep = path.Split('.');
            int skipCnt = 0;
            for (int i = pathStep.Length - 1; i >= 0 && ret == null; i--)
            {
                string step = pathStep[i];
                if (step.IndexOf('[') >= 0) skipCnt = 2;
                int idx = searchPath.LastIndexOf(step);
                if (idx > 0)
                {
                    searchPath = searchPath.Substring(0, idx);
                    if (skipCnt > 0)
                    {
                        skipCnt--;
                    }
                    else if (!string.IsNullOrEmpty(searchPath))
                    {
                        ret = obj.FindProperty(searchPath + name);
                    }
                }
            }
            if (ret == null) ret = obj.FindProperty(name);
            return ret;
        }

        public static float Indent()
        {
            return EditorGUI.indentLevel*15f;
        }

        public static void PropertyField(Rect position, SerializedProperty property, GUIContent label,
            FieldInfo fieldInfo,
            bool includeChildren = false)
        {
            var getUtil = Type.GetType("UnityEditor.ScriptAttributeUtility,UnityEditor");
            if (getUtil != null)
            {
                var getHandler = getUtil.GetMethod("GetHandler", BindingFlags.Static | BindingFlags.NonPublic);
                if (getHandler != null)
                {
                    object[] a = {property};
                    var handler = getHandler.Invoke(null, a);
                    {
                        var pf = handler.GetType().GetField("m_DecoratorDrawers", BindingFlags.NonPublic | BindingFlags.Instance);
                        if (pf != null)
                        {
                            var decorators = pf.GetValue(handler);
                            
                            try
                            {
                                if (decorators != null) 
                                    pf.SetValue(handler, null);
                                var gui = handler.GetType().GetMethod("OnGUI", BindingFlags.Public | BindingFlags.Instance);
                                if (gui != null)
                                {
                                    object[] args = { position, property, label, includeChildren };
                                    gui.Invoke(handler, args);
                                }
                            }
                            finally
                            {
                                if (decorators != null) 
                                    pf.SetValue(handler, decorators);
                            }
                        }
                    }
                
                }


            }
            return;
            
            //switch (property.propertyType)
            //{
            //    case SerializedPropertyType.Integer:
            //        property.intValue = EditorGUI.IntField(position, label, property.intValue);
            //        break;
            //    case SerializedPropertyType.Boolean:
            //        property.boolValue = EditorGUI.Toggle(position, label, property.boolValue);
            //        break;
            //    case SerializedPropertyType.Float:
            //        property.floatValue = EditorGUI.FloatField(position, label, property.floatValue);
            //        break;
            //    case SerializedPropertyType.String:
            //        property.stringValue = EditorGUI.TextField(position,label,property.stringValue);
            //        break;
                    
            //    case SerializedPropertyType.Color:
            //        property.colorValue = EditorGUI.ColorField(position, label, property.colorValue);
            //        break;
            //    case SerializedPropertyType.ObjectReference:
            //        property.objectReferenceValue = EditorGUI.ObjectField(position, label, property.objectReferenceValue, fieldInfo.FieldType);
            //        break;
            //    case SerializedPropertyType.LayerMask:
            //        property.intValue = EditorGUI.MaskField(position, label, property.intValue, GetLayers());
            //        break;
            //    case SerializedPropertyType.Enum:
            //        int length = property.enumNames.Length;
            //        GUIContent[] contents = new GUIContent[length];
            //        for (int i = 0; i < length; i++)
            //        {
            //            contents[i] = new GUIContent(property.enumNames[i]);
            //        }
            //        property.enumValueIndex = EditorGUI.Popup(position, label, property.enumValueIndex, contents);
            //        break;
            //    case SerializedPropertyType.Vector2:
            //        property.vector2Value = EditorGUI.Vector2Field(position,label, property.vector2Value);
            //        break;
            //    case SerializedPropertyType.Vector3:
            //        property.vector3Value = EditorGUI.Vector3Field(position,label, property.vector3Value);
            //        break;
            //    case SerializedPropertyType.Rect:
            //        property.rectValue = EditorGUI.RectField(position,label, property.rectValue);
            //        break;
            //    case SerializedPropertyType.ArraySize:
            //        //return thisSP.intValue;
            //        break;
            //    case SerializedPropertyType.Character:
            //        //property.intValue = EditorGUI.st;
            //        break;
            //    case SerializedPropertyType.AnimationCurve:
            //        property.animationCurveValue = EditorGUI.CurveField(position, label, property.animationCurveValue);
            //        break;
            //    case SerializedPropertyType.Bounds:
            //        property.boundsValue = EditorGUI.BoundsField(position, label, property.boundsValue);
            //        break;
            //    case SerializedPropertyType.Gradient:
            //        //property.intValue return SafeGradientValue(thisSP);
            //        break;
            //    case SerializedPropertyType.Quaternion:
            //        Quaternion q = property.quaternionValue;
            //        Vector4 v = new Vector4(q.x, q.y, q.z, q.w);
            //        v = EditorGUI.Vector4Field(position, label.text, v);
            //        property.quaternionValue = new Quaternion(v.x, v.y, v.z, v.w);
            //        break;
            //    default:
            //        throw new NotImplementedException("Unimplemented propertyType " + property.propertyType + ".");
            //}
        }

        public static float PropertyHeight(SerializedProperty property)
        {
            switch (property.propertyType)
            {
                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                    return EditorGUIUtility.singleLineHeight*(EditorGUIUtility.wideMode ? 1 : 2);
                case SerializedPropertyType.Vector4:
                case SerializedPropertyType.Quaternion:
                    return EditorGUIUtility.singleLineHeight*2;
                case SerializedPropertyType.Bounds:
                    return EditorGUIUtility.singleLineHeight*3;
                case SerializedPropertyType.Rect:
                    return EditorGUIUtility.singleLineHeight*(EditorGUIUtility.wideMode ? 2 : 3);
                default:
                    return CalculatePropertyHeight(EditorGUIUtility.singleLineHeight, property);
            }
        }

        private static string[] GetLayers()
        {
            List<string> l = new List<string>();
            for (int i = 0; i < 32; i++)
            {
                string name = LayerMask.LayerToName(i);
                if (!string.IsNullOrEmpty(name))
                {
                    l.Add(name);
                }
            }
            return l.ToArray();
        }
    }

}

