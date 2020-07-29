using System;
using TMPro;
using UnityEngine;

public class TimerIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI textField;
#pragma warning restore 0649

    private MachineController _machineController;

    public void Start()
    {
        _machineController = FindObjectOfType<MachineController>();
    }

    void Update()
    {
        if (_machineController.RemainingTime > 0)
        {
            textField.text = String.Format("Bonus: {0:0.00}", _machineController.RemainingTime);
        }
        else
        {
            textField.text = String.Format("Bonus: 0");
        }
    }
}
