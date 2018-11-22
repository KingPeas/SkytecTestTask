using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ConstSelectExample : MonoBehaviour
{
    public const string TEST_CONSTANT = "const1";
    public const string TEST_CONSTANT2 = "const2";
    public const string TEST_CONSTANT3 = "const3";
    public const string TEST_CONSTANT4 = "const4";
    public const string TEST_CONSTANT5 = "const5";

    public const int INTTEST1 = 15;
    public const int INTTEST_2 = 3;

    [ConstSelect(typeof(ConstSelectExample))]
    [PropertyArgs(tip = "A string constant. Displays the name of the constant.")]
    public string StrConst = "";

    [ConstSelect(typeof(ConstSelectExample))]
    [PropertyArgs(tip = "Another string constant. Displays the name of the constant.")]
    public string StrConst2 = "";

    [ConstSelect(typeof(ConstSelectExample))]
    [PropertyArgs(tip = "An array of string constants. Displays a list of the names of the constants.")]
    public string[] ListStrConst = new string[3];
    
    [ConstSelect(typeof(ConstSelectExample))]
    [PropertyArgs(tip = "Property stores an integer value, and displays it as a constant name.")]
    public int IntConst = 0;

    [Disable]
    [PropertyArgs(tip = "This field displays the actual value of the integer constants for.")]
    public int IntConstValue = 0;

    [ConstSelect(typeof(Mathf))]
    [PropertyArgs(tip = "Float value from the class Mathf. Displays the name of the constant.")]
    public float valueMathf = 0.0f;


    void Update()
    {
        IntConstValue = IntConst;
    }
}
