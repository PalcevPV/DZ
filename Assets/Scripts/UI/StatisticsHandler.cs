using UnityEngine;

public class StatisticsHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    [SerializeField] private StatisticsView _cubeStatisticsView;
    [SerializeField] private StatisticsView _bombStatisticsView;


    private void OnEnable()
    {
        _cubeSpawner.StatisticsChanged += UpdateCubeValues;
        _bombSpawner.StatisticsChanged += UpdateBombValues;
    }

    private void OnDisable()
    {
        _cubeSpawner.StatisticsChanged -= UpdateCubeValues;
        _bombSpawner.StatisticsChanged -= UpdateBombValues;
    }
    private void Start()
    {
        _cubeStatisticsView.SetStatistics(_cubeSpawner.CreatedCount, _cubeSpawner.SpawnedCount, _cubeSpawner.ActiveCount);
        _bombStatisticsView.SetStatistics(_cubeSpawner.CreatedCount, _cubeSpawner.SpawnedCount, _cubeSpawner.ActiveCount);      
    }

    private void UpdateBombValues(int createdCount, int spawnedCount, int activeCount)
    {
        _bombStatisticsView.SetStatistics(createdCount, spawnedCount, activeCount);
    }

    private void UpdateCubeValues(int createdCount, int spawnedCount, int activeCount)
    {
        _cubeStatisticsView.SetStatistics(createdCount, spawnedCount, activeCount);
    }
}