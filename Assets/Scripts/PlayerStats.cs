using UnityEngine;

public class PlayerStats : Stats
{
    [SerializeField] private int experiencePoints;
    
    public int XP
    {
        get => experiencePoints;
        set => experiencePoints = value;
    }
}