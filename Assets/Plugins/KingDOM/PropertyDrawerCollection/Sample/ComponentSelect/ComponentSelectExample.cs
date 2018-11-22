using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ComponentSelectExample : MonoBehaviour
{
    [ComponentSelect]
    public Component colliderTest = null;

    [ComponentSelect(typeof(Camera))]
    public Component monoTest = null;

    public bool test = true;

	
    // Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
        if ((colliderTest as MonoBehaviour) != null)
            (colliderTest as MonoBehaviour).enabled = test;
        if ((monoTest as MonoBehaviour) != null)
        (monoTest as MonoBehaviour).enabled = test;
	}
}
