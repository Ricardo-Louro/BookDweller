using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using Quaternion = UnityEngine.Quaternion;
using Random = UnityEngine.Random;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Spawner : MonoBehaviour
{
    private GameObject                          player;
    private PlayerMovement                      playerMov;
    private Vector3                             rotationVector;
    [SerializeField] private float              spawnRadius;
    [SerializeField] private GameObject[]       enemyTypes;
    [SerializeField] private int                initialMaxHordeValue;

    private int maxHordeValue;
    private int currentHordeValue;

    private float spawnCooldown;
    private float lastSpawnTime;

    private IList<GameObject> possibleEnemies;

    // Start is called before the first frame update
    private void Start()
    {
        possibleEnemies = new List<GameObject>();
        player = GameObject.FindWithTag("Player");
        playerMov = player.GetComponent<PlayerMovement>();
        maxHordeValue = initialMaxHordeValue;
        currentHordeValue = 0;
        rotationVector = Vector3.zero;
        lastSpawnTime = float.MinValue;
        StartCoroutine(DuplicateMaxHordeValue());
    }

    // Update is called once per frame
    private void Update()
    {
        
        if(Time.time - lastSpawnTime > spawnCooldown && currentHordeValue < maxHordeValue)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        lastSpawnTime = Time.time;

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

        if(possibleEnemies.Count == 1)
        {
            chosenEnemy = Instantiate(possibleEnemies[0],
                                      new Vector3(spawnLocation.x,
                                                  spawnLocation.y,
                                                  0),
                                      new Quaternion(0,0,0,0));
        }
        else
        {
            int index = Random.Range(0, possibleEnemies.Count -1);
            Debug.Log(index);

            chosenEnemy = Instantiate(possibleEnemies[index],
                                      new Vector3(spawnLocation.x,
                                                  spawnLocation.y,
                                                  0),
                                      new Quaternion(0,0,0,0));
        }

        currentHordeValue += chosenEnemy.GetComponent<EnemyStats>().SpawnValue;

        if(maxHordeValue - currentHordeValue > 0)
        {
            Spawn();
        }
    }

    private IEnumerator DuplicateMaxHordeValue()
    {
        while(true)
        {
            yield return new WaitForSeconds(10);
            maxHordeValue *= 2;
        }
    }
}
