using System;
using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Renderer _renderer;
    private CubeColorChanger _cubeColorChanger = new CubeColorChanger();
    private Coroutine _coroutine;

    private float _minLifeTime = 2;
    private float _maxLifeTime = 5;
    private bool _isColorChanged = false;

    public event Action<Cube> TimeOut;
    public Renderer Renderer => _renderer;

    private void OnEnable()
    {
        _isColorChanged = false;
        _coroutine = null;
        _cubeColorChanger.ResetColor(this);
    }

    private void OnDisable()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Ground>())
        {
            if (!_isColorChanged)
            {
                _cubeColorChanger.SetColor(this);
                _isColorChanged = true;
            }

            if (_coroutine == null)
            {
                _coroutine = StartCoroutine(Counter());
            }
        }
    }

    private IEnumerator Counter()
    {
        yield return new WaitForSeconds(InitializedLifeTime());
        gameObject.SetActive(false);
        TimeOut?.Invoke(this);
    }

    public float InitializedLifeTime()
    {
        return UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
    }
}