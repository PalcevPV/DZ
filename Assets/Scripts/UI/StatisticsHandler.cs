using UnityEngine;

public abstract class StatisticsHandler<T> : MonoBehaviour
    where T : MonoBehaviour, IPoolable<T>
{
    [SerializeField] private Spawner<T> _spawner;

    [SerializeField] private StatisticsView _statisticsView;

    private void OnEnable()
    {
        _spawner.StatisticsChanged += UpdateValues;
    }

    private void OnDisable()
    {
        _spawner.StatisticsChanged -= UpdateValues;
    }
    private void Start()
    {
        _statisticsView.SetStatistics(_spawner.CreatedCount, _spawner.SpawnedCount, _spawner.ActiveCount);    
    }

    private void UpdateValues(int createdCount, int spawnedCount, int activeCount)
    {
        _statisticsView.SetStatistics(createdCount, spawnedCount, activeCount);
    }
}