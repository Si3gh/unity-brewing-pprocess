using TMPro;
using UnityEngine;

public class GrainRemainingIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private TextMeshProUGUI textField;
#pragma warning restore 0649

    private GrainStock _grainStock;

    public void Start()
    {
        _grainStock = FindObjectOfType<GrainStock>();
    }

    void Update()
    {
        textField.text = $"Grãos restantes : {_grainStock.RemainingGrains}";
    }
}
