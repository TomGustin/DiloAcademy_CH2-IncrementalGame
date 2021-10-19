using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI goldInfo;
    [SerializeField] private TextMeshProUGUI autoCollectInfo;
    [SerializeField] private UITapText tapTextPrefab;

    private static HUD instance;

    public static HUD Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<HUD>();
            return instance;
        }
    }

    private List<UITapText> tapTextPool = new List<UITapText>();

    public void UpdateAutoCollectText(string text)
    {
        autoCollectInfo.text = $"Auto Collect : {text} / second";
    }

    public void UpdateGoldText(string text)
    {
        goldInfo.text = $"Gold : {text}";
    }

    public void ShowTap(Vector2 tapPosition, Transform parent, string value)
    {
        UITapText tapText = GetOrCreateTapText();
        tapText.transform.SetParent(parent, false);
        tapText.transform.position = tapPosition;

        tapText.text.text = $"+{value}";
        tapText.gameObject.SetActive(true);
    }

    private UITapText GetOrCreateTapText()
    {
        UITapText tapText = tapTextPool.Find(x => !x.gameObject.activeSelf);

        if (!tapText)
        {
            tapText = Instantiate(tapTextPrefab).GetComponent<UITapText>();
            tapTextPool.Add(tapText);
        }

        return tapText;
    }
}
