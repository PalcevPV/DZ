using UnityEngine;
using UnityEngine.Pool;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _prefab;
    [SerializeField] private float _repeatRate = 3f;
    [SerializeField] private int _poolCapacity = 5;
    [SerializeField] private int _poolMaxSize = 5;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_prefab),
            actionOnGet: (cube) => ActionOnGet(cube),
            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void ActionOnGet(Cube cube)
    {
        cube.transform.position = GeneratePosition();
        cube.GetComponent<Rigidbody>().linearVelocity = Vector3.zero;
        cube.gameObject.SetActive(true);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube()
    {
        Cube cube = _pool.Get();
        cube.TimeOut += ReturnCube;
    }

    private void ReturnCube(Cube cube)
    {
        cube.TimeOut -= ReturnCube;
        _pool.Release(cube);
    }

    private Vector3 GeneratePosition()
    {
        float minPositionX = -20f, maxPositionX = 20f;
        float minPositionY = 40f, maxPositionY = 45f;
        float minPositionZ = -20f, maxPositionZ = 20f;

        return new Vector3(
        Random.Range(minPositionX, maxPositionX),
        Random.Range(minPositionY, maxPositionY),
        Random.Range(minPositionZ, maxPositionZ));
    }
}