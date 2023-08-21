using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeView : MonoBehaviour, IObserver
{
    [SerializeField] private Image iconImage;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private TextMeshProUGUI valueText;
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Button buyButton;
    
    private UpgradeTemplate _upgrade;
    private UpgradeCategory _category;
    private event Action<UpgradeCallbackData> _buyButtonCallback;

    private void Awake()
    {
        buyButton.onClick.AddListener(InvokeBuyButtonCallback);
    }
    
    private void OnDestroy()
    {
        buyButton.onClick.RemoveListener(InvokeBuyButtonCallback);
        if (_upgrade != null && _category != null)
        {
            _upgrade.RemoveObserver(this, _category);
        }
    }

    public void DisplayUpgrade(UpgradeTemplate upgrade, UpgradeCategory category)
    {
        _upgrade = upgrade;
        _category = category;

        iconImage.sprite = upgrade.Icon;
        typeText.text = "TYPE: " + upgrade.Type.ToString().ToUpper();
        valueText.text = "VALUE: " + upgrade.Value;
        labelText.text = upgrade.Label;
        descriptionText.text = upgrade.Description;
    
        _upgrade.AddObserver(this, _category);
        UpdateUIState();
    }
    
    public void OnObservableUpdate(IObservable observable, UpgradeCategory category)
    {
        if (ReferenceEquals(observable, _upgrade) && category == _category)
        {
            UpdateUIState();
        }
    }
    
    private void UpdateUIState()
    {
        if (_upgrade.IsPurchased)
            SetUpgradePurchased();
        else
            SetUpgradeAvailable();
    }

    private void SetUpgradePurchased()
    {
        costText.text = "BOUGHT";
        buyButton.interactable = false;
    }

    private void SetUpgradeAvailable()
    {
        costText.text = _upgrade.Cost.ToString();
        buyButton.interactable = true;
    }

    private void InvokeBuyButtonCallback()
    {
        if (_buyButtonCallback != null)
        {
            _buyButtonCallback.Invoke(new UpgradeCallbackData { Upgrade = _upgrade, Category = _category });
        }
    }
    
    public void SetBuyButtonCallback(Action<UpgradeCallbackData> callback)
    {
        _buyButtonCallback = callback;
    }

    public Button GetBuyButton() => buyButton;
}
