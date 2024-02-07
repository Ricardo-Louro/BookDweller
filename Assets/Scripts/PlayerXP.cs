using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    [SerializeField] private UISystem _uiSystem;
    [SerializeField] private int experienceGoal;
    
    private int currentXP;
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

        _uiSystem.UpdateXPUI(experienceGoal, currentXP);
    }

    private void LevelUp()
    {
        //LEVEL UP!
    }
}
