using UnityEngine;


public class SelectableLabelExample : MonoBehaviour
{
    [SelectableLabel("Here you can display any text\n" +
                     "\thttps://www.assetstore.unity3d.com/en/\n" +
                     "\t\tFor example, this")]
    [Space(15)]
    public string hoge;
}
