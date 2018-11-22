using System;
using System.Collections.Generic;
using UnityEditor;
using System.Linq;


[CustomPropertyDrawer(typeof(AssetBundleVariantAttribute))]
public class AssetBundleVariantDrawer : PopupTextEdit
{
    private string[] bundleVariants = null;
    //private static int[] typeNumbers = null;

    public override string[] GetOptions()
    {
        if (bundleVariants == null)
        {
            List<string> variants = new List<string>(); 
            
            bundleVariants = AssetDatabase.GetAllAssetBundleNames();
            for (int i = 0; i < bundleVariants.Length; i++)
            {
                int idx = bundleVariants[i].LastIndexOf(".");
                if (idx > 0)
                {
                    variants.Add(bundleVariants[i].Substring(idx+1, bundleVariants[i].Length - idx-1));
                }
            }
            bundleVariants = variants.ToArray();
        }
        return bundleVariants;
    }

    protected override string GetOnlyName(string fullName)
    {
        return fullName;
    }

    private AssetBundleVariantAttribute assetBundleVariantAttribute
    {
        get
        {
            return (AssetBundleVariantAttribute)attribute;
        }
    }

}