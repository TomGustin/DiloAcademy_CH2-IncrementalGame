using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UITapText : MonoBehaviour
{
    public float spawnTime;
    public TextMeshProUGUI text;

    private float spawnCounterTime;

    private void OnEnable()
    {
        spawnCounterTime = 0f;
    }

    void Update()
    {
        spawnCounterTime += Time.unscaledDeltaTime;

        if (spawnCounterTime >= spawnTime)
        {
            gameObject.SetActive(false);
        } else
        {
            text.CrossFadeAlpha(0f, 0.5f, false);

            if (text.color.a == 0f)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
