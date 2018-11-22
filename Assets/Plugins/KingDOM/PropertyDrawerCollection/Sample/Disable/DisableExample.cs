using UnityEngine;

public class DisableExample : MonoBehaviour
{
    [Disable][PropertyArgs(tip="I am disable")]  public string hoge = "hoge";

    [Disable][PropertyArgs(tip="And I am disable")] public int fuga = 1;

    [Disable]
    [PropertyArgs(tip = "And me too")]
    public AudioType audioType = AudioType.ACC;
}
