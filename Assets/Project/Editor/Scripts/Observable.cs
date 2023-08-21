using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observable : IObservable
{
    private Dictionary<UpgradeCategory, List<IObserver>> _observers;

    public void AddObserver(IObserver observer, UpgradeCategory category)
    {
        if (!_observers.ContainsKey(category))
        {
            _observers[category] = new List<IObserver>();
        }
        
        if (!_observers[category].Contains(observer))
        {
            _observers[category].Add(observer);
        }
    }

    public void RemoveObserver(IObserver observer, UpgradeCategory category)
    {
        if (_observers.ContainsKey(category))
        {
            _observers[category].Remove(observer);
        }
    }

    public void NotifyObservers(UpgradeCategory category)
    {
        if (_observers.ContainsKey(category))
        {
            foreach (var observer in _observers[category])
            {
                observer.OnObservableUpdate(this, category);
            }
        }
    }
}
