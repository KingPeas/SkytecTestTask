/// <summary>
/// Animator Paramater attribute.
/// </summary>
public class AnimatorLayerAttribute : PropertyBaseAttribute
{

    /// <summary>
    /// Name of the field that stores a reference to an object with an animator. If not specified, the source acts as the current object.
    /// </summary>
    public string SourceName = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimatorLayerAttribute"/> class.
    /// </summary>
    public AnimatorLayerAttribute(string sourceAnimator)
    {
        SourceName = sourceAnimator;
    }
    public AnimatorLayerAttribute()
    {
        SourceName = "";
    }


}
