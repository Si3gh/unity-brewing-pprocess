using System.Collections.Generic;
using UnityEngine;

public class GrainStock : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Grain[] grains;
#pragma warning restore 0649

    private float _secondsSpentInProcessor = 5;
    private Stack<Grain> storedGrains;

    public int RemainingGrains => storedGrains.Count;

    public void Start()
    {
        ReestockGrains();

        var config = FindObjectOfType<GrinderConfiguration>();
        _secondsSpentInProcessor = config.SecondsSpentInProcessor;
    }

    internal void ReestockGrains()
    {
        storedGrains = new Stack<Grain>(grains);
        foreach (var grain in storedGrains)
        {
            grain.ResetGrain(_secondsSpentInProcessor);
        }
    }

    public void ReleaseGrain()
    {
        if(RemainingGrains == 0)
        {
            WarnNotEnoughtGrains();
            return;
        }

        var grain = storedGrains.Pop();
        grain.Release();
    }

    private void WarnNotEnoughtGrains()
    {
        Debug.LogError("Sem grãos suficientes");
    }
}
