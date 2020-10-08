using Assets.Scripts.Minigames.Moer.UI;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachinePotencyIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Image barFill;
#pragma warning restore 0649

    private MachineController _machineController;
    private GrainProcessor _grainProcessor;

    public void Start()
    {
        _machineController = FindObjectOfType<MachineController>();
        _grainProcessor = FindObjectOfType<GrainProcessor>();
    }

    void Update()
    {
        barFill.fillAmount = MathfExtensions.Map(_grainProcessor.CurrentPotency, _machineController.MinPotency, _machineController.MaxPotency, 0, 1);
        barFill.color = returnColor();
    }

    private Color32 returnColor(){
        if (_grainProcessor.CurrentPotency < 35) 
        {
            return Color32Factory.BluePotencyColor();
        }
        if (_grainProcessor.CurrentPotency < 75)
        {
            return Color32Factory.GreenPotencyColor();
        }
        return Color32Factory.RedPotencyColor();

    }
}
