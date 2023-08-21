namespace Project.Editor.Scripts
{
    public interface IUpgrade
    {
        bool IsPurchased { get; }
        bool TryPurchase(MoneyCounter moneyCounter, UpgradeCategory category);
        void NotifyObservers(UpgradeCategory category);
    }
}