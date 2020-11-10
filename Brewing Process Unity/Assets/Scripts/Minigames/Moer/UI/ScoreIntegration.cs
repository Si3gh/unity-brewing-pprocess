using Assets.Scripts.Minigames.Moer.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private TextMeshProUGUI textField;
    
    [Header("Range de valores para ser processado.")]
    [SerializeField] [Range(0, 2)] private float minimumToProcess = 0.8f;
    [SerializeField] [Range(0, 2)] private float maximumToProcess = 1.2f;
    [SerializeField] private float minimumTime = 20f;

    [Header("Mensagens do placar")]
    [SerializeField] private Reward TerribleReward;
    [SerializeField] private Reward PerfectReward;

    [Header("Mensagens do placar")]
    [SerializeField] private string loseMessage;
    [SerializeField] private string inconsistentMessage;
    [SerializeField] private string lowProcessedAmountMessage;
    [SerializeField] private string highProcessedAmountMessage;
    [SerializeField] private string perfectProcessedAmountMessage;

    [Header("Icones de estrela")]
    [SerializeField] private TogglebleObject[] starObjects;

    [Header("Porcentagem")]
    [SerializeField] private Image[] percentageBars;
#pragma warning restore 0649

    private MachineController _machineController;
    private RewardAdder _rewardAdder;
    private GrainColector _grainColector;
    private List<float> lowProcessedGrains;
    private List<float> perfectProcessedGrains;
    private List<float> highProcessedGrains;

    void Awake()
    {
        _rewardAdder = FindObjectOfType<RewardAdder>();
        _grainColector = FindObjectOfType<GrainColector>();
        _machineController = FindObjectOfType<MachineController>();
        ResetGrainsCount();
    }

    private void ResetGrainsCount()
    {
        lowProcessedGrains = new List<float>();
        perfectProcessedGrains = new List<float>();
        highProcessedGrains = new List<float>();
    }

    public void ShowScore(float remainingTime)
    {
        ResetGrainsCount();
        scoreBoard.SetActive(true);
        SeparateIntoScore(_grainColector.CollectedGrains);
        textField.text = AnalyseGrainsAndGetMessage();
        SetProcessedValue();
        ActivateStars(remainingTime);
    }

    private void ActivateStars(float remainingTime)
    {
        foreach (var stars in starObjects)
        {
            stars.Inactivate();
        }

        var lowQualityCount = lowProcessedGrains.Count;
        var okQualityCount = perfectProcessedGrains.Count;
        var highQualityCount = highProcessedGrains.Count;

        if (!IsValid(lowQualityCount, okQualityCount, highQualityCount))
        {
            return;
        }

        starObjects[0].Activate();

        if (okQualityCount <= highQualityCount + lowQualityCount)
        {
            return;
        }

        starObjects[1].Activate();

        if (remainingTime < minimumTime)
        {
            return;
        }

        starObjects[2].Activate();
        _rewardAdder.AddReward(PerfectReward);
    }

    private void SetProcessedValue()
    {
        foreach (var percentageBar in percentageBars)
        {
            percentageBar.color = Color.black;
        }

        var percentageIndex = 0;

        foreach (var item in lowProcessedGrains)
        {
            percentageBars[percentageIndex].color = Color32Factory.BluePotencyColor();
            percentageIndex++;
        }

        foreach (var item in perfectProcessedGrains)
        {
            percentageBars[percentageIndex].color = Color32Factory.GreenPotencyColor();
            percentageIndex++;
        }

        foreach (var item in highProcessedGrains)
        {
            percentageBars[percentageIndex].color = Color32Factory.RedPotencyColor();
            percentageIndex++;
        }
    }

    private string AnalyseGrainsAndGetMessage()
    {
        var lowQualityCount = lowProcessedGrains.Count;
        var okQualityCount = perfectProcessedGrains.Count;
        var highQualityCount = highProcessedGrains.Count;

        if (!IsValid(lowQualityCount, okQualityCount, highQualityCount))
        {
            return loseMessage;
        }

        if (lowQualityCount >= highQualityCount + okQualityCount)
        {
            return lowProcessedAmountMessage;
        }

        if (highQualityCount >= okQualityCount + lowQualityCount)
        {
            return highProcessedAmountMessage;
        }

        if (lowQualityCount + highQualityCount >= okQualityCount)
        {
            return inconsistentMessage;
        }

        return perfectProcessedAmountMessage;
    }

    private static bool IsValid(int lowQualityCount, int okQualityCount, int highQualityCount)
    {
        return lowQualityCount + okQualityCount + highQualityCount == 20;
    }

    private void SeparateIntoScore(List<Grain> grainsProcessed)
    {
        if (grainsProcessed.Count == 0)
        {
            _rewardAdder.AddReward(TerribleReward);
        }

        foreach (var grain in grainsProcessed)
        {
            if (grain.ProcessedValue < minimumToProcess)
            {
                lowProcessedGrains.Add(grain.ProcessedValue);
                continue;
            }

            if (grain.ProcessedValue > maximumToProcess)
            {
                highProcessedGrains.Add(grain.ProcessedValue);
                continue;
            }

            perfectProcessedGrains.Add(grain.ProcessedValue);
        }
    }
}
