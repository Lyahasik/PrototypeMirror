using UnityEngine;
using Zenject;

public class SpawnPoint : MonoBehaviour
{
    private PointStorage _pointStorage;

    private bool _isLock;

    public bool IsLock
    {
        get => _isLock;
        set => _isLock = value;
    }

    [Inject]
    public void Construct(PointStorage pointStorage)
    {
        _pointStorage = pointStorage;
    }

    private void Start()
    {
        _pointStorage.AddPoint(this);
    }
}
