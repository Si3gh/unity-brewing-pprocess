using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MachinePotencyIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI textField;
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
        textField.text = String.Format("Potencia da máquina: {0:0.00}", _grainProcessor.CurrentPotency);
        barFill.color = returnColor();
    }

    private Color32 returnColor(){
        if (_grainProcessor.CurrentPotency < 35) 
        {
            return new Color32(75, 157, 179, 255); //BLUE
        }
        if (_grainProcessor.CurrentPotency < 75)
        {
           return new Color32(67, 233, 55, 255); //GREEN
        }
        return  new Color32(233, 79, 55, 255); //GREEN

    }
}
