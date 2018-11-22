using System;
using UnityEditor;
using System.Linq;


[CustomPropertyDrawer(typeof(AssetBundleNameAttribute))]
public class AssetBundleNameDrawer : PopupTextEdit
{
    private string[] bundleNames = null;

    public override string[] GetOptions()
    {
        if (bundleNames == null)
        {
            bundleNames = AssetDatabase.GetAllAssetBundleNames();
            for (int i = 0; i < bundleNames.Length; i++)
            {
                int idx = bundleNames[i].LastIndexOf(".");
                bundleNames[i] = idx > 0 ? bundleNames[i].Substring(0, idx) : bundleNames[i];
            }
        }
        return bundleNames;
    }

    protected override string GetOnlyName(string fullName)
    {
        return fullName;
    }

    private AssetBundleNameAttribute assetBundleNameAttributeAttribute
    {
        get
        {
            return (AssetBundleNameAttribute)attribute;
        }
    }

}