using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private PlayerStats _playerStats;
    
    [SerializeField] private int                currentXP;
    [SerializeField] private int                experienceGoal;
    [SerializeField] private int                experienceGoalMultiPerLevel;
    private int                                 currentLevel;
    [SerializeField] private int                bigShotUnlockLevel;

    [SerializeField] private PlayerAttack       playerAttack;

    public int CurrentXp
    {
        get => currentXP;
        set => currentXP = value;
    }

    public int ExperienceGoal
    {
        get => experienceGoal;
        set => experienceGoal = value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        currentXP = 0;    
        currentLevel = 1;
    }

    public void GainXP(int experience)
    {
        currentXP += experience;

        if (currentXP >= experienceGoal)
        {
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;

        if(currentLevel == bigShotUnlockLevel)
        {
            playerAttack.UnlockBigShot();
        }

        playerAttack.UpgradeBasicCooldown();

        if(currentLevel >= bigShotUnlockLevel)
        {
            playerAttack.UpgradeBigScale();
        }

        _playerStats.LVL += 1;
        experienceGoal *= experienceGoalMultiPerLevel;
        currentXP = 0;
    }


}
