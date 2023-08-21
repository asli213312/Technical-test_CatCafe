using System;
using System.Collections;
using System.Collections.Generic;
using Project.Editor.Scripts;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Upgrades/Create Upgrade", fileName = "NewUpgrade")]
public class UpgradeTemplate : ScriptableObject, IObservable, IUpgrade
{
    public Sprite Icon;
    public UpgradeType Type;
    public int Value;
    public int Cost;
    public string Label;
    public string Description;
    [SerializeField] private bool isPurchased;

    public bool IsPurchased { get => isPurchased; private set => isPurchased = value; }

    private readonly Dictionary<UpgradeCategory, List<IObserver>> _observers = new Dictionary<UpgradeCategory, List<IObserver>>();
    
    public bool TryPurchase(MoneyCounter moneyCounter, UpgradeCategory category)
    {
        if (!IsPurchased && moneyCounter.HasEnoughMoney(Cost))
        {
            moneyCounter.SubtractMoney(Cost);
            IsPurchased = true;
            
            NotifyObservers(category);
            return true;
        }
        else
        {
            Debug.Log("Not enough money to buy this upgrade.");
        }
        return false;
    }

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
