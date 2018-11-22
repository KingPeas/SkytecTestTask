using System;
using UnityEditor;
using System.Linq;


[CustomPropertyDrawer(typeof(TypePopupAttribute))]
public class TypePopupDrawer : PopupTextEdit
{
    private string[] nameTypes = null;
    //private static int[] typeNumbers = null;

    public override string[] GetOptions()
    {
        if (nameTypes == null)
        {
            Type[] types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(asm => asm.GetTypes()).Where(type => typePopupAttribute.parentType.IsAssignableFrom(type)).Distinct().ToArray();
            nameTypes = new string[types.Length];
            for (int i = 0; i < types.Length; i++)
            {
                nameTypes[i] = types[i].FullName;
            }
            Array.Sort(nameTypes);
            //typeNumbers = new int[nameTypes.Length];
        }
        return nameTypes;
    }

    private TypePopupAttribute typePopupAttribute
    {
        get
        {
            return (TypePopupAttribute)attribute;
        }
    }

}