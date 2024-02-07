using UnityEngine;

public class PlayerStats : Stats
{
    [SerializeField] private int experiencePoints;
    [SerializeField] private int maxHP;

    public int MaxHP
    {
        get => maxHP;
        set => maxHP = value;
    }
    public int XP
    {
        get => experiencePoints;
        set => experiencePoints = value;
    }
}