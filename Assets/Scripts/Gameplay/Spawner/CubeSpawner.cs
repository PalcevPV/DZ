using System;
using UnityEngine;

public class CubeSpawner : Spawner<Cube>
{
    [SerializeField] private float _repeatRate = 3f;

    public event Action<Cube> Spawned;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnCube), 0, _repeatRate);
    }

    private void SpawnCube()
    {
        Cube cube = Spawn(GeneratePosition());
        Spawned?.Invoke(cube);
    }

    private Vector3 GeneratePosition()
    {
        float minPositionX = -20f, maxPositionX = 20f;
        float minPositionY = 40f, maxPositionY = 45f;
        float minPositionZ = -20f, maxPositionZ = 20f;

        return new Vector3(
        UnityEngine.Random.Range(minPositionX, maxPositionX),
        UnityEngine.Random.Range(minPositionY, maxPositionY),
        UnityEngine.Random.Range(minPositionZ, maxPositionZ));
    }
}
