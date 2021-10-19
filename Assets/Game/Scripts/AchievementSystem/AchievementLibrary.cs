using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Create Achievement Library", fileName = "Achievement Library")]
public class AchievementLibrary : ScriptableObject
{
    [SerializeField] private List<AchievementData> achievements = new List<AchievementData>();

    public AchievementData GetAchievement(string achievementID)
    {
        if (achievements.Exists(x => x.achievementID.Equals(achievementID)))
        {
            return achievements.Find(x => x.achievementID.Equals(achievementID));
        }

        return null;
    }

    public void ResetAchievement()
    {
        foreach (AchievementData data in achievements)
        {
            data.collect = false;
        }
    }
}

[System.Serializable]
public class AchievementData
{
    public string achievementName;
    [TextArea] public string achievementDescription;
    public string achievementID;
    public bool collect;
}
