using UnityEngine;
using UnityEngine.UI;

public class RewardsMenu : MenuItem
{
#pragma warning disable 0649
    [SerializeField] protected Image[] Icons;
#pragma warning disable 0649

    private RewardIconMap _rewardIconMap;

    public void Awake()
    {
        _rewardIconMap = FindObjectOfType<RewardIconMap>();
    }

    protected override void ShowScreenAction()
    {
        var rewardsAgreggated = PlayerPrefs.GetString("Rewards", "");

        var rewardIds = rewardsAgreggated.Split(';');

        FillIcons(rewardIds);

        Screen.SetActive(true);
    }

    private void FillIcons(string[] rewards)
    {
        var loopCount = rewards.Length > Icons.Length ? Icons.Length : rewards.Length;

        foreach (var item in Icons)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < loopCount; i++)
        {
            var sprite = _rewardIconMap.FindSprite(rewards[i]);

            if (sprite != null)
            {
                Icons[i].gameObject.SetActive(true);
                Icons[i].sprite = sprite;
            }
        }
    }
}
