using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    private GameObject player;
    [SerializeField] private GameObject[] enemyTypes;
    [SerializeField] private int initialMaxHordeValue;

    private int maxHordeValue;
    private int currentHordeValue;

    private float spawnCooldown;
    private float lastSpawnTime;

    // Start is called before the first frame update
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        maxHordeValue = initialMaxHordeValue;
        currentHordeValue = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if(spawnCooldown < (Time.time - lastSpawnTime) && currentHordeValue != maxHordeValue)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        List<GameObject> possibleEnemies = new List<GameObject>();

        foreach(GameObject enemy in enemyTypes)
        {
            if(enemy.GetComponent<EnemyStats>().SpawnValue < maxHordeValue - currentHordeValue)
            {
                possibleEnemies.Add(enemy);
            }
        }
        
        GameObject chosenEnemy;



        if(possibleEnemies.Count == 1)
        {
            chosenEnemy = Instantiate(possibleEnemies[0]);
        }
        else
        {
            int index = Random.Range(0, possibleEnemies.Count -1);
            chosenEnemy = Instantiate(possibleEnemies[index]);
        }

        currentHordeValue -= chosenEnemy.GetComponent<EnemyStats>().ScoreValue;

        if(currentHordeValue > 0)
        {
            Spawn();
        }
    }
}
