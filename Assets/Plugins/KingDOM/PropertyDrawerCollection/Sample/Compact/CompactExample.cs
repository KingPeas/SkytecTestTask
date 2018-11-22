using UnityEngine;
using System.Collections;

public class CompactExample : MonoBehaviour
{
	[Compact]
    [PropertyArgs(tip = "Vector2 in a compact mode")]
	public Vector2 vector2;
	[Compact]
    [PropertyArgs(tip = "Vector3 in a compact mode")]
    public Vector3 vector3;
	[Compact]
    [PropertyArgs(tip = "Vector4 in a compact mode")]
    public Vector4 vector4;
	[Compact]
    [PropertyArgs(tip = "Rect in a compact mode")]
    public Rect rect;
	[Compact]
    [PropertyArgs(tip = "Quaternion in a compact mode")]
    public Quaternion quaternion;
	[Compact]
    [PropertyArgs(tip = "Bounds in a compact mode")]
    public Bounds bounds;
    [Compact]
    [PropertyArgs(tip = "List of Bounds in a compact mode")]
    public Bounds[] boundsList;
}
