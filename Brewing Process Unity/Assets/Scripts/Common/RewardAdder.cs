using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RewardAdder : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject Canvas;
    [SerializeField] private Image RewardImage;
    [SerializeField] private TextMeshProUGUI RewardTitle;
    [SerializeField] private TextMeshProUGUI RewardDescription;
    [SerializeField] private RewardAddBehaviour RewardAddBehaviour;
#pragma warning restore 0649

    public void AddReward(Reward reward)
    {
        var rewardString = PlayerPrefs.GetString("Rewards", "");

        var rewards = rewardString.Split(';');

        if (!rewards.Any(x => x == reward.rewardGameId))
        {
            PlayerPrefs.SetString("Rewards", $"{rewardString}{reward.rewardGameId};");
            ShowReward(reward);
            RewardAddBehaviour.AddReward(reward);
        }
    }

    private void ShowReward(Reward reward)
    {
        Canvas.SetActive(true);
        RewardImage.sprite = reward.Sprite;
        RewardTitle.text = reward.rewardName;
        RewardDescription.text = reward.description;
    }
}
