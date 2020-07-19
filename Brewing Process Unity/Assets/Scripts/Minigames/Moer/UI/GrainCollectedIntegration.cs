using TMPro;
using UnityEngine;

public class GrainCollectedIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI textField;
#pragma warning restore 0649

    private GrainColector _grainColector;

    public void Start()
    {
        _grainColector = FindObjectOfType<GrainColector>();
    }

    void Update()
    {
        textField.text = $"Grãos coletados: {_grainColector.QuantityCollected}";    
    }
}
