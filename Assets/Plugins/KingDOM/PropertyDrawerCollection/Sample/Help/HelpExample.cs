using UnityEngine;
using System.Collections;

public class HelpExample : MonoBehaviour {

    [HelpAttribute("The opening door animation.")]
    [PropertyBase]
    [PropertyArgs(tip = "This property contains the help message.")]
    public Animator OpenDor;
}
