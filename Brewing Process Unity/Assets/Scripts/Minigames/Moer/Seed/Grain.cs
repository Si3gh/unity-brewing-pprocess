using UnityEngine;

public class Grain : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField] private GameObject unprocessedModel;
    [SerializeField] private GameObject processedModel;
#pragma warning restore 0649

    private Rigidbody _rigidbody;
    private GrainColector _grainColector;
    private GrainProcessor _grainProcessor;
    private float _maxSecondsSpendInProcessor;
    private Vector3 _initialPosition;

    private float secondsSpendInProcessor = 0;
    public float ProcessedValue { get; private set; }
    public GrainStatus Status { get; private set; }

    public void Awake()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _initialPosition = gameObject.GetComponentInParent<Transform>().position;
    }

    public void Start()
    {
        _grainColector = FindObjectOfType<GrainColector>();
        _grainProcessor = FindObjectOfType<GrainProcessor>();
        ValidateColliders();
    }

    public void Update()
    {
        if(Status == GrainStatus.PROCESSING)
        {
            secondsSpendInProcessor += Time.deltaTime;

            if (secondsSpendInProcessor > _maxSecondsSpendInProcessor)
            {
                MarkAsProcessed();
            }
        }
    }

    public void AddProcessValue(float processedValueAdded)
    {
        if (Status != GrainStatus.PROCESSING)
        {
            return;
        }

        ProcessedValue += processedValueAdded;
    }

    public void Release()
    {
        Status = GrainStatus.RELEASED;
        gameObject.SetActive(true);
    }

    public void StartProcessing()
    {
        Status = GrainStatus.PROCESSING;
        unprocessedModel.SetActive(false);
        _rigidbody.isKinematic = true;
        _rigidbody.detectCollisions = false;
    }

    private void MarkAsProcessed()
    {
        Status = GrainStatus.PROCESSED;
        gameObject.SetActive(true);
        processedModel.SetActive(true);
        _rigidbody.isKinematic = false;
        _rigidbody.detectCollisions = true;
    }

    public void MarkAsCollected()
    {
        Status = GrainStatus.COLLECTED;
        gameObject.SetActive(false);
    }

    public void ResetGrain(float maxSecondsSpendInProcessor)
    {
        Status = GrainStatus.STORED;
        _maxSecondsSpendInProcessor = maxSecondsSpendInProcessor;
        secondsSpendInProcessor = 0;
        ProcessedValue = 0;
        gameObject.transform.position = _initialPosition;
        _rigidbody.detectCollisions = true;
        _rigidbody.isKinematic = false;
        processedModel.SetActive(false);
        unprocessedModel.SetActive(true);
        gameObject.SetActive(false);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other == _grainProcessor.Collider && Status == GrainStatus.RELEASED)
        {
            _grainProcessor.DecreasePotency();
            StartProcessing();
            return;
        }

        if (other == _grainColector.Collider && Status == GrainStatus.PROCESSED)
        {
            _grainColector.Collect(this);
            MarkAsCollected();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other == _grainProcessor.Collider && Status == GrainStatus.PROCESSING)
        {
            ProcessedValue += _grainProcessor.CurrentPotency * Time.deltaTime;
        }
    }

    private void ValidateColliders()
    {
        if (_grainProcessor is null || _grainProcessor.Collider is null)
        {
            Debug.LogError("Falta um objeto do tipo GrainCollector");
        }

        if (_grainColector is null || _grainColector.Collider is null)
        {
            Debug.LogError("Falta um objeto do tipo GrainProcessor");
        }
    }
}
