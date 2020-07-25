using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderAnimationINtegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Animator _rightGrinderAnimator;
    [SerializeField] private Animator _leftGrinderAnimator;
#pragma warning disable 0649

    private GrainProcessor _grainProcessor;
    private MachineController _machineController;
    void Start()
    {
        _grainProcessor = FindObjectOfType<GrainProcessor>();
        _machineController = FindObjectOfType<MachineController>();
    }

    void Update()
    {
        var currentPotency = _grainProcessor.CurrentPotency;
        _rightGrinderAnimator.SetFloat("potency", currentPotency);
        _leftGrinderAnimator.SetFloat("potency", currentPotency);

        var active = _machineController.MachineOn;
        _rightGrinderAnimator.SetBool("active", active);
        _leftGrinderAnimator.SetBool("active", active);
    }
}
