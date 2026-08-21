using UnityEngine;

public class EventHandler : MonoBehaviour
{
    [SerializeField] private CubeSpawner _cubeSpawner;
    [SerializeField] private BombSpawner _bombSpawner;

    private void OnEnable()
    {
        _cubeSpawner.Spawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        _cubeSpawner.Spawned -= OnCubeSpawned;
    }

    private void OnCubeSpawned(Cube cube)
    {
        cube.TimeOut += OnCubeTimeOut;
    }

    private void OnCubeTimeOut(Cube cube)
    {
        _bombSpawner.OnCubeTimeOut(cube.transform.position);
        cube.TimeOut -= OnCubeTimeOut;
    }
}