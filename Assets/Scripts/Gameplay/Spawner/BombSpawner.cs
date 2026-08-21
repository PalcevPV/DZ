using UnityEngine;

public class BombSpawner : Spawner<Bomb>
{
    public void OnCubeTimeOut(Vector3 position)
    {
        Spawn(position);
    }
}