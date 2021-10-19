using TMPro;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    [SerializeField] private Transform popUpTransform;
    [SerializeField] private TextMeshProUGUI achievementName;
    [SerializeField] private TextMeshProUGUI achievementDescription;
    [SerializeField] private float popUpShowDuration = 3f;

    private float popUpShowDurationCounter;

    private void Update()
    {
        if (popUpShowDurationCounter > 0)
        {
            popUpShowDurationCounter -= Time.unscaledDeltaTime;
            popUpTransform.localScale = Vector3.LerpUnclamped(popUpTransform.localScale, Vector3.one, 0.5f);
        }
        else
        {
            popUpTransform.localScale = Vector2.LerpUnclamped(popUpTransform.localScale, Vector3.right, 0.5f);
        }
    }

    public void ShowAchivementPopUp(string achievementName, string achievementDescription)
    {
        this.achievementName.text = achievementName;
        this.achievementDescription.text = achievementDescription;
        popUpShowDurationCounter = popUpShowDuration;
        popUpTransform.localScale = Vector2.right;
    }
}
