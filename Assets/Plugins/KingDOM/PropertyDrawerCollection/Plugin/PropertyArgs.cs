using System;
/// <summary>
/// Arguments for Property
/// </summary>
[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
public class PropertyArgsAttribute: Attribute
{
    /// <summary>
    /// Text tips
    /// </summary>
    public string tip = "";
    /// <summary>
    /// Text label
    /// </summary>
    public string label = "";
    /// <summary>
    /// The number of rows to display properties
    /// </summary>
    public int lines = 0;
    /// <summary>
    /// Do not display the label
    /// </summary>
    public bool noLabel = false;
    /// <summary>
    /// Execution methods list.
    /// </summary>
    public string[] callbacks = null;

    public string hideIfEmpty = "";
    /*/// <summary>
    /// Arguments for Property
    /// </summary>
    public PropertyArgsAttribute()
    {
    }*/
    /// <summary>
    /// Arguments for Property
    /// </summary>
    /// <param name="label">Text label</param>
    /// <param name="tip">Text tips</param>
    public PropertyArgsAttribute(string label = "", string tip = "")
    {
        this.tip = tip;
        this.label = label;
    }
    /// <summary>
    /// Arguments for Property
    /// </summary>
    /// <param name="args">Other Arguments for Property</param>
    public PropertyArgsAttribute(PropertyArgsAttribute args)
    {
        this.tip = args.tip;
        this.label = args.label;
        this.lines = args.lines;
        this.noLabel = args.noLabel;
        this.callbacks = args.callbacks;
        this.hideIfEmpty = args.hideIfEmpty;
    }

}
