using System;
using System.IO;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEngine;
using System.Collections;
using Object = UnityEngine.Object;

namespace KingDOM
{
    /// <summary>
    /// Utilities to work with the plugin
    /// </summary>
    public class KingUtil //: MonoBehaviour
    {

        /// <summary>
        /// Displays an error message when checking
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="target">Source of error</param>
        /// <returns>Always returns false</returns>
        public static bool CheckFailed(string message, Object target = null)
        {
            Debug.LogError(message, target);
            return false;
        }
        /// <summary>
        /// Object checking that the value is not empty.
        /// </summary>
        /// <param name="checkedObject">Inspected object</param>
        /// <param name="errorMessage">The text of the error if the check failed</param>
        /// <param name="checkBefore">Result before checking</param>
        /// <returns>Returns the final result of the test which takes into account the importance of checking that the object is not null</returns>
        public static bool CheckNotNull(System.Object checkedObject, string errorMessage, bool checkBefore = false, Object target = null)
        {
            return (checkedObject != null || CheckFailed(errorMessage, target)) && checkBefore;
        }
        /// <summary>
        /// Проверяет что слой входит в маску слоев
        /// </summary>
        /// <param name="layerValue">значение слоя</param>
        /// <param name="mask">Маска слоев</param>
        /// <returns></returns>
        public static bool LayerInMask(int layerValue, LayerMask mask)
        {
            bool ret = ((1 << layerValue) & mask) != 0;

            return ret;
        }
        /// <summary>
        /// Проверяет что слой по имени входит в допустимую группу
        /// </summary>
        /// <param name="layerName">Имя слоя</param>
        /// <param name="mask">маска для фильтрации</param>
        /// <returns></returns>
        public static bool LayerInMask(string layerName, LayerMask mask)
        {
            int val = LayerMask.NameToLayer(layerName);
            return LayerInMask(val, mask);
        }
        /// <summary>
        /// Reference to the file in the application directory
        /// </summary>
        /// <param name="folder">Directory</param>
        /// <param name="file">File</param>
        /// <returns>Path to file</returns>
        public static string AppPath(string folder, string file)
        {
            string path = Application.dataPath;

            if (!Application.isEditor)
            {
                path = Directory.GetParent(path).FullName;
            }
            //DirectoryInfo dir = new DirectoryInfo(path);
            path = Path.GetFullPath(path);

            if (folder != "")
                path = Path.Combine(path, folder);

            if (file != "")
                path = Path.Combine(path, file);
            else
            {
                path += Path.DirectorySeparatorChar;
            }
            return path;
        }
        /// <summary>
        /// Reference to the folder in the application directory
        /// </summary>
        /// <param name="folder">Directory</param>
        /// <returns>Path to directory</returns>
        public static string AppPath(string folder)
        {
            return AppPath(folder, "");
        }
        /// <summary>
        /// Relative path to file
        /// </summary>
        /// <returns>Relative path</returns>
        /// <param name="path">Absolute path</param>
        public static string RelativePath(string path)
        {
            return RelativePath("", path);
        }
        /// <summary>
        /// Relative path to file
        /// </summary>
        /// <returns>Relative path</returns>
        /// <param name="fromPath">Root directory</param>
        /// <param name="toPath">Converted to a relative path</param>
        public static string RelativePath(string fromPath, string toPath)
        {
            if (String.IsNullOrEmpty(toPath)) return "";
            if (String.IsNullOrEmpty(fromPath)) fromPath = AppPath("", "");

            toPath = toPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);// + Path.DirectorySeparatorChar;
            fromPath = fromPath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);// + Path.DirectorySeparatorChar;
            if (fromPath[fromPath.Length - 1] != Path.DirectorySeparatorChar) fromPath += Path.DirectorySeparatorChar;
            Uri fromUri = new Uri(fromPath);
            Uri toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme) { return toPath; } // path can't be made relative.
            if (toUri.Scheme.ToUpperInvariant() != "FILE" || toUri.HostNameType != UriHostNameType.Basic)
            {
                return toPath;
            }
            string relativePath = "";
            try
            {
                relativePath = parseRelativePath(fromUri.LocalPath, toUri.LocalPath); //fromUri.MakeRelativeUri(toUri);
            }
            catch
            {
                relativePath = toUri.LocalPath + Path.AltDirectorySeparatorChar + "ErrorTesting";
            }
            relativePath = Uri.UnescapeDataString(relativePath);

            relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            return relativePath;
        }

        private static string parseRelativePath(string from, string to)
        {
            string res = "";
            string pattern = @"[\\\/]+";
            Regex regexFrom = new Regex(pattern);
            Regex regexTo = new Regex(pattern);
            from = regexFrom.Replace(from, Path.DirectorySeparatorChar.ToString());
            to = regexTo.Replace(to, Path.DirectorySeparatorChar.ToString());
            string[] pathesF = regexFrom.Split(from);
            string[] pathesT = regexTo.Split(to);
            IEnumerator eFrom = pathesF.GetEnumerator();
            IEnumerator eTo = pathesT.GetEnumerator();
            bool existRepeat = false;
            string valFrom = "";
            string valTo = "";
            bool fromExist = eFrom.MoveNext();
            bool toExist = eTo.MoveNext();
            while (fromExist || toExist)
            {
                valFrom = fromExist ? eFrom.Current.ToString() : null;
                valTo = toExist ? eTo.Current.ToString() : null;
                if (!string.IsNullOrEmpty(valFrom) && !string.IsNullOrEmpty(valTo) && valFrom == valTo)
                {
                    if (!existRepeat)
                    {
                        existRepeat = true;
                    }
                }
                else
                {
                    if (!existRepeat)
                        return to;

                    if (!string.IsNullOrEmpty(valTo))
                    {
                        res = res + (string.IsNullOrEmpty(res) ? "" : Path.DirectorySeparatorChar.ToString()) + valTo;
                    }

                    if (!string.IsNullOrEmpty(valFrom))
                    {
                        res = ".." + (string.IsNullOrEmpty(res) ? "" : Path.DirectorySeparatorChar.ToString()) + res;
                    }
                }
                fromExist = fromExist && eFrom.MoveNext();
                toExist = toExist && eTo.MoveNext();
            }
            return res;
        }
        /// <summary>
        /// Get Absolute path
        /// </summary>
        /// <returns>Absolute path</returns>
        /// <param name="relativePath">Relative path</param>
        public static string AbsolutePath(string relativePath)
        {
            return AbsolutePath("", relativePath);
        }
        /// <summary>
        /// Get Absolute path
        /// </summary>
        /// <returns>Absolute path</returns>
        /// <param name="appPath">Application path</param>
        /// <param name="relativePath">Relative path</param>
        public static string AbsolutePath(string appPath, string relativePath)
        {

            if (Path.IsPathRooted(relativePath))
            {
                return Path.GetFullPath(relativePath);
            }
            else
            {
                if (string.IsNullOrEmpty(appPath)) appPath = KingUtil.AppPath("");
                return Path.GetFullPath(Path.Combine(appPath, relativePath));
            }
        }
        /// <summary>
        ///  Number rounding to a number with a given accuracy
        /// </summary>
        /// <param name="value">The number to be rounded</param>
        /// <param name="precision">Accuracy</param>
        /// <returns>Result after rounding</returns>
        public static float Round(float value, float precision)
        {
            return (float)Round((double)value, (double)precision);
        }
        /// <summary>
        /// Number rounding to a number with a given accuracy
        /// </summary>
        /// <param name="value">The number to be rounded</param>
        /// <param name="precision"> Accuracy</param>
        /// <returns>Result after rounding</returns>
        public static double Round(double value, double precision)
        {
            if (precision < double.Epsilon)
                return value;

            double modul = value % precision;
            double result = value - modul;
            if (result < 0)
            {
                if (-modul >= precision / 2)
                    result -= precision;
            }
            else
            {
                if (modul >= precision / 2)
                    result += precision;
            }

            return result;

        }
        /// <summary>
        /// Get type by name
        /// </summary>
        /// <param name="TypeName"> Type name</param>
        /// <returns>Loaded assembly type</returns>
        public static Type GetType(string TypeName)
        {

            // Try Type.GetType() first. This will work with types defined
            // by the Mono runtime, in the same assembly as the caller, etc.
            var type = Type.GetType(TypeName);

            // If it worked, then we're done here
            if (type != null)
                return type;

            // If the TypeName is a full name, then we can try loading the defining assembly directly
            if (TypeName.Contains("."))
            {

                // Get the name of the assembly (Assumption is that we are using 
                // fully-qualified type names)
                var assemblyName = TypeName.Substring(0, TypeName.IndexOf('.'));

                // Attempt to load the indicated Assembly
                var assembly = Assembly.Load(assemblyName);
                if (assembly == null)
                    return null;

                // Ask that assembly to return the proper Type
                type = assembly.GetType(TypeName);
                if (type != null)
                    return type;

            }

            // If we still haven't found the proper type, we can enumerate all of the 
            // loaded assemblies and see if any of them define the type
            var currentAssembly = Assembly.GetExecutingAssembly();
            var referencedAssemblies = currentAssembly.GetReferencedAssemblies();
            foreach (var assemblyName in referencedAssemblies)
            {

                // Load the referenced assembly
                var assembly = Assembly.Load(assemblyName);
                if (assembly != null)
                {
                    // See if that assembly defines the named type
                    type = assembly.GetType(TypeName);
                    if (type != null)
                        return type;
                }
            }

            // The type just couldn't be found...
            return null;

        }
        /// <summary>
        /// Get only name
        /// </summary>
        /// <param name="fullName">Name with description</param>
        /// <returns>Name without description</returns>
        public static string GetOnlyName(string fullName)
        {
            Regex reg = new Regex(@"^[\w_.]*");
            return reg.Match(fullName).Value;
        }

        public static T GetPropertyValue<T>(object obj, string propName)
        {
            Type type = obj.GetType();
            T result = default(T);
            FieldInfo field = type.GetField(propName);
            if (field != null && field.FieldType == typeof(T))
            {
                return (T)field.GetValue(obj);
            }
            else
            {
                PropertyInfo pInfo = type.GetProperty(propName);
                if (pInfo != null && pInfo.PropertyType == typeof(T))
                {
                    return (T)pInfo.GetValue(obj, null);
                }
            }

            return result;
        }

        public static object GetPropertyValue(object obj, string propName)
        {
            Type type = obj.GetType();
            object result = obj;
            FieldInfo field = type.GetField(propName);
            if (field != null)
            {
                return field.GetValue(obj);
            }
            else
            {
                PropertyInfo pInfo = type.GetProperty(propName);
                if (pInfo != null)
                {
                    return pInfo.GetValue(obj, null);
                }
            }

            return result;
        }

        public static void SetPropertyValue(object obj, string propName, object val)
        {
            Type type = obj.GetType();
            FieldInfo field = type.GetField(propName);
            if (field != null)
            {
                var v = Convert.ChangeType(val, field.FieldType);
                field.SetValue(obj, v);
            }
            else
            {
                PropertyInfo pInfo = type.GetProperty(propName);
                if (pInfo != null)
                {
                    var v = Convert.ChangeType(val, pInfo.PropertyType);
                    pInfo.SetValue(obj, v, null);
                }
            }
        }
    }

}
