using UnityEngine;
using System.Collections;

public class TypePopupExample : MonoBehaviour
{

    [TypePopup]
    [PropertyArgs(tip = "Here you can choose the name of the heir Type MonoBehaviour.")]
    public string typeName = "";
    [TypePopup(typeof(Collider))]
    [PropertyArgs(tip = "Here you can choose the name of the heir Type Collider.")]
    public string typeCollider = "";
    [TypePopup(typeof(Collider))]
    [PropertyArgs(tip = "List to select the names of heirs Type MonoBehaviour.")]
    public string[] typeColliders = new string[0];
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
