using System;
using UnityEngine;
using System.Collections;

public class PropertyPopupExample : MonoBehaviour
{
    [PropertyPopup(PropertyPopupAttribute.SourceType.TypeName, "UnityEngine.Rigidbody")]
    [PropertyArgs(tip = "Selection of properties RigidBody.")]
    public string PropertyByTypeName = "";
    [TypePopup]
    [PropertyArgs("Type", "Select the TypeReaction object for the next field.")] 
    public string TypeName = "";
     [PropertyPopup(PropertyPopupAttribute.SourceType.FieldTypeName, "TypeName")]
     [PropertyArgs(tip = "Field to select the properties on behalf of the type specified in the TypeName(Type).")] 
    public string PropertyByFieldValue = "";
     [PropertyPopup(PropertyPopupAttribute.SourceType.FieldTarget)]
     [PropertyArgs(tip = "Link to the properties of this component.")] 
    public string PropertySelf = "";

    [PropertyBase]
    [PropertyArgs(tip = "Other object for a selection of properties.")]
    public Component TypeTarget = null;
    [PropertyPopup(PropertyPopupAttribute.SourceType.FieldTarget, "TypeTarget")]
    [PropertyArgs(tip = "Field for a selection of all available properties of another object on the link in TypeTarget.")] 
    public string prop_other = "";

    [PropertyPopup(PropertyPopupAttribute.SourceType.CalculatedType, "CalculatedType")] public string prop_calculated;

    public Type CalculatedType
    {
        get { return typeof(Rigidbody); }
    }

    // Use this for initialization
	void Start ()
	{
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
