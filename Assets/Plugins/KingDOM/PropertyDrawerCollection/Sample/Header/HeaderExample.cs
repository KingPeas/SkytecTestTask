using UnityEngine;
using System.Collections;

public class HeaderExample : MonoBehaviour
{
		[Header("Header first", size = 28, color = "green")]
    [Splitter]
    [Space(10)]
        [Header("subtitle first",italic = true, size = 13)]
        [PropertyBase()]
        [PropertyArgs(tip = "Tip for the title.")]
		public string hoge;

		public string hogehoge;
		public AnimationCurve curve;

		[Header("Header second")]
        [PropertyArgs(tip = "Other tip.")]
		public string
				hoge2;
	
		public string hogehoge2;
		public AnimationCurve curve2;

    void Start()
    {
        //hoge = hoge;
    }
}
