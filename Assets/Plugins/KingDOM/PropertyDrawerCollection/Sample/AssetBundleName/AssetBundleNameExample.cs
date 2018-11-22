using UnityEngine;
using System.Collections;

public class AssetBundleNameExample : MonoBehaviour
{

    [AssetBundleName]
    [PropertyArgs(tip = "Here you can choose the name of the AssetBundle.")]
    public string bundleName = "";
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
