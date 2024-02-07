using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int score;
    [SerializeField] private UISystem _uiSystem;

    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        _uiSystem.UpdateScoreUI(score);
    }

    public int GetScore() => score;

    
}
