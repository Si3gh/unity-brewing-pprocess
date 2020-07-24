using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrainCollectedIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Image barFill;
#pragma warning restore 0649

    private MachineController _machineController;
    private GrainColector _grainColector;

    public void Start()
    {
        _machineController = FindObjectOfType<MachineController>();
        _grainColector = FindObjectOfType<GrainColector>();
    }

    void Update()
    {
        barFill.fillAmount = MathfExtensions.Map(_grainColector.QuantityCollected, 0, _machineController.QuantityToCollect, 0, 1); 
    }
}
