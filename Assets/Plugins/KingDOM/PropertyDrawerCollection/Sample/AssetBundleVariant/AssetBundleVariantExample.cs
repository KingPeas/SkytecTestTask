using UnityEngine;
using System.Collections;

public class AssetBundleVariantExample : MonoBehaviour
{

    [AssetBundleVariant]
    [PropertyArgs(tip = "Here you can choose the name of the AssetBundle Variant.")]
    public string bundleVariant = "";
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
