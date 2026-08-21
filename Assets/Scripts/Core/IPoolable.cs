using System;

public interface IPoolable<T>
{
    public event Action<T> TimeOut;

    public void ResetObject();
}
