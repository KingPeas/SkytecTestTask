using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorLayerExample : MonoBehaviour
{
    [AnimatorLayer]
    [PropertyArgs(tip = "Layer name from Animator Controller.")]
    public string AnimatorLayer;

    [AnimatorLayer]
    [PropertyArgs(tip = "Layer hash from Animator Controller.")]
    public int AnimatorLayerHash;

    [PropertyBase]
    [PropertyArgs(tip = "Link to an object AnimatorController")]
    public Transform OtherControllerLink;

    [AnimatorLayer("OtherControllerLink")]
    [PropertyArgs(tip = "Layer name from Other Animator Controller.")]
    public string AnimatorLayer2;

    private Animator animator;

    void Start()
    {
    }

    void Update()
    {
    }
}
