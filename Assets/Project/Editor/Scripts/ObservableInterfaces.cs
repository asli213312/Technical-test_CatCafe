using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObservable
{
    void AddObserver(IObserver observer, UpgradeCategory category);
    void RemoveObserver(IObserver observer, UpgradeCategory category);
    void NotifyObservers(UpgradeCategory category);
}

public interface IObserver
{
    void OnObservableUpdate(IObservable observable, UpgradeCategory category);
}
