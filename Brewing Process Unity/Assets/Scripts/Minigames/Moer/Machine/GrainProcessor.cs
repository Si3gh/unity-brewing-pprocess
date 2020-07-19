using UnityEngine;

public class GrainProcessor : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private int potencyIncreaseAmount;
    [SerializeField] private int potencyDecreaseAmount;
    [SerializeField] [Range(0, 10)] private float decreaseSpeed = 5;
    [SerializeField] [Range(0, 100)] private float inititalPotency = 50;
    [SerializeField] private BoxCollider boxCollider;
#pragma warning restore 0649

    private float currentPotency;
    private MachineController _machineController;

    public float CurrentPotency => currentPotency;
    public BoxCollider Collider => boxCollider;

    public void Start()
    {
        _machineController = FindObjectOfType<MachineController>();
    }

    public void Update()
    {
        if (_machineController.MachineOn)
        {
            currentPotency -= Time.deltaTime * decreaseSpeed;
        }
    }

    public void IncreasePotency()
    {
        if (!_machineController.MachineOn)
        {
            return;
        }

        currentPotency += potencyIncreaseAmount;
        if (currentPotency > _machineController.MaxPotency)
        {
            _machineController.StopMachine();
        }
    }

    public void DecreasePotency()
    {
        if (!_machineController.MachineOn)
        {
            return;
        }

        currentPotency -= potencyDecreaseAmount;
        if (currentPotency < _machineController.MinPotency)
        {
            _machineController.StopMachine();
        }
    }

    internal void ResetPotency()
    {
        currentPotency = inititalPotency;
    }
}
