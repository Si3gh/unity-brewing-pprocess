using UnityEngine;
using System.Collections;

public static class MathfExtensions
{
    public static float Map(float Value, float inMin, float inMax, float outMin, float outMax)
    {
        return (Value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
}
