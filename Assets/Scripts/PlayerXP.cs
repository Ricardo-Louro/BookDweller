using UnityEngine;

public class PlayerXP : MonoBehaviour
{
    private int currentXP;
    private int experienceGoal;

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
        }
    }

    private void LevelUp()
    {
        //LEVEL UP!
    }
}
