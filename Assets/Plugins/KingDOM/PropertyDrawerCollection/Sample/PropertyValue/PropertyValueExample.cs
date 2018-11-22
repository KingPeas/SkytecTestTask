using UnityEngine;
using System.Collections;

public class PropertyValueExample : MonoBehaviour
{

    [TypePopup]
    [PropertyArgs(tip = "It contains the selected name of the Type")]
    public string type = "";

    [PropertyPopup(PropertyPopupAttribute.SourceType.FieldTypeName, "type")]
    [PropertyArgs(tip = "It contains the name of the property of the selected Type.")]
    public string property = "";

    [PropertyValue("type", "property")]
    [PropertyArgs(tip = "Value for comparison. By changing the properties you will change the field of value input")]
    public string propValue = "";

    [PropertyValue("type", "property")]
    [PropertyArgs(tip = "The list of values for comparison.")]
    public string[] properties = new string[3];

    [TypePopup]
    [PropertyArgs(tip = "The choice of another type")]
    public string otherType = "";

    [PropertyPopup(PropertyPopupAttribute.SourceType.FieldTypeName, "otherType")]
    [PropertyArgs(tip = "Selecting another property for OtherType")]
    public string otherProperty = "";

    [PropertyValue("otherType", "otherProperty")]
    [PropertyArgs(tip = "Value for comparison with OtherProperty.")]
    public string propOtherValue = "";

    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
