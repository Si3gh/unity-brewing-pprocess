using UnityEngine;

public class MachineController : MonoBehaviour
{
    private int _quantityToCollect = 20;
    private float _timerValue = 50;
    private ScoreIntegration _scoreIntegration;
    private float remainingTime;
    private GrainProcessor _grainProcessor;
    private GrainColector _grainColector;
    private GrainStock _grainStock;

    public int QuantityToCollect => _quantityToCollect;
    public float RemainingTime => remainingTime;
    public bool MachineOn { get; private set; }
    public float MinPotency { get; private set; } = 0;
    public float MaxPotency { get; private set; } = 100;

    public void Start()
    {
        _grainProcessor = FindObjectOfType<GrainProcessor>();
        _grainColector = FindObjectOfType<GrainColector>();
        _grainStock = FindObjectOfType<GrainStock>();
        _scoreIntegration = FindObjectOfType<ScoreIntegration>();

        var config = FindObjectOfType<GrinderConfiguration>();
        _quantityToCollect = config.QuantityToCollect;
        _timerValue = config.TimerValue;
    }

    public void Update()
    {
        if (MachineOn)
        {
            remainingTime -= Time.deltaTime;
        }
    }

    public void StartMachine()
    {
        remainingTime = _timerValue;
        MachineOn = true;
        _grainProcessor.ResetPotency();
        _grainColector.ResetCollector();
        _grainStock.ReestockGrains();
    }

    public void StopMachine()
    {
        MachineOn = false;
        _scoreIntegration.ShowScore();
    }

    public bool isBroken()
    {
        var tooSlow = _grainProcessor.CurrentPotency < MinPotency;
        var tooFast = _grainProcessor.CurrentPotency > MaxPotency;
        var notCollectedAll = _grainColector.QuantityCollected < QuantityToCollect;
        return tooSlow || tooFast || notCollectedAll;
    }
}
