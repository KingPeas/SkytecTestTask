using System;
using System.Reflection.Emit;

[Serializable]
public class CompareValueExt{

	public enum CompareType
    {
        [EnumLabel("None")]
        None,
        [EnumLabel("<")]
        Less,
        [EnumLabel("<=")]
        LessOrEqual,
        [EnumLabel("==")]
        Equal,
        [EnumLabel(">=")]
        MoreOrEqual,
        [EnumLabel(">")]
        More,
        [EnumLabel("!=")]
        NotEqual,
        [EnumLabel("Exist")]
        NotEmpty
    }
    [PropertyPopup(PropertyPopupAttribute.SourceType.FieldTypeName, "script")]
    [PropertyArgs("Field", "Field checking of the object. If a value isn’t defined, the check won’t be done.")]
    public string property = "";
    [EnumLabel]
    [PropertyArgs("Compare", "The rule of comparing values.")]
    public CompareType compare = CompareType.Equal;
    [PropertyValue("script", "property")]
    [PropertyArgs("Value", "Value which is compared to the value stored in the field of the checking object. By Default it is empty.", noLabel = true)]
    public string propertyValue = null;
}
