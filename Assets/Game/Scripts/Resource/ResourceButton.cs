using UnityEngine;

public class ResourceButton : MonoBehaviour
{
    [SerializeField] private ResourceUIButton ruib;
    private ResourceConfig config;

    private void Start()
    {
        ruib.Init(OnClick);
    }

    public void SetConfig(ResourceConfig config)
    {
        this.config = config;
        string desc = $"Unlock {config.name}!";
        string unlock_cost = $"Unlock Cost\n{config.GetUnlockCost()}";
        string upgrade_cost = $"Upgrade Cost\n{config.GetUpgradeCost()}";
        ruib.ChangeType(ResourceUIButton.ButtonType.Lock);
        ruib.UpdateDetail(desc, upgrade_cost, unlock_cost);
    }

    public ResourceConfig GetResourceConfig()
    {
        return config;
    }

    public void SetBuyable(bool isBuyable)
    {
        ruib.SetBuyable(isBuyable);
    }

    private void OnClick()
    {
        if (config.level == 0) UnlockResource();
        else UpgradeResource();

        ResourceSystem.Instance.UpdateResource();
    }

    private void UpgradeResource()
    {
        if (ResourceSystem.Instance.GoldEarn() < config.GetUpgradeCost()) return;
        ResourceSystem.Instance.AddGold(-config.GetUpgradeCost());
        config.level++;
        string desc = $"{config.name} Lv. {config.level}\n+{config.GetOutput().ToString("0")}";
        string unlock_cost = $"Unlock Cost\n{config.GetUnlockCost()}";
        string upgrade_cost = $"Upgrade Cost\n{config.GetUpgradeCost()}";
        ruib.UpdateDetail(desc, upgrade_cost, unlock_cost);
    }

    private void UnlockResource()
    {
        if (ResourceSystem.Instance.GoldEarn() < config.GetUnlockCost()) return;
        ResourceSystem.Instance.AddGold(-config.GetUnlockCost());
        config.level++;
        string desc = $"{config.name} Lv. {config.level}\n+{config.GetOutput().ToString("0")}";
        string unlock_cost = $"Unlock Cost\n{config.GetUnlockCost()}";
        string upgrade_cost = $"Upgrade Cost\n{config.GetUpgradeCost()}";
        ruib.ChangeType(ResourceUIButton.ButtonType.Unlock);
        ruib.UpdateDetail(desc, upgrade_cost, unlock_cost);

        ResourceSystem.Instance.UnlockResource($"building-{config.name.ToLower()}");
    }

    [System.Serializable]
    public class ResourceConfig
    {
        public string name;
        public int level;
        public double unlockCost;
        public double upgradeCost;
        public double output;

        public double GetOutput()
        {
            return output * level;
        }

        public double GetUpgradeCost()
        {
            return upgradeCost * level;
        }

        public double GetUnlockCost()
        {
            return unlockCost;
        }
    }
}
