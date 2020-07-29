using System.Linq;
using TMPro;
using UnityEngine;

public class ScoreIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject scoreBoard;
    [SerializeField] private TextMeshProUGUI textField;
#pragma warning restore 0649

    private MachineController _machineController;
    private GrainColector _grainColector;

    void Awake()
    {
        _grainColector = FindObjectOfType<GrainColector>();
        _machineController = FindObjectOfType<MachineController>();
    }

    public void ShowScore()
    {
        scoreBoard.SetActive(true);
        var grainsProcessed = _grainColector.CollectedGrains;
        string result = $"Score Timer: {_machineController.RemainingTime}\n";

        foreach (var grain in grainsProcessed)
        {
            result += $"Grão processado: {grain.ProcessedValue}\n";
        }

        textField.text = result;
    }
}
