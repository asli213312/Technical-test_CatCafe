using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;

public class MoneyCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;
    private int _money;
    
    private event Action<int> OnMoneyAdded;
    
    private void OnEnable()
    {
        OnMoneyAdded += UpdateOnMoney;
    }

    private void OnDestroy()
    {
        OnMoneyAdded -= UpdateOnMoney;
    }

    private void UpdateOnMoney(int newMoney)
    {
        _money += newMoney;
        
        if (_money < 0) _money = 0;
        UpdateMoneyText();     
    }
    
    private void UpdateMoneyText()
    {
        moneyText.text = _money.ToString();
    }

    public bool HasEnoughMoney(int amount) => _money >= amount;

    public void SubtractMoney(int amount) => AddMoney(-amount);
    
    public void AddMoney(int amount) => OnMoneyAdded?.Invoke(amount);

    public int GetCurrentMoney() => _money;
}
