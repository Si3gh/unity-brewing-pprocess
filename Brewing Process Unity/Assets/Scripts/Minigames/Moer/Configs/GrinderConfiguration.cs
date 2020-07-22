using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrinderConfiguration : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private int potencyIncreaseAmount;
    [SerializeField] private int potencyDecreaseAmount;
    [SerializeField] [Range(0, 10)] private float decreaseSpeed = 5;
    [SerializeField] [Range(0, 100)] private float inititalPotency = 50;
    [SerializeField] private int quantityToCollect = 20;
    [SerializeField] private float timerValue = 50;
    [SerializeField] private float secondsSpentInProcessor = 5;
#pragma warning restore 0649

    public int PotencyIncreaseAmount => potencyIncreaseAmount;
    public int PotencyDecreaseAmount => potencyDecreaseAmount;
    public float DecreaseSpeed => decreaseSpeed;
    public float InititalPotency => inititalPotency;
    public int QuantityToCollect => quantityToCollect;
    public float TimerValue => timerValue;
    public float SecondsSpentInProcessor => secondsSpentInProcessor;
}
