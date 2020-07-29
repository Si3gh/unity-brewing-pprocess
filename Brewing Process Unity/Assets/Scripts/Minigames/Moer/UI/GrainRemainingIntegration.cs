using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GrainRemainingIntegration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private Image barFill;
#pragma warning restore 0649

    private GrainStock _grainStock;

    public void Start()
    {
        _grainStock = FindObjectOfType<GrainStock>();
    }

    void Update()
    {
        barFill.fillAmount = MathfExtensions.Map(_grainStock.RemainingGrains,0, _grainStock.TotalGrains, 0,1);
    }
}
