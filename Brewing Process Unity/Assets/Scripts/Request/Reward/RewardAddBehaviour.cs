using Request;
using UnityEngine;

public class RewardAddBehaviour : MonoBehaviour
{

    private HttpRequest _httpRequest;

    private void Start()
    {
        _httpRequest = FindObjectOfType<HttpRequest>();
    }

    public void AddReward(Reward reward)
    {
        StartCoroutine(
            _httpRequest.PostRequest(
                $"badges/user/acquire/{reward.rewardGameId}"
            )
        );
    }
}
