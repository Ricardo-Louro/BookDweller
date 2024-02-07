using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UISystem : MonoBehaviour
{
    [SerializeField] private Slider XPBar;
    [SerializeField] private TMP_Text Score;

    public void UpdateXPUI(int maxXP, int currentXP)
    {
        XPBar.maxValue = maxXP;
        XPBar.value = currentXP;
    }

    public void UpdateScoreUI(int newScore)
    {
        Score.text = $"Score: {newScore}";
    }
}
