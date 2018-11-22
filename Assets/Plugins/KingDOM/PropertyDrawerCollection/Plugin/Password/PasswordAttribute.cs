using UnityEngine;

/// <summary>
/// Property for password
/// </summary>
public class PasswordAttribute : PropertyBaseAttribute
{

	/*/// <summary>
	/// Mask for password hiding 
	/// </summary>
    public char mask = '*';//*/
    /// <summary>
    /// Need for password hiding
    /// </summary>
	public bool useMask = true;
    /// <summary>
    /// Minimum length for check. If the password is less, you will receive a warning message.
    /// </summary>
	public int minLength = 0;
    /// <summary>
    /// Maximum length for check. If the password is longer, the entered value will be truncated.
    /// </summary>
	public int maxLength = int.MaxValue;
    /// <summary>
    /// Property for password
    /// </summary>
	public PasswordAttribute ()
	{
	}
    /// <summary>
    /// Property for password
    /// </summary>
    /// <param name="minLength">Minimum length for check</param>
    /// <param name="maxLength">Maximum length for check</param>
	public PasswordAttribute (int minLength, int maxLength)
	{
		this.minLength = minLength;
		this.maxLength = maxLength;
	}
    /// <summary>
    /// Property for password
    /// </summary>
    /// <param name="minLength">Maximum length for check</param>
    /// <param name="useMask">Need for password hiding</param>
	public PasswordAttribute (int minLength, bool useMask)
	{
		this.useMask = useMask;
		this.minLength = minLength;
	}
	/// <summary>
	/// Property for password
	/// </summary>
    /// <param name="minLength">Minimum length for check</param>
    /// <param name="maxLength">Maximum length for check</param>
    /// <param name="useMask">Need for password hiding</param>
	public PasswordAttribute (int minLength, int maxLength, bool useMask)
	{
		this.useMask = useMask;
		this.minLength = minLength;
		this.maxLength = maxLength;
	}
}