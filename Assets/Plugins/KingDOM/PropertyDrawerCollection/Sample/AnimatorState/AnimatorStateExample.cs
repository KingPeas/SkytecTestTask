using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorStateExample : MonoBehaviour
{
    [AnimatorState]
    [PropertyArgs(tip = "State name from Animator Controller.")]
    public string AnimatorState;

    [AnimatorState]
    [PropertyArgs(tip = "State hash from Animator Controller.")]
    public int AnimatorStateInt;

    [PropertyBase]
    [PropertyArgs(tip = "Link to an object AnimatorController")]
    public Transform OtherControllerLink;

    [AnimatorState("OtherControllerLink")]
    [PropertyArgs(tip = "State name from Other Animator Controller.")]
    public string AnimatorState2;

    private Animator animator;

    void Start()
    {
    }

    void Update()
    {
    }
}
