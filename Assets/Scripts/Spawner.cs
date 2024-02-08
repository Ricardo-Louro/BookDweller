using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Spawner : MonoBehaviour
{
    private GameObject                          player;
    private Vector3                             rotationVector;
    private float                               spawnRadius;
    [SerializeField] private GameObject[]       enemyTypes;
    [SerializeField] private float              initialMaxHordeValue;
    [SerializeField] private float              hordeIncreasingMultiplier;
    private float                               maxHordeValue;
    private int                                 currentHordeValue;
    [SerializeField] private float              spawnCooldown;
    private IList<GameObject>                   possibleEnemies;
    private bool                                endOfLoop;

    // Start is called before the first frame update
    private void Start()
    {
        possibleEnemies = new List<GameObject>();
        player = GameObject.FindWithTag("Player");
        maxHordeValue = initialMaxHordeValue;
        currentHordeValue = 0;
        rotationVector = Vector3.zero;
        StartCoroutine(IncreaseMaxHordeValue());
        StartCoroutine(Spawn());
    }

    private IEnumerator Spawn()
    {
        while(true)
        {
            yield return new WaitForSeconds(spawnCooldown);
            endOfLoop = false;

            while (!endOfLoop)
            {
                spawnRadius = Random.Range(10,30);

                possibleEnemies.Clear();

                foreach(GameObject enemy in enemyTypes)
                {
                    if(enemy.GetComponent<EnemyStats>().SpawnValue + currentHordeValue <= maxHordeValue)
                    {
                        possibleEnemies.Add(enemy);
                    }
                }

                float randomAngle = Random.Range(0.0f, 2 * Mathf.PI);
                
                rotationVector.x = Mathf.Cos(randomAngle);
                rotationVector.y = Mathf.Sin(randomAngle);

                Vector2 spawnLocation = player.transform.position + rotationVector.normalized * spawnRadius;
                GameObject chosenEnemy;
                
                if(possibleEnemies.Count == 0)
                {
                    endOfLoop = true;
                    chosenEnemy = null;
                }
                else if(possibleEnemies.Count == 1)
                {
                    chosenEnemy = Instantiate(possibleEnemies[0],
                                            new Vector3(spawnLocation.x,
                                                        spawnLocation.y,
                                                        0),
                                            new Quaternion(0,0,0,0));
                }
                else
                {
                    int index = Random.Range(0, possibleEnemies.Count);

                    chosenEnemy = Instantiate(possibleEnemies[index],
                                            new Vector3(spawnLocation.x,
                                                        spawnLocation.y,
                                                        0),
                                            new Quaternion(0,0,0,0));
                }

                if(chosenEnemy is not null)
                {
                    currentHordeValue += chosenEnemy.GetComponent<EnemyStats>().SpawnValue;
                }

                yield return new WaitForSeconds(0.0001f);
            }
        }
    }

    private IEnumerator IncreaseMaxHordeValue()
    {
        while(true)
        {
            yield return new WaitForSeconds(10);
            maxHordeValue *= hordeIncreasingMultiplier;
        }
    }

    public void DeductHordeValue(int value)
    {
        currentHordeValue -= value;
    }
}
