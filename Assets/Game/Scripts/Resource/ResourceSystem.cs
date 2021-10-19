using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSystem : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float autoCollectPercentage;
    [SerializeField] private ResourceButton.ResourceConfig[] resourceConfigs;
    [SerializeField] private Transform resourcesParent;
    [SerializeField] private ResourceButton resourcePrefab;
    [SerializeField] private PointOfInterest poi;

    private List<ResourceButton> activeResources = new List<ResourceButton>();
    private double collectTimer;
    private double goldEarn;

    private static ResourceSystem instance;

    public static ResourceSystem Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<ResourceSystem>();
            return instance;
        }
    }

    void Start()
    {
        AddAllResources();
    }

    void Update()
    {
        collectTimer += Time.unscaledDeltaTime;

        if (collectTimer >= 1f)
        {
            CollectResource();
            collectTimer = 0f;
        }

        CheckResourceCost();
    }

    public void CollectResource(out double output)
    {
        output = 0;

        foreach (ResourceButton resource in activeResources)
        {
            if (resource.GetResourceConfig().level > 0) output += resource.GetResourceConfig().GetOutput();
        }
    }

    public void UpdateResource()
    {
        for (int i = 0; i < activeResources.Count; i++)
        {
            if (i != 0)
            {
                if (activeResources[i - 1].GetResourceConfig().level > 0)
                {
                    activeResources[i].gameObject.SetActive(true);
                }
            } else
            {
                if (activeResources[i].GetResourceConfig().level == 0)
                {
                    activeResources[i].gameObject.SetActive(true);
                }
            }
        }
    }

    public void UnlockResource(string resourceID)
    {
        poi.OnNotify(resourceID);
    }

    private void AddAllResources()
    {
        foreach (ResourceButton.ResourceConfig config in resourceConfigs)
        {
            GameObject go = Instantiate(resourcePrefab.gameObject, resourcesParent, false);
            ResourceButton resource = go.GetComponent<ResourceButton>();

            resource.SetConfig(config);
            go.SetActive(false);

            activeResources.Add(resource);
        }

        UpdateResource();
    }

    private void CollectResource()
    {
        CollectResource(out double output);

        output *= autoCollectPercentage;

        HUD.Instance.UpdateAutoCollectText(output.ToString("F1"));

        AddGold(output);
    }

    private void CheckResourceCost()
    {
        foreach (ResourceButton resource in activeResources)
        {
            bool isBuyable;
            if (resource.GetResourceConfig().level > 0)
            {
                isBuyable = goldEarn >= resource.GetResourceConfig().GetUpgradeCost();
            }
            else
            {
                isBuyable = goldEarn >= resource.GetResourceConfig().GetUnlockCost();
            }

            resource.SetBuyable(isBuyable);
        }
    }

    public void AddGold(double value)
    {
        goldEarn += value;
        HUD.Instance.UpdateGoldText(goldEarn.ToString("0"));
    }

    public double GoldEarn()
    {
        return goldEarn;
    }
}
