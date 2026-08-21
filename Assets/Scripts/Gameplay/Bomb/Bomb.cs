using System;
using UnityEngine;

[RequireComponent(typeof(Exploder), typeof(Renderer), typeof(Countdown))]
public class Bomb : MonoBehaviour, IPoolable<Bomb>
{
    private Exploder _exploder;
    private AlphaChanger _alphaChanger = new AlphaChanger();
    private Renderer _renderer;
    private Countdown _countdown;

    private float _minLifeTime = 2;
    private float _maxLifeTime = 5;
    private float _lifeTime;
    private float _explosionRadius = 20f;
    private float _explosionForce = 1000f;
    private float _maxAlpha = 1f;

    public event Action<Bomb> TimeOut;

    private void Awake()
    {
        _exploder = GetComponent<Exploder>();
        _renderer = GetComponent<Renderer>();
        _countdown = GetComponent<Countdown>();
    }

    private void OnEnable()
    {
        Initialization();

        _alphaChanger.ResetAlpha(_renderer);

        _countdown.ProgressChanged += ChangeAlpha;
        _countdown.Finished += Explode;

        _countdown.Begin(_lifeTime);
    }

    private void OnDisable()
    {
        _countdown.ProgressChanged -= ChangeAlpha;
        _countdown.Finished -= Explode;

        _countdown.Stop();
    }

    private void Initialization()
    {
        _lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
    }

    private void ChangeAlpha(float progress)
    {
        _alphaChanger.SetAlpha(_renderer, _maxAlpha - progress);
    }

    private void Explode()
    {
        _exploder.ExplodeInRadius(transform.position, _explosionRadius, _explosionForce);

        TimeOut?.Invoke(this);
    }
}