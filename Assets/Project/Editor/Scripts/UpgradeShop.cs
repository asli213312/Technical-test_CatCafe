using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UpgradeShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private Transform scrollViewContent;
    [SerializeField] private GameObject upgradePrefab;
    [SerializeField] private MoneyCounter moneyCounter;
    [SerializeField] private PlayerData playerData;
    [SerializeField] private List<UpgradeCategory> availableCategories;

    private void Start()
    {
        UpdatePlayerLevelText(playerData.Level);
        InitializeUpgradeList();
        playerData.OnLevelChanged += OnPlayerLevelChanged;
    }
    
    private void OnDestroy()
    {
        playerData.OnLevelChanged -= OnPlayerLevelChanged;
    }

    private void OnPlayerLevelChanged(int newLevel)
    {
        UpdatePlayerLevelText(newLevel);
        RefreshUpgradeList();
    }

    private void UpdatePlayerLevelText(int newLevel)
    {
        levelText.text = "LEVEL: " + newLevel;
    }

    private void InitializeUpgradeList()
    {
        RefreshUpgradeList();
    }

    private void RefreshUpgradeList()
    {
        foreach (Transform child in scrollViewContent)
        {
            Destroy(child.gameObject);
        }

        int playerLevel = playerData.Level;

        foreach (var category in availableCategories)
        {
            if (category.RequiredLevel <= playerLevel)
            {
                foreach (var upgrade in category.Upgrades)
                {
                    if (!upgrade.IsPurchased)
                    {
                        CreateUpgrade(upgrade, category);
                    }
                }
            }
        }
    }
    
    private void CreateUpgrade(UpgradeTemplate upgrade, UpgradeCategory category)
    {
        GameObject upgradeUIInstance = Instantiate(upgradePrefab, scrollViewContent);
        UpgradeView upgradeView = upgradeUIInstance.GetComponent<UpgradeView>();

        if (upgradeView != null)
        {
            upgradeView.DisplayUpgrade(upgrade, category);
            upgradeView.SetBuyButtonCallback(OnBuyButtonClick);
        }
    }
    
    private void OnBuyButtonClick(UpgradeCallbackData data)
    {
        TryBuyUpgrade(data.Upgrade, data.Category);
    }
    
    private void TryBuyUpgrade(UpgradeTemplate upgrade, UpgradeCategory category)
    {
        if (!category.Upgrades.Contains(upgrade))
        {
            Debug.LogError("Trying to purchase an upgrade not in the category.");
            return;
        }

        if (upgrade.TryPurchase(moneyCounter, category))
            Debug.Log("Upgrade purchased: " + upgrade.Label);
    }
}
