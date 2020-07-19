using System;
using TMPro;
using UnityEngine;

public class MachinePotencyIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI textField;
#pragma warning restore 0649

    private GrainProcessor _grainProcessor;

    public void Start()
    {
        _grainProcessor = FindObjectOfType<GrainProcessor>();
    }

    void Update()
    {
        textField.text = String.Format("Potencia da máquina: {0:0.00}", _grainProcessor.CurrentPotency);
    }
}
