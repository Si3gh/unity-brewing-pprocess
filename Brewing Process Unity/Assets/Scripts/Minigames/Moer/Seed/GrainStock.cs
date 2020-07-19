using System.Collections.Generic;
using UnityEngine;

public class GrainStock : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] Grain[] grains;
    [SerializeField] private float secondsSpentInProcessor = 5;
#pragma warning restore 0649

    private Stack<Grain> storedGrains;

    public int RemainingGrains => storedGrains.Count;

    public void Start()
    {
        ReestockGrains();
    }

    internal void ReestockGrains()
    {
        storedGrains = new Stack<Grain>(grains);
        foreach (var grain in storedGrains)
        {
            grain.ResetGrain(secondsSpentInProcessor);
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
