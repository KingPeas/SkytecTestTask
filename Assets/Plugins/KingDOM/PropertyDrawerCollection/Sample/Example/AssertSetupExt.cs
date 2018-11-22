using UnityEngine;
using System.Collections;

public class AssertSetupExt : MonoBehaviour {

    [TypePopup]
    [PropertyArgs(tip = "Seeking script in the tree of visualization")]
    public string script = "";

    public CompareValueExt compareValue = new CompareValueExt();
    public CompareValueExt[] compareValues = new CompareValueExt[0];
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
