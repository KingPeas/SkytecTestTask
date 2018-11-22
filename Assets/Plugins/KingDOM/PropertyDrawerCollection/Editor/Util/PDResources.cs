using UnityEngine;
using System.Collections.Generic;
using System.IO;
using UnityEditor;

namespace KingDOM.Resouces
{
    public class PDResources : ScriptableObject
    {
        public const string DEFAULT_EXT = "png";

        private static bool isInit = false; //;
        private static Dictionary<string, Texture2D> resources;
        private static string currentFolder;

        private static void Init()
        {
            resources = new Dictionary<string, Texture2D>();
            MonoScript ms = MonoScript.FromScriptableObject(PDResources.CreateInstance("PDResources"));
            currentFolder = AssetDatabase.GetAssetPath(ms);
            currentFolder = Path.GetDirectoryName(currentFolder);

            isInit = true;
        }

        private static Texture2D loadImage(string name, string extension)
        {
            string path = Path.Combine(currentFolder, string.Format("{0}.{1}", name, extension));
            return AssetDatabase.LoadAssetAtPath(path, typeof(Texture2D)) as Texture2D;
        }

        public static void AddImage(string name)
        {
            AddImage(name, DEFAULT_EXT);
        }

        public static void AddImage(string name, string extension)
        {
            if (!isInit) Init();

            if (string.IsNullOrEmpty(name)) return;

            resources[name] = loadImage(name, extension);
        }

        public static Texture2D GetImage(string name)
        {
            if (!isInit) Init();
            Texture2D image = null;
            if (resources.ContainsKey(name))
                image = resources[name];
            return image;
        }

    }

}

