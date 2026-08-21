using System;
using UnityEngine;

[RequireComponent(typeof(Countdown))]
public class Cube : MonoBehaviour, IPoolable<Cube>
{
    [SerializeField] private Renderer _renderer;
    private CubeColorChanger _cubeColorChanger = new CubeColorChanger();
    private Countdown _countdown;

    private float _minLifeTime = 2;
    private float _maxLifeTime = 5;
    private bool _isColorChanged;

    public event Action<Cube> TimeOut;

    public Renderer Renderer => _renderer;

    private void Awake()
    {
        _countdown = GetComponent<Countdown>();
    }

    private void OnEnable()
    {
        _isColorChanged = false;
        _countdown.Finished += Counter;
        _cubeColorChanger.ResetColor(this);
    }

    private void OnDisable()
    {
        _countdown.Finished -= Counter;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            if (!_isColorChanged)
            {
                _cubeColorChanger.SetColor(this);
                _isColorChanged = true;
                _countdown.Begin(InitializedLifeTime());
            }
        }
    }

    private void Counter()
    {
        gameObject.SetActive(false);
        TimeOut?.Invoke(this);
    }

    public float InitializedLifeTime()
    {
        return UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
    }
}