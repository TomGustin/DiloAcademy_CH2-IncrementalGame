using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest : Subject
{
    [SerializeField] private string poiName;

    public override void OnNotify(string sender, string message)
    {
        foreach (IObserver observer in observers) observer.OnNotify(sender, message);
    }
}
