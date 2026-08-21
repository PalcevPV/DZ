using System;
using UnityEngine;
using UnityEngine.Pool;

public abstract class Spawner<T> : MonoBehaviour
    where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] private T _prefab;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;
    private ObjectPool<T> _pool;
    private int _spawnedCount;
    private int _createdCount = 0;
    public Action<int, int, int> StatisticsChanged;

    public int CreatedCount => _createdCount;
    public int SpawnedCount => _spawnedCount;
    public int ActiveCount => _pool.CountActive;


    private void Awake()
    {
        _pool = new ObjectPool<T>(
            createFunc: () => CreateItem(),
            actionOnGet: (spawnItem) => ActionOnGet(spawnItem),
            actionOnRelease: (spawnItem) => spawnItem.gameObject.SetActive(false),
            actionOnDestroy: (spawnItem) => Destroy(spawnItem.gameObject),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(T spawnItem)
    {
        spawnItem.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        spawnItem.gameObject.SetActive(true);
    }

    private void ReturnItem(T spawnItem)
    {
        spawnItem.TimeOut -= ReturnItem;
        _pool.Release(spawnItem);

        NotifayStatiticsChanged();
    }

    private T CreateItem()
    {
        _createdCount++;
        NotifayStatiticsChanged();

        return Instantiate(_prefab);
    }

    private void NotifayStatiticsChanged()
    {
        StatisticsChanged?.Invoke(_createdCount, _spawnedCount, _pool.CountActive);
    }

    protected T Spawn(Vector3 position)
    {
        T spawnItem = _pool.Get();

        _spawnedCount++;

        spawnItem.transform.position = position;
        spawnItem.TimeOut += ReturnItem;

        NotifayStatiticsChanged();

        return spawnItem;
    }
}