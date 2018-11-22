using UnityEngine;
using System.Collections;

public class PreviewTextureExample : MonoBehaviour
{
    [PreviewTexture(60)]
    [PropertyArgs(tip = "This picture is downloaded from the Internet.")]
    public string
        textureURL = "http://prayersandapples.com/wp-content/uploads/2013/03/Ratatouille-Fat.jpg";

    [PreviewTexture]
    [PropertyArgs(tip = "This picture is loaded from Asset.")]
    public Texture hoge;
}
