using System.Linq;
using UnityEngine;

public class RewardIconMap : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] protected Reward[] Rewards;
#pragma warning disable 0649

    public Sprite FindSprite(string spriteGameId)
    {
        if (spriteGameId == "")
        {
            return null;
        }

        var spriteFound = Rewards.FirstOrDefault(x => x.rewardGameId == spriteGameId);
        
        if (spriteFound != null)
        {
            return spriteFound.Sprite;
        }

        Debug.LogError("Não foi encontrado sprite");
        return null;
    }
}
