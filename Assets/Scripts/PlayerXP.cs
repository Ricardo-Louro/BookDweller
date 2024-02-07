using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    private int currentXP;
    private int experienceGoal;
    private int experienceGoalMultiPerLevel;

    // Start is called before the first frame update
    private void Start()
    {
        currentXP = 0;    
    }

    public void GainXP(int experience)
    {
        currentXP += experience;

        if(currentXP >= experienceGoal)
        {
            LevelUp();

            experienceGoal *= experienceGoalMultiPerLevel;
            currentXP = 0;
        }

        UpdateXPUI();
    }

    private void LevelUp()
    {
        //LEVEL UP!
    }

    private void UpdateXPUI()
    {
        
    }
}
