using System;
using UnityEngine;

/// <summary>
/// Header
/// </summary>
public class HeaderAttribute : DecoratorBaseAttribute
{
	/// <summary>
    /// Text of header
	/// </summary>
    public string text;
    /// <summary>
    /// Font size
    /// </summary>
	public int size = 14;
    /// <summary>
    /// It use bold font
    /// </summary>
    public bool bold = true;
    /// <summary>
    /// It use italic font
    /// </summary>
    public bool italic = false;
    /// <summary>
    /// Text color
    /// </summary>
    public string color = "";

        /// <summary>
        /// Displays header
        /// </summary>
        /// <param name="header">Text of header</param>
		public HeaderAttribute (string text)
		{
			this.text = text;
		}

}