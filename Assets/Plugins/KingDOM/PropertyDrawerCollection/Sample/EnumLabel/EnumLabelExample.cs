using UnityEngine;

[ExecuteInEditMode]  //to reflect the update properties in the editor is "setHigh" and "Test"
public class EnumLabelExample : MonoBehaviour
{

    public enum FilmDefinition
    {
        [EnumLabel("Perfect")]
        [PropertyArgs(label = "Best", tip = "Full HD quality")]
        High,
        [EnumLabel]
        [PropertyArgs(label = "Better", tip = "Full quality")]
        Good,
        [EnumLabel]
        [PropertyArgs(label = "Not bad", tip = "Can be better")]
        Low
    }
    
	[EnumLabel]
    [PropertyArgs(label = "Resolution list", tip = "Select a screen resolution.")]
    
    public FilmDefinition[] tests = new FilmDefinition[3];

    [EnumLabel]
    [PropertyArgs(label = "Reflect resolution", tip = "Select the resolution to reflect.")]
    public FilmDefinition testReflection = FilmDefinition.High;

    [EnumLabel]
    [PropertyArgs(tip = "To change this property, you must change the value of property of SetHigh.")]
    public FilmDefinition Test = FilmDefinition.Low;

    [PropertyBase]
    [PropertyArgs(tip = "Modifying this property, you’re changing the properties of Test.", callbacks = new[] { "Updated" })]
    [Help("You change SetHigh, script will update Test value.")]
    public bool setHigh = false;

    void Updated()
    {
        Test = setHigh ? FilmDefinition.High : FilmDefinition.Low;
    }

}
