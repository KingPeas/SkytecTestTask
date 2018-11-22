using UnityEngine;
using System.Collections;

public class MultilineExample : MonoBehaviour
{
    [Multiline(5)]
    [PropertyArgs("Info", "Information window.")]
    public string info = "";
    [Multiline]
    [PropertyArgs("Info2", "Other information window.")]
    public string info2 = "";
    [Multiline]
    [PropertyArgs("InfoList", "List of information messages")]
    public string[] infoList = {"",""};
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
