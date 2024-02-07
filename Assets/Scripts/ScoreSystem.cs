using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] private int score;
    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
    }

    public int GetScore() => score;

    
}
