using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private int                currentXP;
    [SerializeField] private int                experienceGoal;
    [SerializeField] private int                experienceGoalMultiPerLevel;

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
        playerAttack.UpgradeBulletScale();
        experienceGoal *= experienceGoalMultiPerLevel;
        currentXP = 0;
    }


}
