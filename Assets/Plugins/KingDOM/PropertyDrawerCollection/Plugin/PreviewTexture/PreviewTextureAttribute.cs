using UnityEngine;

/// <summary>
/// Preview for image.
/// </summary>
public class PreviewTextureAttribute : PropertyBaseAttribute
{
    public Rect lastPosition = new Rect (0, 0, 0, 0);
    public long expire = 6000000000; // 10min
    public WWW www;
    public Texture2D cached;
    /// <summary>
    /// Preview for image.
    /// </summary>
    public PreviewTextureAttribute ()
    {

    }
    /// <summary>
    /// Preview for image.
    /// </summary>
    /// <param name="expire">Timeout in seconds</param>
    public PreviewTextureAttribute (int expire)
    {
        this.expire = expire * 1000 * 10000;
    }
}