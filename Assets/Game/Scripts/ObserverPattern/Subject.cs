using System.Collections.Generic;
using UnityEngine;

public abstract class Subject : MonoBehaviour
{
    protected List<IObserver> observers = new List<IObserver>();

    public void Subscribe(IObserver observer)
    {
        if (!observers.Contains(observer)) observers.Add(observer);
    }

    public void Unsubscribe(IObserver observer)
    {
        if (observers.Contains(observer)) observers.Remove(observer);
    }

    public void OnNotify(string message)
    {
        foreach (IObserver observer in observers) observer.OnNotify(message);
    }

    public abstract void OnNotify(string sender, string message);
}
