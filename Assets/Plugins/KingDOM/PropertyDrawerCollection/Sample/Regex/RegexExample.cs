using UnityEngine;

public class RegexExample : MonoBehaviour
{
    [Regex(@"^(?:\d{1,3}\.){3}\d{1,3}$", "Invalid IP address!\nExample: '127.0.0.1'")]
    [PropertyArgs(tip = "In this field you can only enter the IP address")]
    public string serverAddress = "192.168.0.1";
}