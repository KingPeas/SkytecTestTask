using UnityEngine;
using System.Collections;

public class SplitterExample : MonoBehaviour {

    [Header("Header")]
    [Splitter]
    [PropertyBase()]
    [PropertyArgs(tip = "Tip for the title.")]
    public string First;
    public string hogehoge;
    [Splitter(5)]
    public AnimationCurve curve;

    [PropertyArgs(tip = "Other tip.")]
    public string Second;

    public string hogehoge2;

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
