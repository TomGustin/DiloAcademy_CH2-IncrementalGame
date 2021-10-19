using System.Collections.Generic;
using UnityEngine;

public class AchievementSystem : MonoBehaviour, IObserver
{
    [SerializeField] private AchievementUI achievementUI;
    [SerializeField] private AchievementLibrary achievementLibrary;
    [SerializeField] private List<PointOfInterest> pois = new List<PointOfInterest>();

    private void Awake()
    {
        achievementLibrary.ResetAchievement(); // For reset achievement that was collected
        RegisterObserver();
    }

    private void OnDisable()
    {
        UnregisterObserver();
    }

    private void RegisterObserver()
    {
        foreach (PointOfInterest poi in pois) poi.Subscribe(this); 
    }

    private void UnregisterObserver()
    {
        foreach (PointOfInterest poi in pois) poi.Unsubscribe(this);
    }

    private void UnlockAchievement(string messagge)
    {
        AchievementData achievement = achievementLibrary.GetAchievement(messagge);
        
        if (achievement != null)
        {
            if (achievement.collect) return;
            achievement.collect = true;

            achievementUI.ShowAchivementPopUp(achievement.achievementName, achievement.achievementDescription);
        }
    }

    void IObserver.OnNotify(string sender, string messagge)
    {
        UnlockAchievement(messagge);
    }

    void IObserver.OnNotify(string messagge)
    {
        UnlockAchievement(messagge);
    }
}
