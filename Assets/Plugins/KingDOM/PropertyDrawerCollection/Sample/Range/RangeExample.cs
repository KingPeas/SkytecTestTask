using UnityEngine;
using System.Collections;

public class RangeExample : MonoBehaviour {

    [Range(0, 100)]
    [PropertyArgs(tip = "Range from min to max")]
    public int health = 100;


    [Range(30, 0)]
    [PropertyArgs(tip = "Range from max to min")]
    public float experiance = 100;

    [Range(4, 7.5f, 0.3f)]
    [PropertyArgs(tip = "Range from min to max fixed step")]
    public float enemies = 100;

    [Range(30, 0, 3)]
    [PropertyArgs(tip = "Range from max to min fixed step")]
    public float players = 100;
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
