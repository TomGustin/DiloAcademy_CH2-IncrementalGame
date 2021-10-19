using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    [SerializeField] private PointOfInterest poi;
    private static GameManagement instance;

    private int tapCount = 0;

    public static GameManagement Instance
    {
        get
        {
            if (!instance) instance = FindObjectOfType<GameManagement>();
            return instance;
        }
    }

    public void CollectByTap(Vector3 tapPosition, Transform parent)
    {
        ResourceSystem.Instance.CollectResource(out double output);
        tapCount++;
        //HUD.Instance.ShowTap(tapPosition, parent, output.ToString("0"));
        ResourceSystem.Instance.AddGold(output);

        if (tapCount == 1 || tapCount == 10 || tapCount == 100) poi.OnNotify($"tap-{tapCount}");
    }
}
