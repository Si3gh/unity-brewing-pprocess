using System;
using UnityEngine;

public class GrainProcessor : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private BoxCollider boxCollider;
#pragma warning restore 0649

    private int _potencyIncreaseAmount;
    private int _potencyDecreaseAmount;
    private float _decreaseSpeed;
    private float _inititalPotency;
    private float _currentPotency;
    private MachineController _machineController;

    public float CurrentPotency => _currentPotency;
    public BoxCollider Collider => boxCollider;

    public void Start()
    {
        _machineController = FindObjectOfType<MachineController>();

        var config = FindObjectOfType<GrinderConfiguration>();
        _potencyIncreaseAmount = config.PotencyIncreaseAmount;
        _potencyDecreaseAmount = config.PotencyDecreaseAmount;
        _decreaseSpeed = config.DecreaseSpeed;
        _inititalPotency = config.InititalPotency;
    }

    public void Update()
    {
        if (_machineController.MachineOn)
        {
            _currentPotency -= Time.deltaTime * _decreaseSpeed;
            ValidatePotency();
        }
    }

    private void ValidatePotency()
    {
        if (_currentPotency <= 0)
        {
            _machineController.StopMachine();
        }
    }

    public void IncreasePotency()
    {
        if (!_machineController.MachineOn)
        {
            return;
        }

        _currentPotency += _potencyIncreaseAmount;
        if (_currentPotency > _machineController.MaxPotency)
        {
            _machineController.StopMachine();
        }
    }

    public void DecreasePotency()
    {
        if (!_machineController.MachineOn)
        {
            return;
        }

        _currentPotency -= _potencyDecreaseAmount;
        if (_currentPotency < _machineController.MinPotency)
        {
            _machineController.StopMachine();
        }
    }

    internal void ResetPotency()
    {
        _currentPotency = _inititalPotency;
    }
}
