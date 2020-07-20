using UnityEngine;
using System;

[Serializable]
public class Stat 
{
 
    [SerializeField]
    private BarScript bar = null;

    [SerializeField]
    private float maxVal;
    
    [SerializeField]
    private float currentVal;

    public float CurrentVal {
        get => currentVal;
        set {
            
            this.currentVal = value;
            bar.Value = currentVal;
        } 
    }

    public float MaxVal {
        get => maxVal;
        set {
            this.maxVal = value;
            bar.MaxValue = maxVal;
        }
    }
}
