using UnityEngine;

public class EnemyStats : Stats
{
    [SerializeField] private int spawnValue;
    [SerializeField] private int scoreValue;

    public int SpawnValue
    {
        get => spawnValue;
        set => spawnValue = value;
    }

    public int ScoreValue
    {
        get => scoreValue;
        set => scoreValue = value;
    }
}