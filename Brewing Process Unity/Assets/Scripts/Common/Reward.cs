using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Reward", order = 1)]
public class Reward : ScriptableObject
{
    public string rewardName;
    public string rewardGameId;
    public Sprite Sprite;
    public string description;
}
