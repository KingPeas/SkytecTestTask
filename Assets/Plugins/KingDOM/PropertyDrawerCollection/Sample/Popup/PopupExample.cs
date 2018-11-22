using UnityEngine;
using System.Collections;

public class PopupExample : MonoBehaviour
{
    [Popup("Hoge","Fuga","Foo","Bar")]
    [PropertyArgs(tip = "Dropped out list of lines.")]
    public string popupStr;
    [Popup(1,2,3,4,5,6)]
    [PropertyArgs(tip = "Dropped out list of integers.")]
    public int popupInt;
    [Popup(1.5f,2.3f,3.4f,4.5f,5.6f,6.7f)]
    [PropertyArgs(tip = "Dropped out list of floating point numbers.")]
    public float popupFloat;
}
