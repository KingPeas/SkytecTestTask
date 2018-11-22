using UnityEngine;
using System.Collections;

public class PasswordExample : MonoBehaviour
{
	[Password]
    [PropertyArgs(tip: "Displays the default password.")]
	public string pass;
	[Password( 3, 6 )]
    [PropertyArgs(tip: "Displays the password with a restriction on the length of the minimum and maximum number of characters.")]
	public string pass2;
	[Password( 3, false )]
    [PropertyArgs(tip: "Displays the password that is not less than a specified number of characters without a mask.")]
	public string pass3;
    [PropertyArgs(tip: "Displays the password with a restriction on the length without a mask.")]
	[Password (5, 6, false )]
	public string pass4;
}
