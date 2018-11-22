using UnityEngine;

public class ObserveExample : MonoBehaviour
{
    //[Observe("Callback")]
    [PropertyArgs(tip = "When you change the line will be called one CallBack.")]
    public string
        hoge;

    //[Observe("Callback", "Callback2")]
    [PropertyArgs(tip = "If you change the Enum is caused by two CallBack.")]
    public Test
        test;

    public enum Test
    {
        Hoge,
        Fuga
    }

    public void Callback ()
    {
        Debug.Log ("Call1 will be invoked.");
    }

    private void Callback2 ()
    {
        Debug.Log("Call2 will be invoked.");
    }
}
