/// <summary>
/// Animator Paramater attribute.
/// </summary>
public class AnimatorParameterAttribute : PropertyBaseAttribute
{
    /// <summary>
    /// Parameter type in animator
    /// </summary>
    public ParameterType parameterType = ParameterType.None;
    /// <summary>
    /// Name of the field that stores a reference to an object with an animator. If not specified, the source acts as the current object.
    /// </summary>
    public string SourceName = null;

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimatorParameterAttribute"/> class.
    /// </summary>
    public AnimatorParameterAttribute()
        : this(ParameterType.None)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="AnimatorParameterAttribute"/> class.
    /// </summary>
    /// <param name='parameterType'>
    ///  Parameter type in animator
    /// </param>
	/// <param name='SourceName'>
	/// Name of the field that stores a reference to an object with an animator. If not specified, the source acts as the current object.
	/// </param>
    public AnimatorParameterAttribute(ParameterType parameterType, string SourceName = "")
    {
        this.parameterType = parameterType;
        this.SourceName = SourceName;
    }

    /// <summary>
    /// Parameter types
    /// </summary>
    public enum ParameterType
    {
        Vector = 0,
        Float = 1,
        Int = 3,
        Bool = 4,
        Trigger = 9,
        None = 9999,
    }
}
