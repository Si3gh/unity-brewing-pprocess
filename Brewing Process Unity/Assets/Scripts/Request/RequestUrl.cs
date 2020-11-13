using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/URL", order = 3)]
public class RequestUrl : ScriptableObject
{
    public string url;
    public bool isValid;
}
