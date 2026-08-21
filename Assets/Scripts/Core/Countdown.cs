using System;
using System.Collections;
using UnityEngine;

public class Countdown : MonoBehaviour
{
    private Coroutine _coroutine;

    public event Action<float> ProgressChanged;
    public event Action Finished;

    public void Begin(float duration)
    {
        Stop();
        _coroutine = StartCoroutine(TrackTime(duration));
    }

    public void Stop()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private IEnumerator TrackTime(float duration)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float progress = elapsed / duration;
            ProgressChanged?.Invoke(progress);

            yield return null;
        }

        _coroutine = null;
        Finished?.Invoke();
    }
}
