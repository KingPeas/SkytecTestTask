using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorParameterExample : MonoBehaviour
{
    [AnimatorParameter]
    [PropertyArgs(tip = "Parameter name of anytype.")]
    public string anyParam;
    [AnimatorParameter]
    [PropertyArgs(tip = "Parameter hash of anytype.")]
    public int paramHash;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Float)]
    [PropertyArgs(tip = "Parameter name of type float.")]
    public string floatParam;

    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Int)]
    [PropertyArgs(tip = "Parameter name of type integer.")]
    public string intParam;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Bool)]
    [PropertyArgs(tip = "Parameter name of type boolean.")]
    public string boolParam;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Trigger)]
    [PropertyArgs(tip = "Parameter name of trigger.")]
    public string triggerParam;

    [PropertyBase]
    [PropertyArgs(tip = "Link to an object AnimatorController")] 
    public Transform OtherControllerLink;
    [AnimatorParameter(AnimatorParameterAttribute.ParameterType.Float, "OtherControllerLink")]
    [PropertyArgs(tip = "Parameter name of type float by a link to an object OtherControllerLink.")]
    public string floatParamOther;

    [AnimatorParameter]
    [PropertyArgs(tip = "Parameter list of the animator.")]
    public string[] listParam = new string[5];

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //float f = animator.GetFloat(floatParam);

        //int i = animator.GetInteger(intParam);

        //bool b = animator.GetBool(boolParam);

        animator.SetTrigger(triggerParam);
    }
}
