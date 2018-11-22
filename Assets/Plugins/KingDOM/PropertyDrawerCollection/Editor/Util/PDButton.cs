using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using KingDOM.Resouces;
using UnityEditor;

namespace KingDOM.Util
{
    public class PDButton
    {
        public const string ICON_LIST = "List";
        public const string ICON_ERASE = "Erase";
        public const string ICON_ERROR = "Error";
        public const string ICON_INFO = "Info";

        private static bool isInit = false; //;
        private static Dictionary<string, BtnInfo> btns;

        private static void Init()
        {
            btns = new Dictionary<string, BtnInfo>();
            isInit = true;
            AddButton(ICON_LIST, ICON_LIST, "List for select option", "L");
            AddButton(ICON_ERASE, ICON_ERASE, "Clear selection", "C");

        }


        private static BtnInfo GetBtnInfo(string name)
        {
            if (!isInit) Init();

            if (!btns.ContainsKey(name))
            {
                return new BtnInfo("", "", "-");
            }
            else
            {
                return btns[name];
            }
        }

        public static void AddButton(string name, string textureName, string tip, string letter)
        {
            if (!isInit) Init();

            BtnInfo btn = new BtnInfo(textureName, tip, letter);
            btns.Add(name, btn);
        }

        public static bool IconButton(Rect rect, string name)
        {
            BtnInfo info = GetBtnInfo(name);

            Texture2D icon = PDResources.GetImage(info.textureName);
            if (icon == null)
            {
                return GUI.Button(rect, new GUIContent(info.letter, info.tip), EditorStyles.miniButton);
            }
            else
            {
                GUIStyle style = new GUIStyle();
                style.normal.background = icon;
                style.hover.background = icon;
                style.active.background = icon;
                return GUI.Button(rect, new GUIContent("", info.tip), style);
            }
        }


    }

    internal class BtnInfo
    {
        public string textureName;
        public string tip;
        public string letter;
        public bool isInit;

        internal BtnInfo(string textureName, string tip, string letter)
        {
            PDResources.AddImage(textureName);
            this.textureName = textureName;
            this.tip = tip;
            this.letter = letter;
            isInit = false;
        }
    }

}

