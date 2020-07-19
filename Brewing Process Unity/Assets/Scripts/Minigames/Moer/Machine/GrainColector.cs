using System.Collections.Generic;
using UnityEngine;

public class GrainColector : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private BoxCollider boxCollider;
#pragma warning restore 0649

    private MachineController _machineController;
    private List<Grain> _collectedGrains = new List<Grain>();

    public List<Grain> CollectedGrains => new List<Grain>(_collectedGrains.ToArray());
    public int QuantityCollected => _collectedGrains.Count;
    public BoxCollider Collider => boxCollider;

    public void Start()
    {
        _machineController = FindObjectOfType<MachineController>();    
    }

    public void Collect(Grain grain)
    {
        grain.MarkAsCollected();
        _collectedGrains.Add(grain);

        if (QuantityCollected >= _machineController.QuantityToCollect)
        {
            _machineController.StopMachine();
        }
    }

    internal void ResetCollector()
    {
        _collectedGrains.Clear();
    }
}
