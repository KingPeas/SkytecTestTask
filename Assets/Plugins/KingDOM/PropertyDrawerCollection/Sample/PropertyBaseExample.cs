using UnityEngine;
using System.Collections;

public class PropertyBaseExample : MonoBehaviour {
    
    [Header("PropertyBase: Examples")]
    [PropertyBase]  //Point out that the comparison must be made through the base element
    [PropertyArgs(label: "Vector", tip: "Displays Vector instead of the variable name TVector3", lines = 3)]
    // Specifies the label instead of the standard and adds a tip. Also specify the number of lines to display
    public Vector3 TVector3 = new Vector3();

    //You can display a label not only in English
    [PropertyBase]  //Point out that the comparison must be made through the base element
    [PropertyArgs(label: "Speed", tip: "You can display a label not only in English", callbacks = new[] { "Changed" })]
    // Specifies the label instead of the standard and adds a tip.
    public float StartValue = 0.0f;

    //You can determine the values by different attributes
    [PropertyArgs(label: "Speed")]
    [PropertyBase]
    [PropertyArgs(tip = "Please select the speed")]
    [PropertyArgs(callbacks = new []{"Changed"} )]
    public float velocityValue = 0f;

    //All key values can also be applied to other collections of PropertyDrawer
    [SceneName(true)] // for the heirs of the basic properties of the PropertyDrawer also can specify additional parameters
    [PropertyArgs(label = "Scene", tip = "Select the scene to go", callbacks = new[] { "Changed" })] 
    public string selectScene;

    [Range(0, 100)]
    [PropertyArgs(label = "Lives", tip = "Determine the starting value of the number of lives", callbacks = new[] { "Changed" })]
    public int health = 100;

    [Multiline(8)]
    [PropertyArgs(label = "Info", tip = "information field")]
    public string playerBiography = "Please enter your biography";
    
    [Multiline(4)]
    [PropertyArgs(tip = "Information field without a label", noLabel = true)]
    public string DisplayInfo = "Info will display here after update properties.";

    [PropertyBase] [PropertyArgs(hideIfEmpty = "health")] public bool Iamlive = true;
    
    void Changed()
    {
        DisplayInfo = string.Format("{0} - {1}\n{2} - {3}\n{4} - {5}\n{6} - {7}",
            "Start speed", StartValue,
            "Velocity", velocityValue,
            "Scene", selectScene,
            "Lives", health);
    }
}
